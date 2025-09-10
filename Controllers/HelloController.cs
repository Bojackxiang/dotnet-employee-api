using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementApi.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class HelloController : ControllerBase
  {
    [HttpGet]
    public IActionResult Get()
    {
      return Ok("it should works");
    }
  }
}
