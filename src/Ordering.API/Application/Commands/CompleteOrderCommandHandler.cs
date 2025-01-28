public class CompleteOrderCommandHandler : IRequestHandler<CompleteOrderCommand, bool>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderingIntegrationEventService _orderingIntegrationEventService;


    public CompleteOrderCommandHandler(IOrderRepository orderRepository, IOrderingIntegrationEventService orderingIntegrationEventService)
    {
        _orderRepository = orderRepository;
        _orderingIntegrationEventService = orderingIntegrationEventService;
    }

    public async Task<bool> Handle(CompleteOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetAsync(request.OrderId);

        if (order == null)
        {
            throw new KeyNotFoundException($"{request.OrderId} numaralı sipariş bulunamadı!");
        }

        order.SetOrderStatusToComplete();
        await _orderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        var orderCompletedEvent = new OrderCompletedIntegrationEvent(order.Id);
        await _orderingIntegrationEventService.AddAndSaveEventAsync(orderCompletedEvent);

        return true;
    }
}
