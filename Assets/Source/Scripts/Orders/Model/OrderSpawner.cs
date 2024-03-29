using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderSpawner : MonoBehaviour
{
    private OrdersHandler _ordersHolder;
    private Queue<OrderSO> _ordersInLevel;
    private WaitForSeconds _waitForSeconds;

    public void Init(OrdersHandler ordersHolder, List<OrderSO> ordersInLevel, float spawnInterval)
    {
        if (spawnInterval < 1)
            spawnInterval = 1;

        _ordersHolder = ordersHolder;
        _ordersInLevel = new Queue<OrderSO>(ordersInLevel);
        _waitForSeconds = new WaitForSeconds(spawnInterval);
    }

    public void Enable()
    {
        StartCoroutine(Spawn());
    }

    public void Disable()
    {
        StopCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        int ordersCount = _ordersInLevel.Count;

        for (int i = 0; i < ordersCount; i++)
        {
            yield return _waitForSeconds;
            _ordersHolder.AddOrder(_ordersInLevel.Dequeue());
        }
    }
}
