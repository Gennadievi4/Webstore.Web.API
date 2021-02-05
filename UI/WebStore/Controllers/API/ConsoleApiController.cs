using Microsoft.AspNetCore.Mvc;
using System;

namespace WebStore.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsoleApiController : ControllerBase
    {
        [HttpGet("clear")]
        public void Clear() => Console.Clear();

        [HttpGet("writeline/{str}")]
        public void WriteLine(string str) => Console.WriteLine(str);
    }
}
