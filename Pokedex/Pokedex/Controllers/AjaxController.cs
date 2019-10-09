﻿using System;
using System.Collections.Generic;
using System.Globalization;
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
    public class AjaxController : Controller
    {
        private readonly DataService _dataService;

        private readonly AppConfig _appConfig;

        public AjaxController(IOptions<AppConfig> appConfig, DataContext dataContext)
        {
            this._appConfig = appConfig.Value;
            this._dataService = new DataService(dataContext);
        }

        [AllowAnonymous]
        [Route("grab-all-user-pokemon-teams")]
        public List<ExportPokemonViewModel> ExportAllUserPokemonTeams(int pokemonTeamId)
        {
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                List<PokemonTeam> pokemonTeams = this._dataService.GetAllPokemonTeams(User.Identity.Name);
                List<ExportPokemonViewModel> exportList = new List<ExportPokemonViewModel>();
                foreach(var team in pokemonTeams)
                {
                    if(team.FirstPokemon != null)
                    {
                        ExportPokemonViewModel pokemonTeam = new ExportPokemonViewModel(){
                            ExportString = "=== " + team.PokemonTeamName + " ===\n\n",
                            TeamId = team.Id,
                        };

                        pokemonTeam.ExportString += this.FillUserPokemonTeam(team.FirstPokemon);
                        if(team.SecondPokemon != null)
                        {
                            pokemonTeam.ExportString += "\n\n" + this.FillUserPokemonTeam(team.SecondPokemon);
                        }

                        if(team.ThirdPokemon != null)
                        {
                            pokemonTeam.ExportString += "\n\n" + this.FillUserPokemonTeam(team.ThirdPokemon);
                        }

                        if(team.FourthPokemon != null)
                        {
                            pokemonTeam.ExportString += "\n\n" + this.FillUserPokemonTeam(team.FourthPokemon);
                        }

                        if(team.FifthPokemon != null)
                        {
                            pokemonTeam.ExportString += "\n\n" + this.FillUserPokemonTeam(team.FifthPokemon);
                        }

                        if(team.SixthPokemon != null)
                        {
                            pokemonTeam.ExportString += "\n\n" + this.FillUserPokemonTeam(team.SixthPokemon);
                        }

                        exportList.Add(pokemonTeam);
                    }
                }

                return exportList;
            }
            else
            {
                this.RedirectToAction("Home", "Index");
            }

            return null;
        }

        [AllowAnonymous]
        [Route("fill-user-pokemon-team")]
        public string FillUserPokemonTeam(PokemonTeamDetail pokemonTeamDetail)
        {
            Pokemon pokemon = this._dataService.GetPokemonById(pokemonTeamDetail.PokemonId);
            string pokemonName;
            if(!string.IsNullOrEmpty(pokemonTeamDetail.Nickname))
            {
                pokemonName = pokemonTeamDetail.Nickname + " (" + pokemon.Name + ")";
            }
            else
            {
                pokemonName = pokemon.Name;
            }

            if(!string.IsNullOrEmpty(pokemonTeamDetail.Gender))
            {
                pokemonName += " (" + pokemonTeamDetail.Gender.Substring(0,1) + ")";
            }

            if(pokemon.Id.Contains('-'))
            {
                string pokemonForm = this.GetUserFormDetails(pokemon.Id);
                pokemonName += "-" + pokemonForm;
            }

            string pokemonTeamString = pokemonName;
            pokemonTeamString += "\nAbility: " + pokemonTeamDetail.Ability.Name;
            if(pokemonTeamDetail.IsShiny)
            {
                pokemonTeamString += "\nShiny: Yes";
            }
            
            if(pokemonTeamDetail.PokemonTeamEV != null)
            {
                pokemonTeamString += this.FillEVs(pokemonTeamDetail.PokemonTeamEV);
            }
            
            if(pokemonTeamDetail.PokemonTeamIV != null)
            {
                pokemonTeamString += this.FillIVs(pokemonTeamDetail.PokemonTeamIV);
            }

            return pokemonTeamString;
        }

        private string FillEVs(PokemonTeamEV evs)
        {
            string evString = string.Empty;
            if(evs.Health > 0)
            {
                evString += evs.Health.ToString() + " HP";
            }

            if(evs.Attack > 0)
            {
                if(!string.IsNullOrEmpty(evString))
                {
                    evString += " / ";
                }
                
                evString += evs.Attack.ToString() + " Atk";
            }

            if(evs.Defense > 0)
            {
                if(!string.IsNullOrEmpty(evString))
                {
                    evString += " / ";
                }
                
                evString += evs.Defense.ToString() + " Def";
            }

            if(evs.SpecialAttack > 0)
            {
                if(!string.IsNullOrEmpty(evString))
                {
                    evString += " / ";
                }
                
                evString += evs.SpecialAttack.ToString() + " SpA";
            }

            if(evs.SpecialDefense > 0)
            {
                if(!string.IsNullOrEmpty(evString))
                {
                    evString += " / ";
                }
                
                evString += evs.SpecialDefense.ToString() + " SpD";
            }

            if(evs.Speed > 0)
            {
                if(!string.IsNullOrEmpty(evString))
                {
                    evString += " / ";
                }
                
                evString += evs.Speed.ToString() + " Spe";
            }

            if(!string.IsNullOrEmpty(evString))
            {
                evString = "\nEVs: " + evString;
            }

            return evString;
        }

        private string FillIVs(PokemonTeamIV ivs)
        {
            string ivString = string.Empty;
            if(ivs.Health < 31)
            {
                ivString += ivs.Health.ToString() + " HP";
            }

            if(ivs.Attack < 31)
            {
                if(!string.IsNullOrEmpty(ivString))
                {
                    ivString += " / ";
                }
                
                ivString += ivs.Attack.ToString() + " Atk";
            }

            if(ivs.Defense < 31)
            {
                if(!string.IsNullOrEmpty(ivString))
                {
                    ivString += " / ";
                }
                
                ivString += ivs.Defense.ToString() + " Def";
            }

            if(ivs.SpecialAttack < 31)
            {
                if(!string.IsNullOrEmpty(ivString))
                {
                    ivString += " / ";
                }
                
                ivString += ivs.SpecialAttack.ToString() + " SpA";
            }

            if(ivs.SpecialDefense < 31)
            {
                if(!string.IsNullOrEmpty(ivString))
                {
                    ivString += " / ";
                }
                
                ivString += ivs.SpecialDefense.ToString() + " SpD";
            }

            if(ivs.Speed < 31)
            {
                if(!string.IsNullOrEmpty(ivString))
                {
                    ivString += " / ";
                }
                
                ivString += ivs.Speed.ToString() + " Spe";
            }

            if(!string.IsNullOrEmpty(ivString))
            {
                ivString = "\nIVs: " + ivString;
            }

            return ivString;
        }

        [AllowAnonymous]
        [Route("get-user-form-item")]
        public string GetUserFormDetails(string pokemonId)
        {
            string formDetails = string.Empty, itemName = string.Empty;
            PokemonFormDetail pokemonFormDetail;

             pokemonFormDetail = this._dataService.GetPokemonFormDetailByAltFormId(pokemonId);

            formDetails += pokemonFormDetail.Form.Name.Replace(' ', '-');
            
            FormItem formItem = this._dataService.GetFormItemByPokemonId(pokemonId);
            if(formItem != null)
            {
                itemName = formItem.Name;
            }
            else if(formDetails.Contains("Mega") && pokemonFormDetail.AltFormPokemonId != "384-1")
            {
                itemName = "[Insert Mega Stone Here]";
            }

            if(!string.IsNullOrEmpty(itemName))
            {
                formDetails += " @ " + itemName;
            }

            return formDetails;
        }

        [AllowAnonymous]
        [Route("save-pokemon-team")]
        public string SavePokemonTeam(string pokemonTeamName, List<string> pokemonIdList, List<int> abilityIdList, bool exportAbilities, string necrozmaOriginalId, string zygardeOriginalId)
        {
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                if(this._dataService.GetUserWithUsername(this.User.Identity.Name) != null)
                {
                    PokemonTeam pokemonTeam= new PokemonTeam(){
                        PokemonTeamName = pokemonTeamName,
                        UserId = this._dataService.GetUserWithUsername(this.User.Identity.Name).Id,
                    };

                    Pokemon pokemon;
                    Ability ability;
                    PokemonTeamDetail pokemonTeamDetail;

                    for(var i = 0; i < pokemonIdList.Count; i++)
                    {
                        if(pokemonIdList[i] == "718-2")
                        {
                            pokemon = this._dataService.GetPokemonById(zygardeOriginalId);
                        }
                        else if(pokemonIdList[i] == "800-3")
                        {
                            pokemon = this._dataService.GetPokemonById(necrozmaOriginalId);
                        }
                        else
                        {
                            pokemon = this._dataService.GetPokemonById(pokemonIdList[i]);   
                        }

                        ability = (pokemonIdList[i] == "800-3") ? this._dataService.GetAbility(34) : this._dataService.GetAbility(abilityIdList[i]);

                        pokemonTeamDetail = new PokemonTeamDetail()
                        {
                            PokemonId = pokemon.Id,
                            AbilityId = ability.Id,
                        };

                        this._dataService.AddPokemonTeamDetail(pokemonTeamDetail);

                        pokemonTeam.InsertPokemon(pokemonTeamDetail);
                    }

                    this._dataService.AddPokemonTeam(pokemonTeam);

                    return "Team \"" + pokemonTeam.PokemonTeamName + "\" has been added successfully!";
                }
                else
                {
                    return "You must be logged in to save a team.";
                }
            }
            else
            {
                this.RedirectToAction("Home", "Index");
            }

            return null;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("get-pokemon-list")]
        public List<Pokemon> GetPokemonList()
        {
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                List<Pokemon> pokemonList = this._dataService.GetAllPokemonWithoutForms();

                foreach(var pokemon in pokemonList)
                {
                    if (pokemon.Name.Contains("type null"))
                    {
                        pokemon.Name = "Type: Null";
                    }

                    pokemon.Name = pokemon.Name.Replace('_', ' ');

                    TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                    pokemon.Name = textInfo.ToTitleCase(pokemon.Name);

                    if (pokemon.Name.Contains("-O") && pokemon.Name.Substring(pokemon.Name.Length - 2, 2) == "-O")
                    {
                        pokemon.Name = pokemon.Name.Remove(pokemon.Name.Length - 2, 2) + "-o";
                    }
                }

                return pokemonList;
            }
            else
            {
                this.RedirectToAction("Error");
            }

            return null;
        }

        [AllowAnonymous]
        [Route("get-pokemon-by-generation/{generationId}")]
        public IActionResult GetPokemonByGeneration(string generationId)
        {
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                GenerationTableViewModel model = new GenerationTableViewModel()
                {
                    PokemonList = this._dataService.GetAllPokemonWithTypes().Where(x => x.Pokemon.GenerationId == generationId || x.Pokemon.GenerationId.Contains(generationId + '-')).ToList(),
                    AppConfig = _appConfig,
                };

                return this.PartialView("_FillGenerationTable", model);
            }
            else
            {
                return this.RedirectToAction("Home", "Index");
            }
        }

        [AllowAnonymous]
        [Route("export-pokemon-team")]
        public string ExportPokemonTeam(List<string> pokemonIdList, List<string> abilityList, bool exportAbilities, string necrozmaOriginalId, string zygardeOriginalId)
        {
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                string pokemonTeam = string.Empty, pokemonName = string.Empty, pokemonForm = string.Empty;
                Pokemon pokemon;

                for(var i = 0; i < pokemonIdList.Count; i++)
                {
                    if (i != 0)
                    {
                        pokemonTeam += "\n";
                    }

                    pokemon = this._dataService.GetPokemonById(pokemonIdList[i]);
                    pokemonName = pokemon.Name;
                    if(pokemon.Id.Contains('-') && !(pokemonIdList[i] == "718-2" && zygardeOriginalId == "718"))
                    {
                        pokemonForm = this.GetFormDetails(pokemon.Id, necrozmaOriginalId, zygardeOriginalId);
                        pokemonName += "-" + pokemonForm;
                    }

                    pokemonTeam += pokemonName + "\n";
                    if(exportAbilities)
                    {
                        pokemonTeam += "Ability: " + ((pokemonIdList[i] == "800-3") ? this._dataService.GetAbility(34).Name : abilityList[i]) + "\n";
                    }
                }

                return pokemonTeam;
            }
            else
            {
                this.RedirectToAction("Home", "Index");
            }

            return null;
        }

        [AllowAnonymous]
        [Route("get-form-item")]
        public string GetFormDetails(string pokemonId, string necrozmaOriginalId, string zygardeOriginalId)
        {
            string formDetails = string.Empty, itemName = string.Empty;
            PokemonFormDetail pokemonFormDetail;

            if(pokemonId == "800-3")
            {
                pokemonFormDetail = this._dataService.GetPokemonFormDetailByAltFormId(necrozmaOriginalId);
            }
            else if (pokemonId == "718-2")
            {
                pokemonFormDetail = this._dataService.GetPokemonFormDetailByAltFormId(zygardeOriginalId);
            }
            else
            {
                pokemonFormDetail = this._dataService.GetPokemonFormDetailByAltFormId(pokemonId);
            }

            formDetails += pokemonFormDetail.Form.Name.Replace(' ', '-');
            
            FormItem formItem = this._dataService.GetFormItemByPokemonId(pokemonId);
            if(formItem != null)
            {
                itemName = formItem.Name;
            }
            else if(formDetails.Contains("Mega") && pokemonFormDetail.AltFormPokemonId != "384-1")
            {
                itemName = "[Insert Mega Stone Here]";
            }

            if(!string.IsNullOrEmpty(itemName))
            {
                formDetails += " @ " + itemName;
            }

            return formDetails;
        }

        [AllowAnonymous]
        [Route("get-pokemon-team")]
        public TeamRandomizerViewModel GetPokemonTeam(List<string> selectedGens, List<string> selectedLegendaries, List<string> selectedForms, string selectedEvolutions, bool onlyLegendaries, bool onlyAltForms, bool multipleMegas, bool onePokemonForm, bool randomAbility)
        {
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                List<Generation> unselectedGens = this._dataService.GetGenerations().Where(x => !x.Id.Contains('-')).ToList();
                foreach(var item in selectedGens)
                {
                    unselectedGens.Remove(unselectedGens.Find(x => x.Id == item));
                }

                Pokemon pokemon;
                TeamRandomizerViewModel model = new TeamRandomizerViewModel(){
                    AllPokemonChangedNames = new List<Pokemon>(),
                    AllPokemonOriginalNames = new List<Pokemon>(),
                    PokemonAbilities = new List<Ability>(),
                };
                List<Pokemon> pokemonList = new List<Pokemon>();
                List<Pokemon> allPokemon = this._dataService.GetAllPokemonWithoutForms();
                List<Evolution> allEvolutions = this._dataService.GetEvolutions();
                Random rnd = new Random();

                foreach(var gen in unselectedGens)
                {
                    allPokemon = allPokemon.Except(allPokemon.Where(x => (x.GenerationId == gen.Id) || (x.GenerationId.IndexOf('-') > -1 && x.GenerationId.Substring(0, x.GenerationId.IndexOf('-')) == gen.Id)).ToList()).ToList();
                }

                if (selectedEvolutions == "stage1Pokemon")
                {
                    List<Pokemon> newPokemon = new List<Pokemon>();
                    foreach(var p in allPokemon)
                    {
                        if (allEvolutions.Exists(x => x.PreevolutionPokemonId == p.Id) && !allEvolutions.Exists(x => x.EvolutionPokemonId == p.Id))
                        {
                            newPokemon.Add(p);
                        }
                    }

                    allPokemon = newPokemon;
                }
                else if (selectedEvolutions == "middleEvolution")
                {
                    List<Pokemon> newPokemon = new List<Pokemon>();
                    foreach(var p in allPokemon)
                    {
                        if (allEvolutions.Exists(x => x.PreevolutionPokemonId == p.Id) && allEvolutions.Exists(x => x.EvolutionPokemonId == p.Id))
                        {
                            newPokemon.Add(p);
                        }
                    }

                    allPokemon = newPokemon;
                }
                else if (selectedEvolutions == "onlyFullyEvolved")
                {
                    List<Pokemon> newPokemon = new List<Pokemon>();
                    foreach(var p in allPokemon)
                    {
                        if (!allEvolutions.Exists(x => x.PreevolutionPokemonId == p.Id))
                        {
                            newPokemon.Add(p);
                        }
                    }

                    allPokemon = newPokemon;
                }

                if(selectedLegendaries.Count() == 0)
                {
                    List<PokemonLegendaryDetail> legendaryList = this._dataService.GetAllPokemonWithLegendaryTypes();

                    foreach(var p in legendaryList)
                    {
                        allPokemon.Remove(allPokemon.FirstOrDefault(x => x.Id == p.PokemonId));
                    }
                }
                else
                {
                    if(!selectedLegendaries.Contains("Legendary"))
                    {
                        List<PokemonLegendaryDetail> legendaryList = this._dataService.GetAllPokemonWithLegendaryTypes().Where(x => x.LegendaryType.Type == "Legendary").ToList();

                        foreach(var l in legendaryList)
                        {
                            allPokemon.Remove(allPokemon.FirstOrDefault(x => x.Id == l.PokemonId));
                        }
                    }
                    if(!selectedLegendaries.Contains("Mythical"))
                    {
                        List<PokemonLegendaryDetail> legendaryList = this._dataService.GetAllPokemonWithLegendaryTypes().Where(x => x.LegendaryType.Type == "Mythical").ToList();

                        foreach(var l in legendaryList)
                        {
                            allPokemon.Remove(allPokemon.FirstOrDefault(x => x.Id == l.PokemonId));
                        }
                    }
                    if(!selectedLegendaries.Contains("UltraBeast"))
                    {
                        List<PokemonLegendaryDetail> legendaryList = this._dataService.GetAllPokemonWithLegendaryTypes().Where(x => x.LegendaryType.Type == "Ultra Beast").ToList();

                        foreach(var l in legendaryList)
                        {
                            allPokemon.Remove(allPokemon.FirstOrDefault(x => x.Id == l.PokemonId));
                        }
                    }
                }

                if(onlyLegendaries)
                {
                    List<Pokemon> legendaryList = new List<Pokemon>();
                    List<PokemonLegendaryDetail> allLegendaries = this._dataService.GetAllPokemonWithLegendaryTypes();

                    foreach(var p in allLegendaries)
                    {
                        if(allPokemon.Exists(x => x.Id == p.PokemonId))
                        {
                            legendaryList.Add(p.Pokemon);
                        }
                    }

                    allPokemon = legendaryList;
                }

                if(selectedForms.Count() != 0)
                {
                    List<Pokemon> altForms = new List<Pokemon>();

                    if (selectedForms.Contains("Mega"))
                    {  
                        List<PokemonFormDetail> pokemonFormList = this._dataService.GetAllAltFormsOnlyComplete().Where(x => x.Form.Id == 9 || x.Form.Id == 10 || x.Form.Id == 11).ToList();

                        List<PokemonFormDetail> filteredFormList = new List<PokemonFormDetail>();

                        foreach(var p in allPokemon)
                        {
                            List<PokemonFormDetail> altForm = pokemonFormList.Where(x => x.OriginalPokemonId == p.Id).ToList();
                            foreach(var a in altForm)
                            {
                                a.AltFormPokemon.Name += " (" + a.Form.Name + ")";
                            }

                            if(altForm.Count() > 0)
                            {
                                filteredFormList.AddRange(altForm);
                            }
                        }

                        if (filteredFormList.Count() > 0)
                        {
                            foreach(var p in filteredFormList)
                            {
                                altForms.Add(p.AltFormPokemon);
                            }
                        }
                    }

                    if (selectedForms.Contains("Alolan"))
                    {
                        List<PokemonFormDetail> pokemonFormList = this._dataService.GetAllAltFormsOnlyComplete().Where(x => x.Form.Id == 21).ToList();

                        List<PokemonFormDetail> filteredFormList = new List<PokemonFormDetail>();

                        foreach(var p in allPokemon)
                        {
                            List<PokemonFormDetail> altForm = pokemonFormList.Where(x => x.OriginalPokemonId == p.Id).ToList();
                            foreach(var a in altForm)
                            {
                                a.AltFormPokemon.Name += " (" + a.Form.Name + ")";
                            }

                            if(altForm.Count() > 0)
                            {
                                filteredFormList.AddRange(altForm);
                            }
                        }

                        if (filteredFormList.Count() > 0)
                        {
                            foreach(var p in filteredFormList)
                            {
                                altForms.Add(p.AltFormPokemon);
                            }
                        }
                    }

                    if (selectedForms.Contains("Galarian"))
                    {
                        List<PokemonFormDetail> pokemonFormList = this._dataService.GetAllAltFormsOnlyComplete().Where(x => x.Form.Id == 1001).ToList();

                        List<PokemonFormDetail> filteredFormList = new List<PokemonFormDetail>();

                        foreach(var p in allPokemon)
                        {
                            List<PokemonFormDetail> altForm = pokemonFormList.Where(x => x.OriginalPokemonId == p.Id).ToList();
                            foreach(var a in altForm)
                            {
                                a.AltFormPokemon.Name += " (" + a.Form.Name + ")";
                            }

                            if(altForm.Count() > 0)
                            {
                                filteredFormList.AddRange(altForm);
                            }
                        }

                        if (filteredFormList.Count() > 0)
                        {
                            foreach(var p in filteredFormList)
                            {
                                altForms.Add(p.AltFormPokemon);
                            }
                        }
                    }

                    if (selectedForms.Contains("Other"))
                    {
                        List<PokemonFormDetail> pokemonFormList = this._dataService.GetAllAltFormsOnlyComplete();

                        List<Form> formsToRemove = this.GatherRemovableForms();

                        foreach(var f in formsToRemove)
                        {
                            pokemonFormList = pokemonFormList.Where(x => x.Form.Name != f.Name).ToList();
                        }

                        List<PokemonFormDetail> filteredFormList = new List<PokemonFormDetail>();

                        foreach(var p in allPokemon)
                        {
                            List<PokemonFormDetail> altForm = pokemonFormList.Where(x => x.OriginalPokemonId == p.Id).ToList();
                            foreach(var a in altForm)
                            {
                                a.AltFormPokemon.Name += " (" + a.Form.Name + ")";
                            }

                            if(altForm.Count() > 0)
                            {
                                filteredFormList.AddRange(altForm);
                            }
                        }

                        if (filteredFormList.Count() > 0)
                        {
                            foreach(var p in filteredFormList)
                            {
                                altForms.Add(p.AltFormPokemon);
                            }
                        }
                    }

                    allPokemon.AddRange(altForms);

                    allPokemon = this.RemoveExtraPokemonForms(allPokemon);
                }

                if (onlyAltForms)
                {
                    allPokemon = allPokemon.Where(x => x.Id.Contains('-')).ToList();
                }
                
                if (!multipleMegas)
                {
                    List<Pokemon> megaList = new List<Pokemon>();
                    List<PokemonFormDetail> altFormList = this._dataService.GetAllAltForms();
                    foreach(var p in altFormList.Where(x => x.Form.Id == 9
                                                         || x.Form.Id == 10
                                                         || x.Form.Id == 11).ToList())
                    {
                        if(allPokemon.Exists(x => x.Id == p.AltFormPokemonId))
                        {
                            megaList.Add(p.AltFormPokemon);
                        }
                    }

                    if(megaList.Count > 0)
                    {
                        Pokemon mega = megaList[rnd.Next(megaList.Count)];
                        foreach(var p in megaList.Where(x => x.Id != mega.Id))
                        {
                            if (allPokemon.Exists(x => x.Id == p.Id))
                            {
                                allPokemon.Remove(allPokemon.Find(x => x.Id == p.Id));
                            }
                        }
                    }
                }

                if(allPokemon.Count() > 0)
                {
                    for (var i = 0; i < 6; i++)
                    {
                        if (pokemonList.Count() >= allPokemon.Count())
                        {
                            break;
                        }

                        pokemon = allPokemon[rnd.Next(allPokemon.Count)];
                        while (pokemonList.Contains(pokemon))
                        {
                            pokemon = allPokemon[rnd.Next(allPokemon.Count)];
                        }

                        if (onePokemonForm)
                        {
                            string originalPokemonId;
                            if (pokemon.Id.Contains('-'))
                            {
                                originalPokemonId = pokemon.Id.Substring(0, pokemon.Id.IndexOf('-'));
                            }
                            else
                            {
                                originalPokemonId = pokemon.Id;
                            }

                            List<Pokemon> altForms = this._dataService.GetAltForms(originalPokemonId);

                            if (pokemon.Id.Contains('-'))
                            {
                                altForms.Remove(altForms.Find(x => x.Id == pokemon.Id));
                                altForms.Add(this._dataService.GetPokemonById(originalPokemonId));
                            }

                            foreach(var p in altForms)
                            {
                                if (allPokemon.Exists(x => x.Id == p.Id))
                                {
                                    allPokemon.Remove(allPokemon.Find(x => x.Id == p.Id));
                                }
                            }
                        }

                        pokemonList.Add(pokemon);
                    }
                }

                model.AllPokemonChangedNames = pokemonList;
                foreach(var p in pokemonList)
                {
                    model.AllPokemonOriginalNames.Add(this._dataService.GetPokemonById(p.Id));
                }

                model.PokemonURLs = new List<string>();
                foreach(var p in model.AllPokemonOriginalNames)
                {
                    model.PokemonURLs.Add(this.Url.Action("Pokemon", "Home", new { name = p.Name.Replace(": ", "_").Replace(' ', '_').ToLower() }));
                }

                foreach(var p in model.AllPokemonOriginalNames)
                {
                    List<Ability> abilities = new List<Ability>();
                    PokemonAbilityDetail pokemonAbilities = this._dataService.GetPokemonWithAbilities(p.Id);
                    abilities.Add(pokemonAbilities.PrimaryAbility);
                    if(pokemonAbilities.SecondaryAbility != null)
                    {
                        abilities.Add(pokemonAbilities.SecondaryAbility);
                    }

                    if(pokemonAbilities.HiddenAbility != null)
                    {
                        abilities.Add(pokemonAbilities.HiddenAbility);
                    }

                    if(p.Id == "744")
                    {
                        abilities.Add(this._dataService.GetAbility(174));
                    }

                    if(p.Id == "718" || p.Id == "718-1")
                    {
                        model.PokemonAbilities.Add(abilities[0]);
                    }
                    else
                    {
                        model.PokemonAbilities.Add(abilities[rnd.Next(abilities.Count)]);
                    }
                }

                return model;
            }
            else
            {
                this.RedirectToAction("Home", "Index");
            }

            return null;
        }

        [AllowAnonymous]
        [Route("gather-removable-forms")]
        public List<Form> GatherRemovableForms()
        {
            List<Form> forms = new List<Form>();

            forms.Add(this._dataService.GetForm(9));
            forms.Add(this._dataService.GetForm(10));
            forms.Add(this._dataService.GetForm(11));
            forms.Add(this._dataService.GetForm(21));
            forms.Add(this._dataService.GetForm(1001));
            forms.Add(this._dataService.GetForm(34));
            forms.Add(this._dataService.GetForm(35));
            forms.Add(this._dataService.GetForm(33));
            forms.Add(this._dataService.GetForm(47));
            forms.Add(this._dataService.GetForm(13));
            forms.Add(this._dataService.GetForm(46));
            forms.Add(this._dataService.GetForm(45));
            forms.Add(this._dataService.GetForm(44));
            forms.Add(this._dataService.GetForm(29));
            forms.Add(this._dataService.GetForm(22));

            return forms;
        }

        [AllowAnonymous]
        [Route("remove-extra-pokemon-forms")]
        public List<Pokemon> RemoveExtraPokemonForms(List<Pokemon> pokemonList)
        {
            Random rnd = new Random();
            List<Pokemon> pumpkabooCount = pokemonList.Where(x => x.Id.StartsWith("710")).ToList();
            while(pumpkabooCount.Count() > 1)
            {
                pokemonList.Remove(pumpkabooCount[rnd.Next(pumpkabooCount.Count)]);
                pumpkabooCount = pokemonList.Where(x => x.Id.StartsWith("710")).ToList();
            }

            List<Pokemon> gourgeistCount = pokemonList.Where(x => x.Id.StartsWith("711")).ToList();
            while(gourgeistCount.Count() > 1)
            {
                pokemonList.Remove(gourgeistCount[rnd.Next(gourgeistCount.Count)]);
                gourgeistCount = pokemonList.Where(x => x.Id.StartsWith("711")).ToList();
            }

            return pokemonList;
        }

        [AllowAnonymous]
        [Route("get-pokemon-abilities")]
        public List<Ability> GetPokemonAbilities(string pokemonId)
        {
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return this._dataService.GetAbilitiesForPokemon(pokemonId);
            }
            else
            {
                this.RedirectToAction("Home", "Index");
            }

            return null;
        }

        [AllowAnonymous]
        [Route("get-typing-effectiveness")]
        public TypeEffectivenessViewModel GetTypingTypeChart(int primaryTypeId, int secondaryTypeId)
        {
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return this._dataService.GetTypeChartTyping(primaryTypeId, secondaryTypeId);
            }
            else
            {
                this.RedirectToAction("Home", "Index");
            }

            return null;
        }

        [AllowAnonymous]
        [Route("get-pokemon-by-typing")]
        public TypingEvaluatorViewModel GetPokemon(int primaryTypeId, int secondaryTypeId)
        {
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                List<PokemonTypeDetail> typingList = this._dataService.GetAllPokemonWithSpecificTypes(primaryTypeId, secondaryTypeId);
                List<Pokemon> pokemonList = new List<Pokemon>();

                foreach(var p in typingList)
                {
                    if(p.PokemonId.Contains('-'))
                    {
                        Pokemon pokemon = this._dataService.GetAltFormWithFormName(p.PokemonId);
                        pokemonList.Add(pokemon);
                    }
                    else
                    {
                        pokemonList.Add(p.Pokemon);
                    }
                }

                TypingEvaluatorViewModel model = new TypingEvaluatorViewModel(){
                    AllPokemonWithTypes = typingList,
                    AllPokemon = pokemonList,
                };

                return model;
            }
            else
            {
                this.RedirectToAction("Home", "Index");
            }

            return null;
        }
    }
}
