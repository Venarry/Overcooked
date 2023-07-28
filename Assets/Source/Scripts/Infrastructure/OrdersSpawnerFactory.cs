using System.Collections.Generic;
using UnityEngine;

public class OrdersSpawnerFactory
{
    public OrderSpawner Create(OrdersHandler ordersHandler, List<OrderSO> orders, float spawnInterval)
    {
        OrderSpawner orderSpawner = new GameObject("OrderSpawner").AddComponent<OrderSpawner>();
        orderSpawner.Init(ordersHandler, orders, spawnInterval);
        orderSpawner.Enable();

        return orderSpawner;
    }
}
