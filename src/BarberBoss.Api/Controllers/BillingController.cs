using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Necessário para usar o ToListAsync()
// using SeuProjeto.Data; // Ajuste para o namespace onde está o seu AppDbContext
using BarberBoss.Infrastructure;
using BarberBoss.Communication.Responses;
using BarberBoss.Application.UseCases.Expenses.GetAll;
namespace BarberBoss.Api.Controllers;

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
    [ProducesResponseType(typeof(ResponseExpenseJson), StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCode.Status)]
    public async Task<IActionResult> GetAllBilling([FromServices] IGetAllExpenseUseCase useCase)
    {
        var response = await useCase.Execute();

        if(response.Expenses.Count != 0)
        {
            return Ok(response);
        }
        return Ok("Lista");
    }
}