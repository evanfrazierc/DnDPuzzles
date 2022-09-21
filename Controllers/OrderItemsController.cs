using AutoMapper;
using DnDPuzzles.Data;
using DnDPuzzles.Data.Entities;
using DnDPuzzles.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDPuzzles.Controllers
{
    [Route("/api/orders/{orderid}/items")]
    public class OrderItemsController : Controller
    {
        private readonly IPuzzleRepository repository;
        private readonly ILogger<OrderItemsController> logger;
        private readonly IMapper mapper;

        public OrderItemsController(IPuzzleRepository repository, 
            ILogger<OrderItemsController> logger,
            IMapper mapper)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get(int orderId)
        {
            var order = repository.GetOrderbyId(orderId);
            if (order != null) return Ok(mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemViewModel>>(order.Items));
            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int orderId, int id)
        {
            var order = repository.GetOrderbyId(orderId);
            if (order != null)
            {
                var item = order.Items.Where(i => i.Id == id).FirstOrDefault();
                if (item != null)
                {
                    return Ok(mapper.Map<OrderItem, OrderItemViewModel>(item));
                }
            }
            return NotFound();
        }
    }
}
