using System.Collections.Generic;
using UnityEngine;

public class OrdersView
{
    private readonly OrderPanel _orderPanel;
    private readonly Transform _spawnPoint;
    private readonly Dictionary<int, OrderPanel> _activePanels = new();

    public OrdersView(Transform spawnPoint)
    {
        _orderPanel = Resources.Load<OrderPanel>("OrderPanel");
        _spawnPoint = spawnPoint;
    }

    public void Add(KeyValuePair<int, OrderSO> order)
    {
        OrderPanel orderPanel = Object.Instantiate(_orderPanel);
        orderPanel.gameObject.name = $"Order {order.Key}";
        orderPanel.transform.SetParent(_spawnPoint.transform);
        orderPanel.transform.localScale = new(1, 1, 1);

        _activePanels.Add(order.Key, orderPanel);

        orderPanel.SetOrderImage(order.Value.OrderImage);
        orderPanel.SetIngredientsImages(order.Value.IngredientTextures);
    }

    public void Remove(int orderNumber)
    {
        if (_activePanels.ContainsKey(orderNumber) == false)
            return;

        Object.Destroy(_activePanels[orderNumber].gameObject);
        _activePanels.Remove(orderNumber);
    }
}
