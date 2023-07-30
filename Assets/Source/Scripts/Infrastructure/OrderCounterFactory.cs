using UnityEngine;

public class OrderCounterFactory
{
    private readonly OrderCounterView _prefab = Resources.Load<OrderCounterView>(AssetsPath.OrderCounter);
    private readonly OrderCounterBuilder _orderCounterBuilder = new();

    public void Create(OrdersHandler ordersHandler)
    {
        OrderCounterView orderCounterView = Object.Instantiate(_prefab);
        _orderCounterBuilder.Build(orderCounterView, ordersHandler);
    }
}
