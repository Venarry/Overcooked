using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private List<OrderSO> _orders;
    [SerializeField] private OrderFactory _orderFactory;
    [SerializeField] private float _orderSpawninterval;
    [SerializeField] private KitchenOrderCounter _kitchenOrderCounter;

    private void Awake()
    {
        Queue<OrderSO> ordersQueue = new(_orders);
        OrdersHolder ordersHolder = new();
        OrdersView ordersView = new(ordersHolder);
        _kitchenOrderCounter.Init(ordersHolder);

        _orderFactory.Init(ordersHolder, ordersQueue, _orderSpawninterval);
        _orderFactory.Enable();
    }
}
