using System.Collections.Generic;
using UnityEngine;

public class OrdersSetup : MonoBehaviour
{
    [SerializeField] private List<OrderSO> _orders;
    [SerializeField] private OrderSpawner _orderSpawner;
    [SerializeField] private KitchenOrderCounter[] _kitchenOrderCounters;
    [SerializeField] private Transform _orderPanelSpawnPoint;
    [SerializeField] private float _orderSpawninterval;

    private OrdersHolder _ordersHolder;
    private OrdersView _ordersView;
    private OrdersPresenter _ordersPresenter;

    public void Create()
    {
        _ordersHolder = new();
        _ordersView = new(_orderPanelSpawnPoint);
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
