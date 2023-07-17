using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrdersPresenter
{
    private OrdersView _ordersView;
    private OrdersHolder _ordersHolder;

    public OrdersPresenter(OrdersView ordersView, OrdersHolder ordersHolder)
    {
        _ordersView = ordersView;
        _ordersHolder = ordersHolder;
    }

    public void Enable()
    {
        _ordersHolder.OrderAdded += _ordersView.Show;
        _ordersHolder.OrderRemoved += _ordersView.Remove;
    }

    public void Disable()
    {
        _ordersHolder.OrderAdded -= _ordersView.Show;
        _ordersHolder.OrderRemoved -= _ordersView.Remove;
    }
}
