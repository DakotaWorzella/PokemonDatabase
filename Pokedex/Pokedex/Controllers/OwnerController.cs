using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Pokedex.DataAccess.Models;

namespace Pokedex.Controllers
{
    [Authorize(Roles = "Owner")]
    [Route("admin")]
    public class OwnerController : Controller
    {
        private readonly DataService _dataService;

        public OwnerController(DataContext dataContext)
        {
            // Instantiate an instance of the data service.
            this._dataService = new DataService(dataContext);
        }

        [Route("users")]
        public IActionResult Users()
        {
            List<User> model = this._dataService.GetUsers();

            return this.View(model);
        }

        [Route("suggestions")]
        public IActionResult Suggestions()
        {
            List<Suggestion> model = this._dataService.GetSuggestions();

            return this.View("Suggestions", model);
        }

        [HttpGet]
        [Route("complete_suggestion/{id:int}")]
        public IActionResult CompleteSuggestion(int id)
        {
            Suggestion model = this._dataService.GetSuggestion(id);

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("complete_suggestion/{id:int}")]
        public IActionResult CompleteSuggestion(Suggestion suggestion)
        {
            suggestion.IsCompleted = true;

            this._dataService.UpdateSuggestion(suggestion);

            return this.RedirectToAction("Suggestions", "Owner");
        }

        [HttpGet]
        [Route("add_update")]
        public IActionResult Update()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("add_update")]
        public IActionResult Update(Update update)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            update.DateCreated = DateTime.Today;

            this._dataService.AddUpdate(update);

            return this.RedirectToAction("Index", "Home");
        }

        [Route("complete_pokemon/{pokemonId}")]
        public IActionResult CompletePokemon(string pokemonId)
        {
            // Ensuring that the pokemon really has all of these added.
            bool PokemonIsComplete = this._dataService.GetAllPokemonWithTypesAndIncomplete().Exists(x => x.PokemonId == pokemonId) &&
                   this._dataService.GetAllPokemonWithAbilitiesAndIncomplete().Exists(x => x.PokemonId == pokemonId) &&
                   this._dataService.GetAllPokemonWithEggGroupsAndIncomplete().Exists(x => x.PokemonId == pokemonId) &&
                   this._dataService.GetBaseStatsWithIncomplete().Exists(x => x.PokemonId == pokemonId) &&
                   this._dataService.GetEVYieldsWithIncomplete().Exists(x => x.PokemonId == pokemonId);

            Pokemon pokemon = this._dataService.GetPokemonByIdNoIncludes(pokemonId);
            pokemon.IsComplete = true;
            this._dataService.UpdatePokemon(pokemon);    
            
            return this.RedirectToAction("Pokemon", "Admin");
        }
    }
}