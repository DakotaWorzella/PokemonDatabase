﻿using System;
using System.Collections.Generic;
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

namespace PokemonDatabase.Controllers
{
    [Route("admin")]
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
            List<PokemonTypeDetail> model = _dataService.GetPokemonWithTypes();

            return View(model);
        }

        [Route("generation")]
        public IActionResult Generations()
        {
            List<Generation> model = _dataService.GetGeneration();

            return View(model);
        }

        [Route("error")]
        public IActionResult Error()
        {
            return View();
        }
    }
}
