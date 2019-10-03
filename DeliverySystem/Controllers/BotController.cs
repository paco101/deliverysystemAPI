using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;

namespace DeliverySystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BotController : ControllerBase
    {
        [HttpGet]
        public async Task<OkResult> Update([FromBody] Update update)
        {
            
            return Ok();
        }
    }
}