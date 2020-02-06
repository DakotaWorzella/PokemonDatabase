using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using Pokedex.DataAccess.Models;

using Pokedex.Models;

namespace Pokedex.Controllers
{
    [Authorize]
    [Route("")]
    public class PokemonTeamController : Controller
    {
        private static List<PokemonTeam> _pokemonTeams;

        private readonly DataService _dataService;

        private readonly AppConfig _appConfig;

        public PokemonTeamController(IOptions<AppConfig> appConfig, DataContext dataContext)
        {
            // Instantiate an instance of the data service.
            this._appConfig = appConfig.Value;
            this._dataService = new DataService(dataContext);
        }

        [HttpGet]
        [Route("create_team")]
        public IActionResult CreateTeam()
        {
            CreatePokemonTeamViewModel model = new CreatePokemonTeamViewModel(){
                AllGames = this._dataService.GetGames().Where(x => x.ReleaseDate <= DateTime.Now).ToList(),
                UserId = this._dataService.GetUserWithUsername(User.Identity.Name).Id,
            };
            
            return this.View(model);
        }

        [HttpPost]
        [Route("create_team")]
        public IActionResult CreateTeam(CreatePokemonTeamViewModel pokemonTeam)
        {
            if (!this.ModelState.IsValid)
            {
                CreatePokemonTeamViewModel model = new CreatePokemonTeamViewModel(){
                    AllGames = this._dataService.GetGames().Where(x => x.ReleaseDate <= DateTime.Now).ToList(),
                    UserId = this._dataService.GetUserWithUsername(User.Identity.Name).Id,
                };
            
                return this.View(model);
            }

            this._dataService.AddPokemonTeam(pokemonTeam);

            return this.RedirectToAction("PokemonTeams", "User");
        }

        [HttpGet]
        [Route("import_teams")]
        public IActionResult ImportTeams()
        {
            return this.View();
        }

        [HttpPost]
        [Route("import_teams")]
        public IActionResult ImportTeams(string importedTeams)
        {
            int userId = this._dataService.GetUserWithUsername(User.Identity.Name).Id;
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            List<string> pokemonTeams = importedTeams.Split("\r\n===").ToList();
            for(var i = 1; i < pokemonTeams.Count(); i++)
            {
                pokemonTeams[i] = "===" + pokemonTeams[i];
            }

            foreach(var p in pokemonTeams)
            {
                this.CreateTeamFromImport(p, userId);
            }

            return this.RedirectToAction("PokemonTeams", "User");
        }

        [HttpGet]
        [Route("create_pokemon/{pokemonTeamId:int}")]
        public IActionResult CreatePokemon(int pokemonTeamId)
        {
            this.UpdatePokemonTeamList();
            if(_pokemonTeams.Count() < pokemonTeamId || _pokemonTeams[pokemonTeamId - 1].SixthPokemonId != null)
            {
                return this.RedirectToAction("PokemonTeams", "User");
            }
            else
            {
                PokemonTeam pokemonTeam = _pokemonTeams[pokemonTeamId - 1];
                List<Pokemon> pokemonList = this.FillPokemonList(pokemonTeam);
                CreateTeamPokemonViewModel model = new CreateTeamPokemonViewModel(){
                    AllPokemon = pokemonList,
                    AllNatures = this._dataService.GetNatures(),
                    NatureId = this._dataService.GetNatureByName("Serious").Id,
                    GameId = pokemonTeam.GameId,
                    Level = 100,
                    Happiness = 255,
                };

                return this.View(model);
            }
        }

        [HttpPost]
        [Route("create_pokemon/{pokemonTeamId:int}")]
        public IActionResult CreatePokemon(CreateTeamPokemonViewModel pokemonTeamDetail, int pokemonTeamId)
        {
            if (!this.ModelState.IsValid)
            {
                CreateTeamPokemonViewModel model = new CreateTeamPokemonViewModel(){
                    AllPokemon = this.FillPokemonList(_pokemonTeams[pokemonTeamId - 1]),
                    AllNatures = this._dataService.GetNatures(),
                    NatureId = this._dataService.GetNatureByName("Serious").Id,
                    GameId = pokemonTeamDetail.GameId,
                    Level = 100,
                    Happiness = 255,
                };

                return this.View(model);
            }

            PokemonTeam pokemonTeam = _pokemonTeams[pokemonTeamId - 1];

            Pokemon pokemon = this._dataService.GetPokemonById(pokemonTeamDetail.PokemonId);

            if(pokemon.GenderRatioId == 10)
            {
                pokemonTeamDetail.Gender = null;
            } 

            int pokemonTeamDetailId = this._dataService.AddPokemonTeamDetail(pokemonTeamDetail);

            pokemonTeam.InsertPokemon(this._dataService.GetPokemonTeamDetail(pokemonTeamDetailId));

            this._dataService.UpdatePokemonTeam(pokemonTeam);

            return this.RedirectToAction("PokemonTeams", "User");
        }

