using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace YetAnotherNoteTaker.Server.Controllers
{
    [ApiController]
    [Route("v0/users/{email}/notebooks/{notebookKey}/notes")]
    public class NotesController : ControllerBase
    {
    }
}
