using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderCounterBuilder
{
    public void Build(OrderCounterView orderCounterView, OrdersHandler ordersHandler)
    {
        OrderCounterInteractModel orderCounterInteractModel = new(ordersHandler);
        OrderCounterInteractPresenter orderCounterInteractPresenter = new(orderCounterInteractModel);

        orderCounterView.Init(orderCounterInteractPresenter);
    }
}
