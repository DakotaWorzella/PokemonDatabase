using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using Pokedex.DataAccess.Models;

using Pokedex.Models;

namespace Pokedex.Controllers
{
    [Authorize(Roles = "Admin,Owner")]
    [Route("admin")]
    public class AdminController : Controller
    {
        private readonly DataService _dataService;

        private readonly AppConfig _appConfig;

        public AdminController(IOptions<AppConfig> appConfig, DataContext dataContext)
        {
            this._appConfig = appConfig.Value;
            this._dataService = new DataService(dataContext);
        }

        [Route("pokemon")]
        public IActionResult Pokemon()
        {
            List<string> model = this._dataService.GetGenerations().Select(x => x.Id).Where(x => x.IndexOf('-') < 0).OrderBy(x => x).ToList();

            return this.View(model);
        }

        [Route("generation")]
        public IActionResult Generations()
        {
            GenerationViewModel model = new GenerationViewModel()
            {
                AllGenerations = this._dataService.GetGenerations(),
                AllPokemon = this._dataService.GetAllPokemon(),
            };

            return this.View(model);
        }

        [Route("type")]
        public IActionResult Types()
        {
            TypeViewModel model = new TypeViewModel()
            {
                AllTypes = this._dataService.GetTypes(),
                AllPokemon = this._dataService.GetAllPokemonWithTypesAndIncomplete(),
            };

            return this.View(model);
        }

        [Route("form_items")]
        public IActionResult FormItems()
        {
            List<FormItem> model = this._dataService.GetFormItems().OrderBy(x => x.Name).ToList();

            return this.View(model);
        }

        [Route("ability")]
        public IActionResult Abilities()
        {
            AbilityViewModel model = new AbilityViewModel()
            {
                AllAbilities = this._dataService.GetAbilities(),
                AllPokemon = this._dataService.GetAllPokemonWithAbilitiesAndIncomplete(),
            };

            return this.View(model);
        }

        [Route("legendary_type")]
        public IActionResult LegendaryTypes()
        {
            LegendaryTypeViewModel model = new LegendaryTypeViewModel()
            {
                AllLegendaryTypes = this._dataService.GetLegendaryTypes(),
                AllPokemon = this._dataService.GetAllPokemonWithLegendaryTypesAndIncomplete(),
            };

            return this.View(model);
        }

        [Route("form")]
        public IActionResult Forms()
        {
            FormViewModel model = new FormViewModel()
            {
                AllForms = this._dataService.GetForms(),
                AllPokemon = this._dataService.GetPokemonFormDetails(),
            };

            return this.View(model);
        }

        [Route("egg_group")]
        public IActionResult EggGroups()
        {
            EggGroupViewModel model = new EggGroupViewModel()
            {
                AllEggGroups = this._dataService.GetEggGroups(),
                AllPokemon = this._dataService.GetAllPokemonWithEggGroupsAndIncomplete(),
            };

            return this.View(model);
        }

        [Route("evolution_methods")]
        public IActionResult EvolutionMethods()
        {
            EvolutionMethodViewModel model = new EvolutionMethodViewModel()
            {
                AllEvolutionMethods = this._dataService.GetEvolutionMethods(),
                AllEvolutions = this._dataService.GetEvolutions(),
            };

            return this.View(model);
        }

        [Route("base_happinesses")]
        public IActionResult BaseHappinesses()
        {
            BaseHappinessViewModel model = new BaseHappinessViewModel()
            {
                AllBaseHappinesses = this._dataService.GetBaseHappinesses(),
                AllPokemon = this._dataService.GetAllPokemonIncludeIncomplete(),
            };

            return this.View(model);
        }

        [Route("classification")]
        public IActionResult Classifications()
        {
            ClassificationViewModel model = new ClassificationViewModel()
            {
                AllClassifications = this._dataService.GetClassifications(),
                AllPokemon = this._dataService.GetAllPokemonWithClassificationsAndIncomplete(),
            };

            return this.View(model);
        }

        [Route("game_availability/{pokemonId}")]
        public IActionResult PokemonGameDetails(string pokemonId)
        {
            Pokemon pokemon = this._dataService.GetPokemonById(pokemonId);
            if(pokemonId.Contains('-'))
            {
                pokemon.Name += " (" + this._dataService.GetFormByAltFormId(pokemonId).Name + ")";
            }

            PokemonGameViewModel model = new PokemonGameViewModel()
            {
                Pokemon = pokemon,
                PokemonGameDetails = this._dataService.GetPokemonGameDetails(pokemonId),
                AllGenerations = this._dataService.GetGenerations().Where(x => x.ReleaseDate >= pokemon.Generation.ReleaseDate).ToList(),
            };

            return this.View(model);
        }

        [Route("available_pokemon")]
        public IActionResult AvailablePokemon()
        {
            List<Generation> model = this._dataService.GetGenerations();

            return this.View(model);
        }

        [Route("nature")]
        public IActionResult Natures()
        {
            List<Nature> model = this._dataService.GetNatures();

            return this.View(model);
        }

        [Route("shiny_hunting_technique")]
        public IActionResult ShinyHuntingTechniques()
        {
            ShinyHuntViewModel model = new ShinyHuntViewModel()
            {
                AllShinyHunters = this._dataService.GetShinyHunters(),
                AllShinyHuntingTechniques = this._dataService.GetShinyHuntingTechniques(),
            };

            return this.View(model);
        }

        [Route("battle_item")]
        public IActionResult BattleItems()
        {
            List<Pokemon> pokemonList = this._dataService.GetAllPokemon();
            foreach(var p in pokemonList.Where(x => x.Id.Contains('-')))
            {
                p.Name += " (" + this._dataService.GetFormByAltFormId(p.Id).Name + ")";
            }

            BattleItemViewModel model = new BattleItemViewModel()
            {
                AllBattleItems = this._dataService.GetBattleItems(),
                AllPokemonTeamDetails = this._dataService.GetPokemonTeamDetails(),
                AllPokemon = pokemonList,
            };

            return this.View(model);
        }
    }
}
