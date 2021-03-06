﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Pokedex.DataAccess.Models;
using Pokedex.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Pokedex.Controllers
{
    /// <summary>
    /// The class that is used to represent the HomeController.
    /// </summary>
    [Authorize]
    [Route("")]
    public class HomeController : Controller
    {
        private static int selectedPokemonId = 0;

        private static int selectedGenerationId = 0;

        private readonly DataService dataService;

        private readonly AppConfig appConfig;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="appConfig">The application configuration.</param>
        /// <param name="dataContext">The database context.</param>
        public HomeController(IOptions<AppConfig> appConfig, DataContext dataContext)
        {
            this.appConfig = appConfig.Value;
            this.dataService = new DataService(dataContext);
        }

        [AllowAnonymous]
        [Route("")]
        public IActionResult Index()
        {
            return this.View(this.appConfig);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("search")]
        public IActionResult Search(string searchText)
        {
            searchText = HttpUtility.UrlDecode(searchText);
            searchText = string.IsNullOrEmpty(searchText) ? string.Empty : searchText.Replace("/", string.Empty).Replace("\\", string.Empty);
            if (string.IsNullOrEmpty(searchText))
            {
                return this.RedirectToAction("AllPokemon", "Home");
            }
            else
            {
                return this.RedirectToAction("SearchRedirect", "Home", new { search = searchText });
            }
        }

        [AllowAnonymous]
        [Route("search/{search}")]
        public IActionResult SearchRedirect(string search)
        {
            search = HttpUtility.UrlDecode(search);

            if (!string.IsNullOrEmpty(search))
            {
                this.ViewData["Search"] = search;
                search = this.dataService.FormatPokemonName(search);

                List<PokemonTypeDetail> model = this.dataService.GetAllPokemonWithTypes()
                                                                 .Where(x => x.Pokemon.Name.Contains(search, StringComparison.OrdinalIgnoreCase))
                                                                 .ToList();

                Pokemon pokemonSearched = this.dataService.GetPokemon(search);

                if (model.Count == 1 || pokemonSearched != null)
                {
                    if (pokemonSearched == null)
                    {
                        pokemonSearched = model[0].Pokemon;
                    }

                    return this.RedirectToAction("Pokemon", "Home", new { Name = pokemonSearched.Name.Replace(": ", "_").Replace(' ', '_').ToLower() });
                }
                else if (model.Count == 0)
                {
                    return this.RedirectToAction("AllPokemon", "Home");
                }
                else
                {
                    AllPokemonTypeViewModel viewModel = new AllPokemonTypeViewModel()
                    {
                        AllPokemon = model,
                        AppConfig = this.appConfig,
                    };

                    return this.View("Search", viewModel);
                }
            }

            return this.RedirectToAction("AllPokemon", "Home");
        }

        [AllowAnonymous]
        [Route("pokemon")]
        public IActionResult AllPokemon()
        {
            List<int> model = this.dataService.GetGenerationsFromPokemon();

            return this.View(model);
        }

        [AllowAnonymous]
        [Route("team_randomizer")]
        public IActionResult TeamRandomizer()
        {
            List<Pokemon> allPokemon = this.dataService.GetAllPokemon();
            List<Generation> generations = this.dataService.GetGenerations();
            List<DataAccess.Models.Type> types = this.dataService.GetTypes();
            List<Game> gamesList = this.dataService.GetGames();
            List<Game> selectableGames = new List<Game>();

            foreach (var gen in generations)
            {
                List<Pokemon> pokemonInGen = allPokemon.Where(x => x.Game.GenerationId == gen.Id).ToList();
                if (pokemonInGen.Count != 0)
                {
                    List<Game> uniqueGames = pokemonInGen.Select(x => x.Game).OrderBy(x => x.ReleaseDate).ThenBy(x => x.Id).GroupBy(y => y.Id).Select(z => z.First()).ToList();
                    List<Game> allGames = gamesList.Where(x => x.GenerationId == gen.Id).ToList();
                    for (var i = 0; i < uniqueGames.Count; i++)
                    {
                        if (i == uniqueGames.Count - 1)
                        {
                            selectableGames.Add(new Game()
                            {
                                Id = uniqueGames[i].Id,
                                Name = string.Join(" / ", allGames.Where(x => x.ReleaseDate >= uniqueGames[i].ReleaseDate).Select(x => x.Name)),
                                GenerationId = gen.Id,
                            });
                        }
                        else
                        {
                            List<Game> games = allGames.Where(x => x.ReleaseDate >= uniqueGames[i].ReleaseDate && x.ReleaseDate < uniqueGames[i + 1].ReleaseDate && !selectableGames.Any(y => y.ReleaseDate == x.ReleaseDate)).ToList();
                            if (games.Count == 0)
                            {
                                selectableGames.Add(uniqueGames[i]);
                            }
                            else
                            {
                                selectableGames.Add(new Game()
                                {
                                    Id = uniqueGames[i].Id,
                                    Name = string.Join(" / ", games.ConvertAll(x => x.Name)),
                                    GenerationId = gen.Id,
                                });
                            }
                        }
                    }
                }
            }

            TeamRandomizerListViewModel model = new TeamRandomizerListViewModel()
            {
                AllGames = selectableGames,
                AllTypes = types,
                AllLegendaryTypes = this.dataService.GetLegendaryTypes(),
            };

            return this.View(model);
        }

        [AllowAnonymous]
        [Route("typing_evaluator")]
        public IActionResult TypingEvaluator()
        {
            TypeEvaluatorViewModel model = new TypeEvaluatorViewModel()
            {
                AllTypes = this.dataService.GetTypes(),
                AllGenerations = this.dataService.GetGenerations(),
            };

            return this.View(model);
        }

        [AllowAnonymous]
        [Route("day_care_evaluator")]
        public IActionResult DayCareEvaluator()
        {
            List<PokemonEggGroupDetail> eggGroupDetails = this.dataService.GetAllBreedablePokemon();
            EggGroupEvaluatorViewModel model = new EggGroupEvaluatorViewModel()
            {
                AllPokemonWithEggGroups = eggGroupDetails,
                AppConfig = this.appConfig,
                GenerationId = this.dataService.GetGenerations().Last().Id,
            };

            List<Pokemon> altForms = this.dataService.GetAllAltForms().ConvertAll(x => x.AltFormPokemon);

            foreach (var e in eggGroupDetails.Where(x => altForms.Any(y => y.Id == x.PokemonId)))
            {
                e.Pokemon.Name = this.dataService.GetAltFormWithFormName(e.PokemonId).Name;
            }

            model.AllPokemon = eggGroupDetails.ConvertAll(x => x.Pokemon);

            return this.View(model);
        }

        [AllowAnonymous]
        [Route("game_availability")]
        public IActionResult GameAvailability()
        {
            List<Game> model = this.dataService.GetGamesForEachReleaseDate();

            return this.View(model);
        }

        [AllowAnonymous]
        [Route("pokemon/{pokemonName}/{pokemonId:int}/{generationId:int}")]
        public IActionResult PokemonWithId(string pokemonName, int pokemonId, int generationId)
        {
            selectedPokemonId = pokemonId;
            if (generationId > this.dataService.GetAvailableGamesFromPokemonId(pokemonId).Last().GenerationId)
            {
                selectedGenerationId = 0;
            }
            else
            {
                selectedGenerationId = generationId;
            }

            return this.RedirectToAction("Pokemon", "Home", new { name = pokemonName });
        }

        [AllowAnonymous]
        [Route("pokemon/{Name}")]
        public IActionResult Pokemon(string name)
        {
            int pokemonId = selectedPokemonId;
            int generationId = selectedGenerationId;
            selectedPokemonId = 0;
            selectedGenerationId = 0;
            name = this.dataService.FormatPokemonName(name);

            Pokemon pokemon = this.dataService.GetPokemon(name);
            if (pokemonId == 0)
            {
                pokemonId = pokemon.Id;
            }

            if (generationId == 0)
            {
                generationId = this.dataService.GetPokemonGameDetails(pokemonId).Last().Game.GenerationId;
            }

            if (pokemon?.IsComplete == true)
            {
                List<PokemonViewModel> pokemonList = new List<PokemonViewModel>();
                PokemonViewModel pokemonDetails = this.dataService.GetPokemonDetails(pokemon, null, this.appConfig);
                pokemonDetails.SurroundingPokemon = this.dataService.GetSurroundingPokemon(pokemon.Id);

                pokemonList.Add(pokemonDetails);

                List<Pokemon> altForms = this.dataService.GetAltForms(pokemon.Id);
                if (altForms.Count > 0)
                {
                    Form form;
                    foreach (var p in altForms)
                    {
                        if (p.IsComplete)
                        {
                            form = this.dataService.GetFormByAltFormId(p.Id);
                            pokemonDetails = this.dataService.GetPokemonDetails(p, form, this.appConfig);

                            pokemonList.Add(pokemonDetails);
                        }
                    }
                }

                List<int> pokemonIds = pokemonList.ConvertAll(x => x.Pokemon.Id);

                if (pokemonIds.IndexOf(pokemonId) == -1)
                {
                    pokemonId = pokemon.Id;
                }

                AdminPokemonDropdownViewModel model = new AdminPokemonDropdownViewModel()
                {
                    PokemonList = pokemonList,
                    PokemonId = pokemonId,
                    GenerationId = generationId,
                };

                if (this.User.IsInRole("Owner"))
                {
                    AllAdminPokemonViewModel allAdminPokemon = this.dataService.GetAllAdminPokemonDetails();
                    DropdownViewModel dropdownViewModel = new DropdownViewModel()
                    {
                        AllPokemon = allAdminPokemon,
                        AppConfig = this.appConfig,
                    };
                    AdminGenerationTableViewModel adminDropdown = new AdminGenerationTableViewModel()
                    {
                        PokemonList = new List<Pokemon>(),
                        DropdownViewModel = dropdownViewModel,
                        AppConfig = this.appConfig,
                    };

                    foreach (var p in pokemonList)
                    {
                        adminDropdown.PokemonList.Add(p.Pokemon);
                    }

                    model.AdminDropdown = adminDropdown;
                }

                return this.View(model);
            }
            else
            {
                return this.RedirectToAction("AllPokemon", "Home");
            }
        }

        [AllowAnonymous]
        [Route("type_chart")]
        public IActionResult TypeChart()
        {
            TypeChartViewModel model = new TypeChartViewModel()
            {
                TypeChart = this.dataService.GetTypeCharts(),
                Types = this.dataService.GetTypes(),
            };

            return this.View(model);
        }

        [AllowAnonymous]
        [Route("capture_calculator")]
        public IActionResult CaptureCalculator()
        {
            CaptureCalculatorViewModel model = new CaptureCalculatorViewModel()
            {
                AllPokemon = this.dataService.GetAllPokemonForCaptureCalculator(),
                AllPokeballs = this.dataService.GetPokeballsForCaptureCalculator(),
                AllStatuses = this.dataService.GetStatuses(),
                AllGenerations = this.dataService.GetGenerations(),
            };

            return this.View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("comment")]
        public IActionResult Comment()
        {
            CommentViewModel model = new CommentViewModel()
            {
                AllCategories = this.dataService.GetCommentCategories(),
                AllPages = this.dataService.GetCommentPages(),
            };
            return this.View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("comment")]
        public IActionResult Comment(Comment comment)
        {
            if (!this.ModelState.IsValid)
            {
                CommentViewModel model = new CommentViewModel()
                {
                    AllCategories = this.dataService.GetCommentCategories(),
                    AllPages = this.dataService.GetCommentPages(),
                };
                return this.View(model);
            }

            if (!string.IsNullOrEmpty(comment.PokemonName))
            {
                Pokemon pokemon = this.dataService.GetPokemon(this.dataService.FormatPokemonName(comment.PokemonName));
                if (pokemon == null)
                {
                    comment.PokemonName = null;
                }
            }

            if (!string.IsNullOrEmpty(comment.PokemonName) && comment.PageId != 3)
            {
                comment.PageId = 3;
            }

            if (this.User.Identity.Name != null)
            {
                comment.CommentorId = this.dataService.GetUserWithUsername(this.User.Identity.Name).Id;
            }

            this.dataService.AddComment(comment);

            comment.Category = this.dataService.GetCommentCategory(comment.CategoryId);
            comment.Page = this.dataService.GetCommentPage(comment.PageId);

            this.EmailComment(comment);

            return this.RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        [Route("about")]
        public IActionResult About()
        {
            return this.View();
        }

        [AllowAnonymous]
        [Route("error")]
        public IActionResult Error()
        {
            return this.View();
        }

        private void EmailComment(Comment comment)
        {
            try
            {
                if (comment.CommentorId != 1)
                {
                    MailAddress fromAddress = new MailAddress(this.appConfig.EmailAddress, "Pokemon Database Website");
                    MailAddress toAddress = new MailAddress(this.appConfig.EmailAddress, "Pokemon Database Email");
                    string body = comment.Category.Name;
                    if (comment.Page != null)
                    {
                        body = string.Concat(body, " for ", comment.Page.Name);
                    }

                    if (comment.PokemonName != null)
                    {
                        body = string.Concat(body, " (", comment.PokemonName, ")");
                    }

                    if (comment.CommentorId != null)
                    {
                        body = string.Concat(body, " by ", this.dataService.GetUserById((int)comment.CommentorId).Username);
                    }
                    else
                    {
                        body = string.Concat(body, " by Anonymous User");
                    }

                    body = string.Concat(body, ": ", comment.Name);

                    SmtpClient smtp = new SmtpClient()
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(fromAddress.Address, this.appConfig.EmailAddressPassword),
                    };

                    using MailMessage message = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = "New Comment for Pokémon Database",
                        Body = body,
                    };
                    smtp.Send(message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Email could not be sent. ", (ex.InnerException != null) ? ex.InnerException.ToString() : ex.Message);
            }
        }
    }
}
