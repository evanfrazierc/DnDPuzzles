using DnDPuzzles.Data;
using DnDPuzzles.Services;
using DnDPuzzles.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDPuzzles.Controllers
{
    public class AppController : Controller
    {
        private readonly IMailService mailService;
        private readonly IPuzzleRepository repository;

        public AppController(IMailService mailService, IPuzzleRepository repository)
        {
            this.mailService = mailService;
            this.repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                // send email
                mailService.SendMessage("e.frazier@nextech.com", model.Subject, model.Message);
                ViewBag.UserMessage = "Mail Sent";
                ModelState.Clear();
            }

            return View();
        }

        public IActionResult About()
        {
            ViewBag.Title = "About Us";

            return View();
        }

        public IActionResult Shop()
        {
            var results = repository.GetAllProducts();
            return View(results);
        }
    }
}
