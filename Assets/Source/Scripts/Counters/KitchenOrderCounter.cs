using System.Collections.Generic;
using UnityEngine;

public class KitchenOrderCounter : MonoBehaviour, IInteractable
{
    private OrdersHolder _ordersHolder;

    public bool CanInteract => true;

    public void Init(OrdersHolder ordersHolder)
    {
        _ordersHolder = ordersHolder;
    }

    public void Interact(PlayerObjectInteract objectInteractSystem)
    {
        if (objectInteractSystem.TryGetPickableType(out IServiceHolder serviceHolder) == false)
            return;

        if (_ordersHolder.TryApplyOrder(serviceHolder.IngredientsType) == false)
            return;

        serviceHolder.RemoveObject();
        objectInteractSystem.RemovePickableRoot();
    }
}
