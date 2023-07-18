using System;
using UnityEngine;

public class CounterInteractModel<T> : IInteractable
{
    private readonly Transform _holdPoint;
    private readonly KitchenObjectType _type;
    private T _pickedItem;

    public event Action<ICookable> CookableSet;
    public event Action CookableRemoved;

    public CounterInteractModel(Transform holdPoint, KitchenObjectType type)
    {
        _holdPoint = holdPoint;
        _type = type;
    }

    public bool CanInteract => true;

    public void Interact(PlayerObjectInteract objectInteractSystem)
    {
        if (TryGetPickable(objectInteractSystem))
            return;

        TryInteractWithPickable(objectInteractSystem);
    }

    private bool TryGetPickable(PlayerObjectInteract objectInteractSystem)
    {
        if (_pickedItem != null || objectInteractSystem.HasPickable == false)
        {
            return false;
        }

        if (objectInteractSystem.CanPlacePickable(_type) == false)
            return false;

        if (objectInteractSystem.TryGetPickableType(out T type) == false)
            return false;

        if (objectInteractSystem.TryGetPickableType(out ICookable cookable))
        {
            CookableSet?.Invoke(cookable);
        }

        objectInteractSystem.TryTakePickable(out IPickable pickable);
        
        _pickedItem = type;
        pickable.SetParent(_holdPoint);

        return true;
    }

    private bool TryInteractWithPickable(PlayerObjectInteract objectInteractSystem)
    {
        if (_pickedItem == null)
            return false;

        if (_pickedItem is IPickable pickable)
        {
            if (objectInteractSystem.HasPickable)
            {
                pickable.Interact(objectInteractSystem);
                return true;
            }
            else if (objectInteractSystem.TryGivePickable(pickable))
            {
                _pickedItem = default;
                CookableRemoved?.Invoke();
                return true;
            }
        }

        return false;
    }
}
