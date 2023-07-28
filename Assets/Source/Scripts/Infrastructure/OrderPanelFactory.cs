using System.Collections.Generic;
using UnityEngine;

public class OrderPanelFactory
{
    private readonly OrderPanelView _orderPanel = Resources.Load<OrderPanelView>(AssetsPath.OrderPanel);

    public OrderPanelView Create(int id, OrderSO order, Transform parent)
    {
        OrderPanelView orderPanel = Object.Instantiate(_orderPanel);
        orderPanel.gameObject.name = $"Order {id}";
        orderPanel.SetParent(parent);
        orderPanel.SetSprites(order.OrderImage, order.IngredientTextures);

        return orderPanel;
    }
}
