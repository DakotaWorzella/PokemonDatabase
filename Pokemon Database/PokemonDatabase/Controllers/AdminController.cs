﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using PokemonDatabase.Models;
using PokemonDatabase.DataAccess.Models;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace PokemonDatabase.Controllers
{
    [Authorize(Roles = "Admin,Owner"), Route("admin")]
    public class AdminController : Controller
    {
        private readonly AppConfig _appConfig;

        private readonly DataService _dataService;

        public AdminController(IOptions<AppConfig> appConfig, DataContext dataContext)
        {
            _dataService = new DataService(dataContext);
            _appConfig = appConfig.Value;
        }

        [Route("")]
        public IActionResult Index()
        {
            ViewBag.ApplicationName = _appConfig.AppName;
            ViewBag.ApplicationVersion = _appConfig.AppVersion;

            return View();
        }

        [Route("pokemon")]
        public IActionResult Pokemon()
        {
            List<PokemonTypeDetail> model = _dataService.GetAllPokemonWithTypes();

            return View(model);
        }

        [Route("generation")]
        public IActionResult Generations()
        {
            GenerationViewModel model = new GenerationViewModel(){
                AllGenerations = _dataService.GetGenerations(),
                AllPokemon = _dataService.GetAllPokemon()
            };

            return View(model);
        }

        [Route("type")]
        public IActionResult Types()
        {
            List<Type> model = _dataService.GetTypes();

            return View(model);
        }

        [Route("ability")]
        public IActionResult Abilities()
        {
            AbilityViewModel model = new AbilityViewModel(){
                AllAbilities = _dataService.GetAbilities(),
                AllPokemon = _dataService.GetAllPokemonWithAbilities()
            };

            return View(model);
        }

        [Route("egggroup")]
        public IActionResult EggGroups()
        {
            EggGroupViewModel model = new EggGroupViewModel(){
                AllEggGroups = _dataService.GetEggGroups(),
                AllPokemon = _dataService.GetAllPokemonWithEggGroups()
            };

            return View(model);
        }

        [HttpGet, Route("add-generation")]
        public IActionResult AddGeneration()
        {
            return View();
        }

        [HttpPost, Route("add-generation")]
        public IActionResult AddGeneration(Generation generation)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _dataService.AddGeneration(generation);

            return RedirectToAction("Generations");
        }

        [HttpGet, Route("add-egg-group")]
        public IActionResult AddEggGroup()
        {
            return View();
        }

        [HttpPost, Route("add-egg-group")]
        public IActionResult AddEggGroup(EggGroup eggGroup)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _dataService.AddEggGroup(eggGroup);

            return RedirectToAction("EggGroups");
        }

        [HttpGet, Route("add-ability")]
        public IActionResult AddAbility()
        {
            return View();
        }

        [HttpPost, Route("add-ability")]
        public IActionResult AddAbility(Ability ability)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _dataService.AddAbility(ability);

            return RedirectToAction("Abilities");
        }

        [HttpGet, Route("edit-generation/{id}")]
        public IActionResult EditGeneration(string id)
        {
            Generation model = _dataService.GetGeneration(id);

            return View(model);
        }

        [HttpPost, Route("edit-generation/{id}")]
        public IActionResult EditGeneration(Generation generation)
        {
            if (!ModelState.IsValid)
            {
                Generation model = _dataService.GetGeneration(generation.Id);

                return View(model);
            }

            _dataService.UpdateGeneration(generation);

            return RedirectToAction("Generations");
        }

        [HttpGet, Route("edit-egg-group/{id:int}")]
        public IActionResult EditEggGroup(int id)
        {
            EggGroup model = _dataService.GetEggGroup(id);

            return View(model);
        }

        [HttpPost, Route("edit-egg-group/{id:int}")]
        public IActionResult EditEggGroup(EggGroup eggGroup)
        {
            if (!ModelState.IsValid)
            {
                EggGroup model = _dataService.GetEggGroup(eggGroup.Id);

                return View(model);
            }

            _dataService.UpdateEggGroup(eggGroup);

            return RedirectToAction("EggGroups");
        }

        [HttpGet, Route("edit-ability/{id:int}")]
        public IActionResult EditAbility(int id)
        {
            Ability model = _dataService.GetAbility(id);

            return View(model);
        }

        [HttpPost, Route("edit-ability/{id:int}")]
        public IActionResult EditAbility(Ability ability)
        {
            if (!ModelState.IsValid)
            {
                Ability model = _dataService.GetAbility(ability.Id);

                return View(model);
            }

            _dataService.UpdateAbility(ability);

            return RedirectToAction("Abilities");
        }

        [HttpGet, Route("delete-generation/{id}")]
        public IActionResult DeleteGeneration(string id)
        {
            Generation model = _dataService.GetGeneration(id);

            return View(model);
        }

        [HttpPost, Route("delete-generation/{id}")]
        public IActionResult DeleteGeneration(Generation generation)
        {
            _dataService.DeleteGeneration(generation.Id);

            return RedirectToAction("Generations");
        }

        [HttpGet, Route("delete-egg-group/{id:int}")]
        public IActionResult DeleteEggGroup(int id)
        {
            EggGroup model = _dataService.GetEggGroup(id);

            return View(model);
        }

        [HttpPost, Route("delete-egg-group/{id:int}")]
        public IActionResult DeleteEggGroup(EggGroup eggGroup)
        {
            _dataService.DeleteEggGroup(eggGroup.Id);

            return RedirectToAction("EggGroups");
        }

        [HttpGet, Route("delete-ability/{id:int}")]
        public IActionResult DeleteAbility(int id)
        {
            Ability model = _dataService.GetAbility(id);

            return View(model);
        }

        [HttpPost, Route("delete-ability/{id:int}")]
        public IActionResult DeleteAbility(Ability ability)
        {
            _dataService.DeleteAbility(ability.Id);

            return RedirectToAction("Abilities");
        }

        [Route("error")]
        public IActionResult Error()
        {
            return View();
        }
    }
}
