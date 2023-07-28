public class OrdersPresenter
{
    private readonly OrdersView _ordersView;
    private readonly OrdersHandler _ordersHolder;
    private readonly LevelMoneyView _levelMoneyView;

    public OrdersPresenter(OrdersView ordersView, OrdersHandler ordersHolder, LevelMoneyView levelMoneyView)
    {
        _ordersView = ordersView;
        _ordersHolder = ordersHolder;
        _levelMoneyView = levelMoneyView;
    }

    public void Enable()
    {
        _ordersHolder.OrderAdded += _ordersView.ShowOrder;
        _ordersHolder.OrderApplied += OnOrderApplied;
    }


    public void Disable()
    {
        _ordersHolder.OrderAdded -= _ordersView.ShowOrder;
        _ordersHolder.OrderApplied -= OnOrderApplied;
    }

    private void OnOrderApplied(int orderKey)
    {
        _ordersView.RemoveOrder(orderKey);
    }
}
