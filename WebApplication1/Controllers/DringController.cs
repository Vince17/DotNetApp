using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("dring")]
    public class DringController : ControllerBase
    {

        private readonly IHubContext<Hubs.ChatHub> _chatHub;
        private readonly ILogger<DringController> _logger;

        public DringController(ILogger<DringController> logger, IHubContext<Hubs.ChatHub> chatHub)
        {
            _logger = logger;
            _chatHub = chatHub;
        }

        [HttpGet]
        public async Task<string> GetAsync()
        {
            string message = DateTime.Now.ToString() + ("dring dring");
            await _chatHub.Clients.All.SendAsync("ReceiveDring", message);
            return "dring test";
        }
    }
}