        [HttpGet]
        [Route("update_pokemon/{pokemonTeamId:int}/{pokemonTeamDetailId:int}")]
        public IActionResult EditPokemon(int pokemonTeamId, int pokemonTeamDetailId)
        {
            this.UpdatePokemonTeamList();
            if(_pokemonTeams.Count() < pokemonTeamId)
            {
                return this.RedirectToAction("PokemonTeams", "User");
            }
            else
            {
                PokemonTeam pokemonTeam = _pokemonTeams[pokemonTeamId - 1];
                PokemonTeamDetail pokemonTeamDetail = this._dataService.GetPokemonTeamDetail(pokemonTeam.GrabPokemonTeamDetailIds()[pokemonTeamDetailId - 1]);

                if(pokemonTeamDetail.Nature == null)
                {
                    pokemonTeamDetail.NatureId = this._dataService.GetNatureByName("Serious").Id;
                }
                
                List<Pokemon> pokemonList = this.FillPokemonList(pokemonTeam);
                UpdateTeamPokemonViewModel model = new UpdateTeamPokemonViewModel(){
                    PokemonTeamDetail = pokemonTeamDetail,
                    AllPokemon = pokemonList,
                    AllNatures = this._dataService.GetNatures(),
                    AllAbilities = this._dataService.GetAbilities(),
                    AllBattleItems = this._dataService.GetBattleItems().OrderBy(x => x.Name).ToList(),
                    GameId = pokemonTeam.GameId,
                };

                return this.View(model);
            }
        }

        [HttpPost]
        [Route("update_pokemon/{pokemonTeamId:int}/{pokemonTeamDetailId:int}")]
        public IActionResult EditPokemon(PokemonTeamDetail pokemonTeamDetail)
        {
            PokemonTeam pokemonTeam = this._dataService.GetPokemonTeamFromPokemon(pokemonTeamDetail.Id);
            if (!this.ModelState.IsValid)
            {
                List<Pokemon> pokemonList = this.FillPokemonList(pokemonTeam);

                UpdateTeamPokemonViewModel model = new UpdateTeamPokemonViewModel(){
                    PokemonTeamDetail = pokemonTeamDetail,
                    AllPokemon = pokemonList,
                    AllNatures = this._dataService.GetNatures(),
                    AllAbilities = this._dataService.GetAbilities(),
                    AllBattleItems = this._dataService.GetBattleItems().OrderBy(x => x.Name).ToList(),
                    GameId = pokemonTeam.GameId,
                };

                return this.View(model);
            }

            Pokemon pokemon = this._dataService.GetPokemonById(pokemonTeamDetail.PokemonId);

            if(pokemon.GenderRatioId == 10)
            {
                pokemonTeamDetail.Gender = null;
            }

            this._dataService.UpdatePokemonTeamDetail(pokemonTeamDetail);

            return this.RedirectToAction("PokemonTeams", "User");
        }

