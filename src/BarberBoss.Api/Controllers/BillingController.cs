using Microsoft.AspNetCore.Mvc;

namespace BillingController.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BillingController : ControllerBase
{
    [HttpPost]
    public IActionResult Register()
    {
        return Ok("Cadastro");
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Lista");
    }
}