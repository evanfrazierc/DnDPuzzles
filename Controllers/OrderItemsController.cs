﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DndPuzzles.Data;
using DndPuzzles.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; 

namespace DndPuzzles.Controllers
{
  [Route("api/orders/{orderId}/items")]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public class OrderItemsController : Controller
  { 
    private readonly IPuzzleRepository _repository;
    private readonly ILogger<OrderItemsController> _logger;
    private readonly IMapper _mapper;

    public OrderItemsController(IPuzzleRepository repository,
      ILogger<OrderItemsController> logger,
      IMapper mapper)
    {
      _repository = repository;
      _logger = logger;
      _mapper = mapper;
    }

    [HttpGet]
    public IActionResult Get(int orderId)
    {
      var order = _repository.GetOrderById(User.Identity.Name, orderId);
      if (order != null) return Ok(_mapper.Map<IEnumerable<OrderItemViewModel>>(order.Items));
      return NotFound();
    }

    [HttpGet("{id}")]
    public IActionResult Get(int orderId, int id)
    {
      var order = _repository.GetOrderById(User.Identity.Name, orderId);
      if (order != null)
      {
        var item = order.Items.Where(o => o.Id == id).FirstOrDefault();

        if (item != null)
        {
          return Ok(_mapper.Map<OrderItemViewModel>(item));
        }
      }
      return NotFound();
    }
  }
}
