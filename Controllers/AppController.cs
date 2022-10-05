using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DndPuzzles.Data;
using DndPuzzles.Services;
using DndPuzzles.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DndPuzzles.Controllers
{
  public class AppController : Controller
  {
    private readonly IMailService _mailService;
    private readonly IPuzzleRepository _repository;

    public AppController(IMailService mailService, IPuzzleRepository repository)
    {
      _mailService = mailService;
      _repository = repository;
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
        // Send the Email
        _mailService.SendMessage("efrazier@angular.com", model.Subject, $"From: {model.Name} - {model.Email}, Message: {model.Message}");
        ViewBag.UserMessage = "Mail Sent...";
        ModelState.Clear();
      }

      return View();
    }

    public IActionResult About()
    {
      return View();
    }

    public IActionResult Shop()
    {
      var results = _repository.GetAllProducts();

      return View(results);
    }
  }
}
