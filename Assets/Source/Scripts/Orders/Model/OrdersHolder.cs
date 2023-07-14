using System;
using System.Collections.Generic;
using UnityEngine;

public class OrdersHolder
{
    private readonly List<OrderSO> _orders = new();

    public event Action<OrderSO> OrderAdded;

    public void AddOrder(OrderSO order)
    {
        _orders.Add(order);
        OrderAdded?.Invoke(order);
    }

    public bool TryApplyOrder(KitchenObjectType[] inputIngredients)
    {
        foreach (OrderSO order in _orders)
        {
            if(order.CorrectOrder(inputIngredients))
            {
                _orders.Remove(order);
                return true;
            }
        }
        Debug.Log("asd");
        return false;
    }
}
