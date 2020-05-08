using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace YetAnotherNoteTaker.Server.Controllers
{
    [ApiController]
    [Route("v0/[controller]")]
    public class NotebooksController : ControllerBase
    {
        [HttpGet]
        public List<string> GetNotebooks(string userEmail)
        {
            return new List<string>();
        }
    }
}
