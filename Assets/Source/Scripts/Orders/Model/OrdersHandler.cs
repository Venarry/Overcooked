using System;
using System.Collections.Generic;

public class OrdersHandler
{
    private readonly Dictionary<int, OrderSO> _orders = new();
    private readonly LevelMoneyModel _levelMoneyModel;

    private int _ordersCounter;

    public event Action<int, OrderSO> OrderAdded;
    public event Action<int> OrderApplied;

    public OrdersHandler(LevelMoneyModel levelMoneyModel)
    {
        _levelMoneyModel = levelMoneyModel;
    }

    public void Interact(PlayerObjectInteract objectInteractSystem)
    {
        if (objectInteractSystem.TryGetPickableType(out IServiceHolder serviceHolder) == false)
            return;

        if (TryApplyOrder(serviceHolder.IngredientsType) == false)
            return;

        serviceHolder.Hilde();
        objectInteractSystem.RemovePickableRoot();
    }

    public void AddOrder(OrderSO order)
    {
        _orders.Add(_ordersCounter, order);
        OrderAdded?.Invoke(_ordersCounter, order);
        _ordersCounter++;
    }

    public bool TryApplyOrder(KitchenObjectType[] inputIngredients)
    {
        foreach (KeyValuePair<int, OrderSO> order in _orders)
        {
            if(order.Value.CorrectOrder(inputIngredients))
            {
                _orders.Remove(order.Key);
                OrderApplied?.Invoke(order.Key);
                _levelMoneyModel.AddMoney(order.Value.Reward);

                return true;
            }
        }

        return false;
    }
}
