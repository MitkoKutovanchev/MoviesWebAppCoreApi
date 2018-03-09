using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Data.DB.Repositories;
using Data.Entity.Entities;
using Microsoft.AspNetCore.Mvc;
using moviesWebApp.Models;
using MoviesWebApp.Models;

namespace moviesWebApp.Controllers
{
    public class HomeController : Controller
    {
        MovieRepository movieRepo = new MovieRepository();

        public IActionResult Index()
        {
            IndexMoviesViewModel moviesVm = new IndexMoviesViewModel();
            moviesVm.moviesForVm = movieRepo.GetAll();
            return View(moviesVm);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
