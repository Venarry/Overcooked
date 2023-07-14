using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private List<OrderSO> _orders;
    [SerializeField] private OrderFactory _orderFactory;
    [SerializeField] private float _orderSpawninterval;

    private void Awake()
    {
        Queue<OrderSO> ordersQueue = new Queue<OrderSO>(_orders);
        OrdersHolder ordersHolder = new OrdersHolder();
        OrdersView ordersView = new OrdersView(ordersHolder);

        _orderFactory.Init(ordersHolder, ordersQueue, _orderSpawninterval);
        _orderFactory.Enable();
    }
}