        [HttpGet]
        [Route("delete_team/{pokemonTeamId:int}")]
        public IActionResult DeleteTeam(int pokemonTeamId)
        {
            this.UpdatePokemonTeamList();
            if(_pokemonTeams.Count() < pokemonTeamId)
            {
                return this.RedirectToAction("PokemonTeams", "User");
            }
            else
            {
                PokemonTeam pokemonTeam = _pokemonTeams[pokemonTeamId - 1];
                PokemonTeamViewModel model = new PokemonTeamViewModel(){
                    Id = pokemonTeam.Id,
                    PokemonTeamName = pokemonTeam.PokemonTeamName,
                    FirstPokemon = pokemonTeam.FirstPokemon,
                    SecondPokemon = pokemonTeam.SecondPokemon,
                    ThirdPokemon = pokemonTeam.ThirdPokemon,
                    FourthPokemon = pokemonTeam.FourthPokemon,
                    FifthPokemon = pokemonTeam.FifthPokemon,
                    SixthPokemon = pokemonTeam.SixthPokemon,
                    AppConfig = this._appConfig,
                };

                return this.View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("delete_team/{pokemonTeamId:int}")]
        public IActionResult DeleteTeam(PokemonTeam pokemonTeam)
        {
            this._dataService.DeletePokemonTeam(pokemonTeam.Id);

            return this.RedirectToAction("PokemonTeams", "User");
        }

        [HttpGet]
        [Route("update_ev/{pokemonTeamId:int}/{pokemonTeamDetailId:int}")]
        public IActionResult EditEV(int pokemonTeamId, int pokemonTeamDetailId)
        {
            this.UpdatePokemonTeamList();
            if(_pokemonTeams.Count() < pokemonTeamId)
            {
                return this.RedirectToAction("PokemonTeams", "User");
            }
            else
            {
                PokemonTeamEV pokemonEV = this._dataService.GetPokemonTeamEV((int)_pokemonTeams[pokemonTeamId - 1].GrabPokemonTeamDetails[pokemonTeamDetailId - 1].PokemonTeamEVId);
                PokemonTeamEVViewModel model = new PokemonTeamEVViewModel(){
                    Id = pokemonEV.Id,
                    Health = pokemonEV.Health,
                    Attack = pokemonEV.Attack,
                    Defense = pokemonEV.Defense,
                    SpecialAttack = pokemonEV.SpecialAttack,
                    SpecialDefense = pokemonEV.SpecialDefense,
                    Speed = pokemonEV.Speed,
                    PokemonId = pokemonEV.Id,
                };

                return this.View(model);
            }
        }

        [HttpPost]
        [Route("update_ev/{pokemonTeamId:int}/{pokemonTeamDetailId:int}")]
        public IActionResult EditEV(PokemonTeamEVViewModel pokemonTeamEV)
        {
            if (!this.ModelState.IsValid)
            {
                PokemonTeamEV pokemon = this._dataService.GetPokemonTeamEV(pokemonTeamEV.PokemonId);
                PokemonTeamEVViewModel model = new PokemonTeamEVViewModel(){
                    Id = pokemon.Id,
                    Health = pokemon.Health,
                    Attack = pokemon.Attack,
                    Defense = pokemon.Defense,
                    SpecialAttack = pokemon.SpecialAttack,
                    SpecialDefense = pokemon.SpecialDefense,
                    Speed = pokemon.Speed,
                    PokemonId = pokemonTeamEV.PokemonId,
                };
            
                return this.View(model);
            }
            else if (pokemonTeamEV.EVTotal > 510)
            {
                PokemonTeamEV pokemon = this._dataService.GetPokemonTeamEV(pokemonTeamEV.PokemonId);
                PokemonTeamEVViewModel model = new PokemonTeamEVViewModel(){
                    Id = pokemon.Id,
                    Health = pokemon.Health,
                    Attack = pokemon.Attack,
                    Defense = pokemon.Defense,
                    SpecialAttack = pokemon.SpecialAttack,
                    SpecialDefense = pokemon.SpecialDefense,
                    Speed = pokemon.Speed,
                    PokemonId = pokemonTeamEV.PokemonId,
                };

                this.ModelState.AddModelError("EVTotal", "Total EVs max at 510.");
                return this.View(model);
            }

            this._dataService.UpdatePokemonTeamEV(pokemonTeamEV);

            return this.RedirectToAction("PokemonTeams", "User");
        }

        [HttpGet]
        [Route("update_moveset/{pokemonTeamId:int}/{pokemonTeamDetailId:int}")]
        public IActionResult EditMoveset(int pokemonTeamId, int pokemonTeamDetailId)
        {
            this.UpdatePokemonTeamList();
            if(_pokemonTeams.Count() < pokemonTeamId)
            {
                return this.RedirectToAction("PokemonTeams", "User");
            }
            else
            {
                PokemonTeamMoveset pokemonMoveset = this._dataService.GetPokemonTeamMoveset((int)_pokemonTeams[pokemonTeamId - 1].GrabPokemonTeamDetails[pokemonTeamDetailId - 1].PokemonTeamMovesetId);
                PokemonTeamMovesetViewModel model = new PokemonTeamMovesetViewModel(){
                    Id = pokemonMoveset.Id,
                    FirstMove = pokemonMoveset.FirstMove,
                    SecondMove = pokemonMoveset.SecondMove,
                    ThirdMove = pokemonMoveset.ThirdMove,
                    FourthMove = pokemonMoveset.FourthMove,
                    PokemonId = pokemonMoveset.Id,
                };

                return this.View(model);
            }
        }

        [HttpPost]
        [Route("update_moveset/{pokemonTeamId:int}/{pokemonTeamDetailId:int}")]
        public IActionResult EditMoveset(PokemonTeamMovesetViewModel pokemonTeamMoveset)
        {
            if (!this.ModelState.IsValid)
            {
                PokemonTeamMoveset pokemon = this._dataService.GetPokemonTeamMoveset(pokemonTeamMoveset.PokemonId);
                PokemonTeamMovesetViewModel model = new PokemonTeamMovesetViewModel(){
                    Id = pokemonTeamMoveset.Id,
                    FirstMove = pokemonTeamMoveset.FirstMove,
                    SecondMove = pokemonTeamMoveset.SecondMove,
                    ThirdMove = pokemonTeamMoveset.ThirdMove,
                    FourthMove = pokemonTeamMoveset.FourthMove,
                    PokemonId = pokemonTeamMoveset.Id,
                };
            
                return this.View(model);
            }

            this._dataService.UpdatePokemonTeamMoveset(pokemonTeamMoveset);

            return this.RedirectToAction("PokemonTeams", "User");
        }

        [HttpGet]
        [Route("update_iv/{pokemonTeamId:int}/{pokemonTeamDetailId:int}")]
        public IActionResult EditIV(int pokemonTeamId, int pokemonTeamDetailId)
        {
            this.UpdatePokemonTeamList();
            if(_pokemonTeams.Count() < pokemonTeamId)
            {
                return this.RedirectToAction("PokemonTeams", "User");
            }
            else
            {
                PokemonTeamIV pokemonIV = this._dataService.GetPokemonTeamIV((int)_pokemonTeams[pokemonTeamId - 1].GrabPokemonTeamDetails[pokemonTeamDetailId - 1].PokemonTeamIVId);
                PokemonTeamIVViewModel model = new PokemonTeamIVViewModel(){
                    Id = pokemonIV.Id,
                    Health = pokemonIV.Health,
                    Attack = pokemonIV.Attack,
                    Defense = pokemonIV.Defense,
                    SpecialAttack = pokemonIV.SpecialAttack,
                    SpecialDefense = pokemonIV.SpecialDefense,
                    Speed = pokemonIV.Speed,
                    PokemonId = pokemonIV.Id,
                };

                return this.View(model);
            }
        }

        [HttpPost]
        [Route("update_iv/{pokemonTeamId:int}/{pokemonTeamDetailId:int}")]
        public IActionResult EditIV(PokemonTeamIVViewModel pokemonTeamIV)
        {
            if (!this.ModelState.IsValid)
            {
                PokemonTeamEV pokemon = this._dataService.GetPokemonTeamEV(pokemonTeamIV.PokemonId);
                PokemonTeamIVViewModel model = new PokemonTeamIVViewModel(){
                    Id = pokemon.Id,
                    Health = pokemon.Health,
                    Attack = pokemon.Attack,
                    Defense = pokemon.Defense,
                    SpecialAttack = pokemon.SpecialAttack,
                    SpecialDefense = pokemon.SpecialDefense,
                    Speed = pokemon.Speed,
                    PokemonId = pokemonTeamIV.PokemonId,
                };
            
                return this.View(model);
            }

            this._dataService.UpdatePokemonTeamIV(pokemonTeamIV);

            return this.RedirectToAction("PokemonTeams", "User");
        }

        [HttpGet]
        [Route("delete_team_pokemon/{pokemonTeamId:int}/{pokemonTeamDetailId:int}")]
        public IActionResult DeletePokemon(int pokemonTeamId, int pokemonTeamDetailId)
        {
            this.UpdatePokemonTeamList();
            if(_pokemonTeams.Count() < pokemonTeamId)
            {
                return this.RedirectToAction("PokemonTeams", "User");
            }
            else
            {
                PokemonTeam pokemonTeam = _pokemonTeams[pokemonTeamId - 1];
                PokemonTeamDetail pokemonTeamDetail = this._dataService.GetPokemonTeamDetail(pokemonTeam.GrabPokemonTeamDetailIds()[pokemonTeamDetailId - 1]);
                PokemonTeamDetailViewModel model = new PokemonTeamDetailViewModel(){
                    Id = pokemonTeamDetail.Id,
                    Pokemon = pokemonTeamDetail.Pokemon,
                    AppConfig = this._appConfig,
                };

                return this.View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("delete_team_pokemon/{pokemonTeamId:int}/{pokemonTeamDetailId:int}")]
        public IActionResult DeletePokemon(PokemonTeamDetail pokemonTeamDetail)
        {
            this._dataService.DeletePokemonTeamDetail(pokemonTeamDetail.Id);

            return this.RedirectToAction("PokemonTeams", "User");
        }

        [HttpGet]
        [Route("edit_team/{pokemonTeamId:int}")]
        public IActionResult EditTeam(int pokemonTeamId)
        {
            this.UpdatePokemonTeamList();
            
            List<Game> allGames = this._dataService.GetGames();
            PokemonTeam pokemonTeam = _pokemonTeams[pokemonTeamId - 1];
            Generation generation = this._dataService.GetLatestGeneration(pokemonTeam.Id);

            UpdatePokemonTeamViewModel model = new UpdatePokemonTeamViewModel(){
                Id = pokemonTeam.Id,
                PokemonTeamName = pokemonTeam.PokemonTeamName,
                GameId = pokemonTeam.GameId,
                UserId = pokemonTeam.UserId,
            };

            if(generation != null)
            {
                model.AllGames = allGames.Where(x => x.GenerationId >= generation.Id).ToList();
            }
            else
            {
                model.AllGames = allGames;
            }

            return this.View(model);
        }

        [HttpPost]
        [Route("edit_team/{pokemonTeamId:int}")]
        public IActionResult EditTeam(PokemonTeam newPokemonTeam)
        {
            PokemonTeam originalPokemonTeam = this._dataService.GetPokemonTeamNoIncludes(newPokemonTeam.Id);
            if (!this.ModelState.IsValid)
            {
                List<Game> allGames = this._dataService.GetGames();
                Generation generation = this._dataService.GetLatestGeneration(newPokemonTeam.Id);

                UpdatePokemonTeamViewModel model = new UpdatePokemonTeamViewModel(){
                    Id = originalPokemonTeam.Id,
                    PokemonTeamName = originalPokemonTeam.PokemonTeamName,
                    GameId = originalPokemonTeam.GameId,
                    UserId = originalPokemonTeam.UserId,
                };

                if(generation != null)
                {
                    model.AllGames = allGames.Where(x => x.GenerationId >= generation.Id).ToList();
                }
                else
                {
                    model.AllGames = allGames;
                }

                return this.View(model);
            }

            if(originalPokemonTeam.PokemonTeamName != newPokemonTeam.PokemonTeamName)
            {
                originalPokemonTeam.PokemonTeamName = newPokemonTeam.PokemonTeamName;
            }

            if(originalPokemonTeam.GameId != newPokemonTeam.GameId)
            {
                originalPokemonTeam.GameId = newPokemonTeam.GameId;
            }

            this._dataService.UpdatePokemonTeam(originalPokemonTeam);

            return this.RedirectToAction("PokemonTeams", "User");
        }

        private void UpdatePokemonTeamList()
        {
            _pokemonTeams = this._dataService.GetAllPokemonTeams(User.Identity.Name);
        }

        private List<Pokemon> FillPokemonList(PokemonTeam pokemonTeam)
        {
            List<Pokemon> pokemonList = this._dataService.GetAllPokemon().ToList();
            if(pokemonTeam.GameId != null)
            {
                List<PokemonGameDetail> pokemonGameDetails = this._dataService.GetPokemonGameDetailsByGame((int)pokemonTeam.GameId);
                pokemonList = pokemonList.Where(x => pokemonGameDetails.Any(y => y.PokemonId == x.Id)).ToList();
            }

            List<Pokemon> altForms = this._dataService.GetAllAltForms().Select(x => x.AltFormPokemon).ToList();
            
            foreach(var p in pokemonList.Where(x => altForms.Any(y => y.Id == x.Id)).ToList())
            {
                p.Name = this._dataService.GetAltFormWithFormName(p.Id).Name;
            }

            pokemonList = pokemonList.OrderBy(x => x.Name).ToList();

            return pokemonList;
        }

        private void CreateTeamFromImport(string importedTeam, int userId)
        {
            string teamName = importedTeam.Split("===\r\n")[0];
            string pokemonString;
            if(teamName.IndexOf("===") != -1)
            {
                teamName += "===";
                int teamNameStart = teamName.IndexOf("] ");
                if(teamNameStart == -1)
                {
                    teamNameStart = 4;
                }
                else
                {
                    teamNameStart += 2;
                }

                int teamNameTo = teamName.LastIndexOf(" ===");
                teamName = teamName.Substring(teamNameStart, teamNameTo - teamNameStart);
                pokemonString = "\r\n" + importedTeam.Split("===\r\n")[1];
            }
            else
            {
                teamName = "Import From Pokemon Showdown";
                pokemonString = importedTeam;
            }

            PokemonTeam pokemonTeam = new PokemonTeam()
            {
                PokemonTeamName = teamName,
                UserId = userId,
            };
            List<string> pokemonList = pokemonString.Split("\r\n\r\n").ToList();
            List<string> filteredPokemonList = new List<string>();
            foreach(var p in pokemonList)
            {
                if(!string.IsNullOrEmpty(p) && filteredPokemonList.Count() < 6)
                {
                    filteredPokemonList.Add(p);
                }
            }

            // Used to ensure that there are no issues when creating the pokemon detail.
            List<PokemonTeamDetailViewModel> pokemonDetailList = new List<PokemonTeamDetailViewModel>();

            foreach(var p in filteredPokemonList)
            {
                pokemonDetailList.Add(this.CreatePokemonDetailFromImport(p));
            }

            foreach(var p in pokemonDetailList)
            {
                if(p.EVs != null)
                {
                    p.PokemonTeamEVId = this._dataService.AddPokemonTeamEV(p.EVs);
                }

                if(p.IVs != null)
                {
                    p.PokemonTeamIVId = this._dataService.AddPokemonTeamIV(p.IVs);
                }
                
                if(p.Moveset != null)
                {
                    p.PokemonTeamMovesetId = this._dataService.AddPokemonTeamMoveset(p.Moveset);
                }

                int pokemonId = this._dataService.AddPokemonTeamDetail(p);
                pokemonTeam.InsertPokemon(this._dataService.GetPokemonTeamDetail(pokemonId));
            }

            this._dataService.AddPokemonTeam(pokemonTeam);
        }

        private PokemonTeamDetailViewModel CreatePokemonDetailFromImport(string importedPokemon)
        {
            PokemonTeamDetailViewModel pokemonTeamDetail = new PokemonTeamDetailViewModel();
            string pokemonName = importedPokemon.Split("\r\n")[0];
            string remainingImportedText = importedPokemon.Replace(pokemonName + "\r\n", string.Empty);
            pokemonName = pokemonName.Trim();

            #region HeldItem
            if(pokemonName.IndexOf(" @ ") != -1)
            {
                string itemName = pokemonName.Split(" @ ")[1];
                BattleItem battleItem = this._dataService.GetBattleItemByName(itemName);
                if(battleItem != null)
                {
                    pokemonTeamDetail.BattleItemId = battleItem.Id;
                }

                pokemonName = pokemonName.Split(" @ " + itemName)[0];
            }
            #endregion

            #region Gender
            int genderCheckStart = pokemonName.LastIndexOf('(');
            if(genderCheckStart != -1 && pokemonName.Substring(genderCheckStart + 2, 1) == ")")
            {
                string genderInitial = pokemonName.Substring(genderCheckStart + 1, 1);
                if(genderInitial == "M")
                {
                    pokemonTeamDetail.Gender = "Male";
                }
                else if(genderInitial == "F")
                {
                    pokemonTeamDetail.Gender = "Female";
                }

                pokemonName = pokemonName.Split(" (" + genderInitial + ")")[0];
            }
            #endregion

            #region Nickname
            if(pokemonName.IndexOf("(") != -1)
            {
                pokemonTeamDetail.Nickname = pokemonName.Substring(0, pokemonName.IndexOf("(") - 1);
                pokemonName = pokemonName.Replace(pokemonTeamDetail.Nickname + " (", string.Empty);
                pokemonName = pokemonName.Replace(")", string.Empty);
            }
            #endregion

            #region Pokemon
            Pokemon pokemon;

            // Used to check for alternate form
            if(pokemonName.LastIndexOf('-') != -1)
            {
                string formName = pokemonName.Split('-').Last();
                if(formName == "Gmax")
                {
                    pokemonName = pokemonName.Replace("-Gmax", string.Empty);
                    formName = string.Empty;
                }

                if(pokemonName == "Meowstic-F" || pokemonName == "Indeedee-F")
                {
                    formName = "Female";
                }

                Form form = this._dataService.GetFormByName(formName);
                if(pokemonName == "Meowstic-F" || pokemonName == "Indeedee-F")
                {
                    formName = "F";
                }

                if(form != null)
                {
                    pokemon = this._dataService.GetPokemonFromNameAndFormName(pokemonName.Replace("-" + formName, string.Empty), form.Name);
                }
                else
                {
                    pokemon = this._dataService.GetPokemon(pokemonName);
                }
            }
            else
            {
                pokemon = this._dataService.GetPokemon(pokemonName);
            }

            pokemonTeamDetail.PokemonId = pokemon.Id;
            #endregion

            #region Ability
            Ability ability;
            string abilityName = remainingImportedText.Split("\r\n")[0];
            if(abilityName.Contains("Ability: "))
            {
                remainingImportedText = remainingImportedText.Replace(abilityName + "\r\n", string.Empty);
                abilityName = abilityName.Split("Ability: ")[1].Trim();
                ability = this._dataService.GetAbilityByName(abilityName);
            }
            else
            {
                ability = this._dataService.GetAbilitiesForPokemon(pokemon.Id).First();
            }
            
            pokemonTeamDetail.AbilityId = ability.Id;
            #endregion

            #region Level
            if(remainingImportedText.Contains("Level:"))
            {
                string pokemonLevel = remainingImportedText.Split("\r\n")[0];
                remainingImportedText = remainingImportedText.Replace(pokemonLevel + "\r\n", string.Empty);
                pokemonLevel = pokemonLevel.Trim();
                pokemonTeamDetail.Level = Convert.ToByte(pokemonLevel.Substring(pokemonLevel.IndexOf(':') + 2, pokemonLevel.Length - (pokemonLevel.IndexOf(':') + 2)));
                if(pokemonTeamDetail.Level == 0)
                {
                    pokemonTeamDetail.Level = 1;
                }
                else if (pokemonTeamDetail.Level > 100 || string.Compare(pokemonTeamDetail.Level.ToString(), pokemonLevel.Substring(pokemonLevel.IndexOf(':') + 2)) != 0)
                {
                    pokemonTeamDetail.Level = 100;
                }
            }
            else
            {
                pokemonTeamDetail.Level = 100;
            }
            #endregion

            #region Shiny
            if(remainingImportedText.Contains("Shiny: Yes"))
            {
                remainingImportedText = remainingImportedText.Replace(remainingImportedText.Split("\r\n")[0] + "\r\n", string.Empty);
                pokemonTeamDetail.IsShiny = true;
            }
            #endregion

            #region Happiness
            if(remainingImportedText.Contains("Happiness:"))
            {
                string happiness = remainingImportedText.Split("\r\n")[0];
                remainingImportedText = remainingImportedText.Replace(happiness + "\r\n", string.Empty);
                happiness = happiness.Trim();
                pokemonTeamDetail.Happiness = Convert.ToByte(happiness.Substring(happiness.IndexOf(':') + 2, happiness.Length - (happiness.IndexOf(':') + 2)));
                if(string.Compare(pokemonTeamDetail.Happiness.ToString(), happiness.Substring(happiness.IndexOf(':') + 2)) != 0)
                {
                    pokemonTeamDetail.Happiness = 255;
                }
            }
            else
            {
                pokemonTeamDetail.Happiness = 255;
            }
            #endregion

            #region EVs
            if(remainingImportedText.Contains("EVs:"))
            {
                string evs = remainingImportedText.Split("\r\n")[0];
                remainingImportedText = remainingImportedText.Replace(evs + "\r\n", string.Empty);
                PokemonTeamEV pokemonEVs = new PokemonTeamEV();
                if(evs.Contains("HP"))
                {
                    string health = evs.Substring(evs.IndexOf("HP") - 4, 3);
                    health = health.Replace(":", string.Empty).Replace("/", string.Empty).Trim();
                    pokemonEVs.Health = Convert.ToByte(health);
                }

                if(evs.Contains("Atk"))
                {
                    string attack = evs.Substring(evs.IndexOf("Atk") - 4, 3);
                    attack = attack.Replace(":", string.Empty).Replace("/", string.Empty).Trim();
                    pokemonEVs.Attack = Convert.ToByte(attack);
                }

                if(evs.Contains("Def"))
                {
                    string defense = evs.Substring(evs.IndexOf("Def") - 4, 3);
                    defense = defense.Replace(":", string.Empty).Replace("/", string.Empty).Trim();
                    pokemonEVs.Defense = Convert.ToByte(defense);
                }

                if(evs.Contains("SpA"))
                {
                    string specialAttack = evs.Substring(evs.IndexOf("SpA") - 4, 3);
                    specialAttack = specialAttack.Replace(":", string.Empty).Replace("/", string.Empty).Trim();
                    pokemonEVs.SpecialAttack = Convert.ToByte(specialAttack);
                }

                if(evs.Contains("SpD"))
                {
                    string specialDefense = evs.Substring(evs.IndexOf("SpD") - 4, 3);
                    specialDefense = specialDefense.Replace(":", string.Empty).Replace("/", string.Empty).Trim();
                    pokemonEVs.SpecialDefense = Convert.ToByte(specialDefense);
                }

                if(evs.Contains("Spe"))
                {
                    string speed = evs.Substring(evs.IndexOf("Spe") - 4, 3);
                    speed = speed.Replace(":", string.Empty).Replace("/", string.Empty).Trim();
                    pokemonEVs.Speed = Convert.ToByte(speed);
                }

                pokemonTeamDetail.EVs = pokemonEVs;
            }
            #endregion

            #region Nature
            if(remainingImportedText.Contains("Nature"))
            {
                string natureName = remainingImportedText.Split("\r\n")[0];
                remainingImportedText = remainingImportedText.Replace(natureName + "\r\n", string.Empty);
                natureName = natureName.Replace("Nature", string.Empty).Trim();
                Nature nature = this._dataService.GetNatureByName(natureName);
                pokemonTeamDetail.NatureId = nature.Id;
            }
            else
            {
                pokemonTeamDetail.NatureId = this._dataService.GetNatureByName("Serious").Id;
            }
            #endregion

            #region IVs
            if(remainingImportedText.Contains("IVs:"))
            {
                string ivs = remainingImportedText.Split("\r\n")[0];
                remainingImportedText = remainingImportedText.Replace(ivs + "\r\n", string.Empty);
                PokemonTeamIV pokemonIVs = new PokemonTeamIV();
                if(ivs.Contains("HP"))
                {
                    string health = ivs.Substring(ivs.IndexOf("HP") - 3, 2).Trim();
                    pokemonIVs.Health = Convert.ToByte(health);
                }

                if(ivs.Contains("Atk"))
                {
                    string health = ivs.Substring(ivs.IndexOf("Atk") - 3, 2).Trim();
                    pokemonIVs.Attack = Convert.ToByte(health);
                }

                if(ivs.Contains("Def"))
                {
                    string health = ivs.Substring(ivs.IndexOf("Def") - 3, 2).Trim();
                    pokemonIVs.Defense = Convert.ToByte(health);
                }

                if(ivs.Contains("SpA"))
                {
                    string health = ivs.Substring(ivs.IndexOf("SpA") - 3, 2).Trim();
                    pokemonIVs.SpecialAttack = Convert.ToByte(health);
                }

                if(ivs.Contains("SpD"))
                {
                    string health = ivs.Substring(ivs.IndexOf("SpD") - 3, 2).Trim();
                    pokemonIVs.SpecialDefense = Convert.ToByte(health);
                }

                if(ivs.Contains("Spe"))
                {
                    string health = ivs.Substring(ivs.IndexOf("Spe") - 3, 2).Trim();
                    pokemonIVs.Speed = Convert.ToByte(health);
                }

                pokemonTeamDetail.IVs = pokemonIVs;
            }
            #endregion

            #region Moveset
            if(remainingImportedText.Contains("- "))
            {
                List<string> moves = remainingImportedText.Split("\r\n").ToList();
                List<string> validMoves = new List<string>();
                string move;
                PokemonTeamMoveset moveset = new PokemonTeamMoveset();
                
                foreach(var m in moves)
                {
                    if(!string.IsNullOrEmpty(m) && validMoves.Count() < 4)
                    {
                        validMoves.Add(m);
                    }
                }

                foreach(var m in validMoves)
                {
                    move = m.Substring(2, m.Length - 2).Trim();
                    moveset.FourthMove = move;
                    moveset = this._dataService.SortMoveset(moveset);
                }

                pokemonTeamDetail.Moveset = moveset;
            }
            #endregion

            return pokemonTeamDetail;
        }
    }
}