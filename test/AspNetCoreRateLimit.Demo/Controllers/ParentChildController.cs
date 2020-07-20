using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AspNetCoreRateLimit.Demo.Controllers
{
    [Route("api/[controller]")]
    public class ParentChildController : Controller
    {
        [HttpGet("parent")]
        public string GetParent1() => "parent1";

        [HttpGet("/parent/child")]
        public string GetParentsChild1() => "parent1/child1";
    }
}
