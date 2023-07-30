using System;
using UnityEngine;

public class OrderCounterInteractPresenter
{
    private readonly OrderCounterInteractModel _orderCounterInteractModel;

    public OrderCounterInteractPresenter(OrderCounterInteractModel orderCounterInteractModel)
    {
        _orderCounterInteractModel = orderCounterInteractModel;
    }

    public void Enable()
    {
        _orderCounterInteractModel.OrderApplied += OnOrderApplied;
    }

    public void Interact(PlayerObjectInteract playerObjectInteract)
    {
        _orderCounterInteractModel.Interact(playerObjectInteract);
    }

    private void OnOrderApplied(DishView dish)
    {
        
    }
}
