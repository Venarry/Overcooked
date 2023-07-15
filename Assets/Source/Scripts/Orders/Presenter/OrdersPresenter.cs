using System.Collections.Generic;
using UnityEngine;

public class OrdersPresenter : MonoBehaviour
{
    [SerializeField] private List<OrderSO> _orders;
    [SerializeField] private OrderSpawner _orderSpawner;
    [SerializeField] private KitchenOrderCounter[] _kitchenOrderCounters;
    [SerializeField] private float _orderSpawninterval;
    [SerializeField] private Transform _point;

    private OrdersHolder _ordersHolder;
    private OrdersView _ordersView;

    public void Init()
    {
        _ordersHolder = new();
        _ordersView = new(_point);
        _ordersHolder.OrderAdded += _ordersView.Add;
        _ordersHolder.OrderRemoved += _ordersView.Remove;

        foreach (KitchenOrderCounter counter in _kitchenOrderCounters)
        {
            counter.Init(_ordersHolder);
        }

        _orderSpawner.Init(_ordersHolder, _orders, _orderSpawninterval);
        _orderSpawner.Enable();
    }

    public void Disable()
    {
        _ordersHolder.OrderAdded -= _ordersView.Add;
        _ordersHolder.OrderRemoved -= _ordersView.Remove;
        _orderSpawner.Disable();
    }
}
