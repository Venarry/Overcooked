using System.Collections.Generic;
using UnityEngine;

public class OrdersView
{
    private readonly OrderPanel _orderPanel;
    private readonly Transform _spawnPoint;
    private readonly Dictionary<int, OrderPanel> _activePanels = new();

    public OrdersView(Transform spawnPoint)
    {
        _orderPanel = Resources.Load<OrderPanel>(AssetsPath.OrderPanel);
        _spawnPoint = spawnPoint;
    }

    public void Show(KeyValuePair<int, OrderSO> order)
    {
        OrderPanel newOrderPanel = CreateNewOrderPanel(order.Key.ToString());
        _activePanels.Add(order.Key, newOrderPanel);

        newOrderPanel.SetOrderImage(order.Value.OrderImage);
        newOrderPanel.SetIngredientsImages(order.Value.IngredientTextures);
    }

    public void Remove(int orderNumber)
    {
        if (_activePanels.ContainsKey(orderNumber) == false)
            return;

        Object.Destroy(_activePanels[orderNumber].gameObject);
        _activePanels.Remove(orderNumber);
    }

    private OrderPanel CreateNewOrderPanel(string objectName)
    {
        OrderPanel orderPanel = Object.Instantiate(_orderPanel);
        orderPanel.gameObject.name = $"Order {objectName}";
        orderPanel.transform.SetParent(_spawnPoint.transform);
        orderPanel.transform.localScale = new(1, 1, 1);

        return orderPanel;
    }
}
