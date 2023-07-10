using System.Collections.Generic;
using UnityEngine;

public class CookingStoveCounter : MonoBehaviour, IInteractable
{
    [SerializeField] private KitchenObjectType _type;
    [SerializeField] private Transform _holdPoint;
    private ICookable _cookable;

    public bool CanInteract => true;

    private void FixedUpdate()
    {
        Cook();
    }

    public void Interact(PlayerObjectInteract objectInteractSystem)
    {
        if (TryGetCookable(objectInteractSystem))
            return;

        TryInteractWithCookable(objectInteractSystem);
    }

    private bool TryGetCookable(PlayerObjectInteract objectInteractSystem)
    {
        if (_cookable == null && objectInteractSystem.HasPickable)
        {
            if (objectInteractSystem.TryGetPickableType(out ICookable cookable) == false)
                return false;

            if (objectInteractSystem.CanPlacePickable(_type) == false)
                return false;

            objectInteractSystem.TryTakePickable(out IPickable pickable);

            _cookable = cookable;
            pickable.SetParent(_holdPoint);
            return true;
        }

        return false;
    }

    private bool TryInteractWithCookable(PlayerObjectInteract objectInteractSystem)
    {
        if (_cookable == null)
            return false;
        
        if (objectInteractSystem.HasPickable)
        {
            if(_cookable is IInteractable interactable)
            {
                interactable.Interact(objectInteractSystem);
                return true;
            }
        }
        else
        {
            if(_cookable is IPickable pickable)
            {
                if (objectInteractSystem.TryGivePickable(pickable))
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
