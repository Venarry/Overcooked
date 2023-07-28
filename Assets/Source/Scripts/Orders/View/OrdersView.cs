using System.Collections.Generic;
using UnityEngine;

public class OrdersView
{
    private readonly OrderPanelFactory _orderPanelFactory;
    private readonly Transform _spawnPoint;
    private readonly Dictionary<int, OrderPanelView> _activePanels = new();

    public OrdersView(Transform spawnPoint)
    {
        _orderPanelFactory = new();
        _spawnPoint = spawnPoint;
    }

    public void ShowOrder(int id, OrderSO order)
    {
        OrderPanelView orderPanel = _orderPanelFactory.Create(id, order, _spawnPoint);
        _activePanels.Add(id, orderPanel);
    }

    public void RemoveOrder(int id)
    {
        if (_activePanels.ContainsKey(id) == false)
            return;

        Object.Destroy(_activePanels[id].gameObject);
        _activePanels.Remove(id);
    }
}
