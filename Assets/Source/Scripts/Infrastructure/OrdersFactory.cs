using UnityEngine;

public class OrdersFactory
{
    public OrdersView Create(OrdersHandler ordersHandler, LevelMoneyView levelMoneyView, Transform spawnPoint)
    {
        OrdersView ordersView = new(spawnPoint);
        OrdersPresenter ordersPresenter = new(ordersView, ordersHandler, levelMoneyView);
        ordersPresenter.Enable();

        return ordersView;
    }
}
