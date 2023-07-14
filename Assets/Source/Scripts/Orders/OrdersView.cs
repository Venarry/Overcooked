using UnityEngine;

public class OrdersView
{
    private OrdersHolder _ordersHolder;

    public OrdersView(OrdersHolder ordersHolder)
    {
        ordersHolder.OrderAdded += Show;
    }

    private void Show(OrderSO order)
    {
        Debug.Log(order.OrderTexture.name);
    }
}
