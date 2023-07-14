using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderFactory : MonoBehaviour
{
    private OrdersHolder _ordersHolder;
    private WaitForSeconds _waitForSeconds;
    private Queue<OrderSO> _ordersInLevel;

    public void Init(OrdersHolder ordersHolder, Queue<OrderSO> ordersInLevel, float spawnInterval)
    {
        if (spawnInterval < 1)
            spawnInterval = 1;

        _ordersHolder = ordersHolder;
        _ordersInLevel = ordersInLevel;
        _waitForSeconds = new WaitForSeconds(spawnInterval);
    }

    public void Enable()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        for (int i = 0; i < _ordersInLevel.Count; i++)
        {
            _ordersHolder.AddOrder(_ordersInLevel.Dequeue());

            yield return _waitForSeconds;
        }
    }
}
