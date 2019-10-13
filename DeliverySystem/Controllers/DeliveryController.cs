using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliverySystem.Bot.Commands;
using DeliverySystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeliverySystem.Controllers
{
    [ApiController]
    public class DeliveryController : Controller
    {
        private ApiDbContext _dbContext;

        public DeliveryController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("api/system/orders/{status}")]
        public async Task<IEnumerable<DeliveryOrder>> GetDeliveryOrders(int status)
        {
            return await _dbContext.DeliveryOrders.Where(c => c.Status == status).ToListAsync();
        }

        [HttpGet]
        [Route("api/system/couriers/{stockId}")]
        public async Task<IEnumerable<Courier>> GetCouriers(int stockId)
        {
            return await _dbContext.Couriers.Where(c => c.StockId == stockId).ToListAsync();
        }

        [HttpGet]
        [Route("api/system/stocks")]
        public async Task<IEnumerable<Stock>> GetStocks()
        {
            return await _dbContext.Stocks.ToListAsync();
        }

        [HttpPost]
        [Route("api/system/adddeliveryorder")]
        public async Task<IActionResult> AddDeliveryOrder([FromBody] DeliveryOrder deliveryOrder)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _dbContext.DeliveryOrders.AddAsync(deliveryOrder);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        [Route("api/system/addcourier")]
        public async Task<IActionResult> AddCourier([FromBody] Courier courier)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _dbContext.Couriers.AddAsync(courier);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        [Route("api/system/addstock")]
        public async Task<IActionResult> AddStock([FromBody] Stock stock)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _dbContext.Stocks.AddAsync(stock);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        [Route("api/system/acceptpackagereceiving/{userId}")]
        public async Task<IActionResult> AcceptReceiving(int courierId)
        {
            var courier = _dbContext.Couriers.FirstOrDefault(c => c.Id == courierId);
            
            if (courier == null)
                return BadRequest("Courier not found");
            if (courier.Status != 2)
                return BadRequest("Courier is not waiting for packages");

            await WorkCommand.AcceptPackageReceiving(_dbContext, courierId);

            return Ok();
        }
    }
}