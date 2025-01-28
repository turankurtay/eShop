using eShop.Ordering.API.Application.IntegrationEvents;
using eShop.Ordering.Domain.AggregatesModel.OrderAggregate;

namespace eShop.Ordering.UnitTests.Application;

[TestClass]
public class CompleteOrderCommandHandlerTest
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<CompleteOrderCommandHandler> _logger;
    private readonly IOrderingIntegrationEventService _orderingIntegrationEventService;


    public CompleteOrderCommandHandlerTest(IOrderingIntegrationEventService orderingIntegrationEventService)
    {
        _orderRepository = Substitute.For<IOrderRepository>();
        _logger = Substitute.For<ILogger<CompleteOrderCommandHandler>>();
        _orderingIntegrationEventService = orderingIntegrationEventService;
    }

    [TestMethod]
    public async Task Handler_throws_exception_if_order_not_found()
    {
        var fakeOrderId = 999;
        var command = new CompleteOrderCommand(fakeOrderId);

        _orderRepository.GetAsync(fakeOrderId)
            .Returns(Task.FromResult<Order>(null));

        var handler = new CompleteOrderCommandHandler(_orderRepository, _orderingIntegrationEventService);

        // Act & Assert
        await Assert.ThrowsExceptionAsync<KeyNotFoundException>(() => handler.Handle(command, CancellationToken.None));
    }

    //[TestMethod]
    //public async Task Handler_completes_order_successfully()
    //{
    //    var fakeOrderId = 1;
    //    //Order protected tanımlı olduğu için oraya müdahele etmedim.
    //    //Reflection kullanarak düzenlenebilir.
    //    var fakeOrder = new Order();
    //    fakeOrder.SetOrderStatus(OrderStatus.Shipped);

    //    _orderRepository.GetAsync(fakeOrderId)
    //        .Returns(Task.FromResult(fakeOrder));

    //    var command = new CompleteOrderCommand(fakeOrderId);
    //    var handler = new CompleteOrderCommandHandler(_orderRepository, _orderingIntegrationEventService);

    //    var result = await handler.Handle(command, CancellationToken.None);

    //    Assert.IsTrue(result);
    //    Assert.AreEqual(OrderStatus.Complete, fakeOrder.OrderStatus);
    //    await _orderRepository.UnitOfWork.Received(1).SaveEntitiesAsync(CancellationToken.None);
    //}

}
