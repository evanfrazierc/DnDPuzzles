using DnDPuzzles.Data;
using DnDPuzzles.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDPuzzles.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ProductsController : ControllerBase
    {
        private readonly IPuzzleRepository repository;
        private readonly ILogger<ProductsController> logger;

        public ProductsController(IPuzzleRepository repository, ILogger<ProductsController> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult<IEnumerable<Product>> Get()
        {
            try
            {
                return Ok(repository.GetAllProducts());
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to get products: {ex}");
                return BadRequest("Failed to get products");
            }
        }
    }
}
