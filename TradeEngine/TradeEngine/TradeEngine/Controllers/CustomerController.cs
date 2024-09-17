using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using TradeEngine.DataContracts;
using TradeEngine.Model;
using TradeEngine.Repositories;

namespace TradeEngine.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CustomerController : Controller
{
    private readonly ICustomerRepository _repository;

    public CustomerController(ICustomerRepository repository)
    {
        _repository = repository;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCustomer customer, CancellationToken cancellationToken)
    {
        var cust = new Customer { Name = customer.Name  };
       await _repository.Create(cust, cancellationToken);

        return Ok(cust.Id);
    }
}