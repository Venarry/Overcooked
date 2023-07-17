using System.Collections.Generic;
using UnityEngine;

public class OrdersFactory : MonoBehaviour
{
    [SerializeField] private List<OrderSO> _orders;
    [SerializeField] private OrderSpawner _orderSpawner;
    [SerializeField] private KitchenOrderCounter[] _kitchenOrderCounters;
    [SerializeField] private float _orderSpawninterval;
    [SerializeField] private Transform _point;

    private OrdersHolder _ordersHolder;
    private OrdersView _ordersView;
    private OrdersPresenter _ordersPresenter;

    public void Create()
    {
        _ordersHolder = new();
        _ordersView = new(_point);
        _ordersPresenter = new OrdersPresenter(_ordersView, _ordersHolder);
        _orderSpawner.Init(_ordersHolder, _orders, _orderSpawninterval); // можно создавать с 0

        foreach (KitchenOrderCounter counter in _kitchenOrderCounters)
        {
            counter.Init(_ordersHolder);
        }
    }

    public void Enable()
    {
        _orderSpawner.Enable();
        _ordersPresenter.Enable();
    }

    public void Disable()
    {
        _orderSpawner.Disable();
        _ordersPresenter.Disable();
    }
}
