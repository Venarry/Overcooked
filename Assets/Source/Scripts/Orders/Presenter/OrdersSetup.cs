using System.Collections.Generic;
using UnityEngine;

public class OrdersSetup : MonoBehaviour
{
    [SerializeField] private List<OrderSO> _orders;
    [SerializeField] private KitchenOrderCounter[] _kitchenOrderCounters;
    [SerializeField] private Transform _orderPanelSpawnPoint;
    [SerializeField] private float _orderSpawninterval;

    public void Setup()
    {
        LevelMoneyFactory levelMoneyFactory = new();
        LevelMoneyModel levelMoneyModel = new();
        LevelMoneyView levelMoneyView = levelMoneyFactory.Create(levelMoneyModel);
        levelMoneyView.Enable();
        levelMoneyView.gameObject.transform.SetParent(transform, false);

        OrdersHandler ordersHandler = new(levelMoneyModel);
        OrdersView ordersView = new(_orderPanelSpawnPoint);
        OrdersPresenter ordersPresenter = new(ordersView, ordersHandler, levelMoneyView);
        ordersPresenter.Enable();

        OrderSpawner orderSpawner = new GameObject("OrderSpawner").AddComponent<OrderSpawner>();
        orderSpawner.Init(ordersHandler, _orders, _orderSpawninterval);
        orderSpawner.Enable();

        foreach (KitchenOrderCounter counter in _kitchenOrderCounters)
        {
            counter.Init(ordersHandler);
        }
    }
}
