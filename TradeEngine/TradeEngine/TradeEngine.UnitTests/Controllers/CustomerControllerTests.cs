using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using TradeEngine.Controllers;
using TradeEngine.DataContracts;
using TradeEngine.Repositories;

namespace TradeEngine.UnitTests.Controllers;

[TestFixture]
public class CustomerControllerTests
{
    private readonly CancellationToken _cancellationToken = CancellationToken.None;
    private CustomerController _controller = null!;

    [SetUp]
    public void SetUp()
    {
        _controller = new CustomerController(new CustomerRepository());
    }

    [Test]
    public async Task GivenCustomerDetails_WhenCreateCalled_ThenReturnsOk()
    {
        var customer = new CreateCustomer { Name = "Phil" };
        var result = await _controller.Create(customer, _cancellationToken);

        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public async Task GivenCustomerDetails_WhenCreateCalledMultipleTimes_ThenReturnsDifferentCustomerIds()
    {
        var customer = new CreateCustomer { Name = "Phil" };
        var result1 = await _controller.Create(customer, _cancellationToken);
        var result2 = await _controller.Create(customer, _cancellationToken);

        var customerId1 = (OkObjectResult)result1;
        var customerId2 = (OkObjectResult)result2;
        Assert.That(customerId1.Value, Is.Not.EqualTo(customerId2.Value));
    }
}