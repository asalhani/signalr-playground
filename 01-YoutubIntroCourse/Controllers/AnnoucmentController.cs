using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalrDemo;

namespace _01_YoutubIntroCourse.Controllers
{
    public class AnnoucmentController : Controller
    {
        private readonly IHubContext<MessageHub> _hubContext;

        public AnnoucmentController(IHubContext<MessageHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpGet("/annoucment")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("/annoucment")]
        public async Task<IActionResult> Post([FromForm] string message)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", message);
            return RedirectToAction("Index");
        }

    }
}