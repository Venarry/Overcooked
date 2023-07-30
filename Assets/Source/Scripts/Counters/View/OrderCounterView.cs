using System.Collections.Generic;
using UnityEngine;

public class OrderCounterView : MonoBehaviour, IInteractable
{
    private OrderCounterInteractPresenter _orderCounterPresenter;

    public bool CanInteract => true;

    public void Init(OrderCounterInteractPresenter orderCounterPresenter)
    {
        _orderCounterPresenter = orderCounterPresenter;
    }

    public void Interact(PlayerObjectInteract objectInteractSystem)
    {
        _orderCounterPresenter.Interact(objectInteractSystem);
    }
}
