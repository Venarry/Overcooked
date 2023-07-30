using System;

public class OrderCounterInteractModel
{
    private readonly OrdersHandler _ordersHandler;

    public event Action<DishView> OrderApplied;

    public OrderCounterInteractModel(OrdersHandler ordersHandler)
    {
        _ordersHandler = ordersHandler;
    }

    public void Interact(PlayerObjectInteract objectInteractSystem)
    {
        if (objectInteractSystem.TryGetPickableType(out DishView dish) == false)
            return;

        if (_ordersHandler.TryApplyOrder(dish.IngredientsType) == false)
            return;

        dish.Hide();
        objectInteractSystem.RemovePickableRoot();

        OrderApplied?.Invoke(dish);
    }
}
