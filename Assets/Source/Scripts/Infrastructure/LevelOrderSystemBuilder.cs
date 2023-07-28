using System.Collections.Generic;
using UnityEngine;

public class LevelOrderSystemBuilder : MonoBehaviour
{
    [SerializeField] private List<OrderSO> _orders;
    [SerializeField] private OrderCounterView[] _kitchenOrderCounters;
    [SerializeField] private Transform _orderPanelSpawnPoint;
    [SerializeField] private float _orderSpawninterval;

    public void Build(LevelMoneyFactory levelMoneyFactory, OrdersFactory ordersFactory, OrdersSpawnerFactory ordersSpawnerFactory)
    {
        LevelMoneyModel levelMoneyModel = new();

        LevelMoneyView levelMoneyView = levelMoneyFactory.Create(levelMoneyModel);
        levelMoneyView.gameObject.transform.SetParent(transform, false);

        OrdersHandler ordersHandler = new(levelMoneyModel);

        ordersFactory.Create(ordersHandler, levelMoneyView, _orderPanelSpawnPoint);
        ordersSpawnerFactory.Create(ordersHandler, _orders, _orderSpawninterval);

        foreach (OrderCounterView counter in _kitchenOrderCounters)
        {
            counter.Init(ordersHandler);
        }
    }
}
