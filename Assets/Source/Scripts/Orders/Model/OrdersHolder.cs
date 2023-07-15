using System;
using System.Collections.Generic;

public class OrdersHolder
{
    private readonly Dictionary<int, OrderSO> _orders = new();
    private int _ordersCounter;

    public event Action<KeyValuePair<int, OrderSO>> OrderAdded;
    public event Action<int> OrderRemoved;

    public void AddOrder(OrderSO order)
    {
        _orders.Add(_ordersCounter, order);
        OrderAdded?.Invoke(new KeyValuePair<int, OrderSO>(_ordersCounter, order));
        _ordersCounter++;
    }

    public bool TryApplyOrder(KitchenObjectType[] inputIngredients)
    {
        foreach (KeyValuePair<int, OrderSO> order in _orders)
        {
            if(order.Value.CorrectOrder(inputIngredients))
            {
                _orders.Remove(order.Key);
                OrderRemoved?.Invoke(order.Key);

                return true;
            }
        }

        return false;
    }
}
