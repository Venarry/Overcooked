using System.Collections.Generic;
using UnityEngine;

public class CookingStoveCounter : MonoBehaviour, IInteractable
{
    [SerializeField] private KitchenObjectType _type;
    [SerializeField] private Transform _holdPoint;
    private ICookable _cookable;

    private void FixedUpdate()
    {
        Cook();
    }

    public void Interact(InteractSystem interactSystem)
    {
        if (TryGetCookable(interactSystem))
            return;

        TryInteractWithCookable(interactSystem);
    }

    private bool TryGetCookable(InteractSystem interactSystem)
    {
        if (_cookable == null && interactSystem.HasPickable)
        {
            if (interactSystem.TryGetPickableType(out ICookable cookable) == false)
                return false;

            if (interactSystem.CanPlacePickable(_type) == false)
                return false;

            interactSystem.TryTakePickable(out IPickable pickable);

            _cookable = cookable;
            pickable.SetParent(_holdPoint);
            return true;
        }

        return false;
    }

    private bool TryInteractWithCookable(InteractSystem interactSystem)
    {
        if (_cookable == null)
            return false;
        
        if (interactSystem.HasPickable)
        {
            if(_cookable is IInteractable interactable)
            {
                interactable.Interact(interactSystem);
                return true;
            }
        }
        else
        {
            if(_cookable is IPickable pickable)
            {
                if (interactSystem.TryGivePickable(pickable))
                {
                    _cookable = null;
                    return true;
                }
            }
        }

        return false;
    }

    private void Cook(float step = 0)
    {
        if (_cookable == null)
            return;

        _cookable.Cook(step);
    }
}
