using System;
using UnityEngine;

public class CounterInteractModel
{
    private readonly Transform _holdPoint;
    private readonly KitchenObjectType _type;
    private IPickable _pickable;

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
        if (TryPlaceItem(objectInteractSystem))
            return;

        TryInteractWithItem(objectInteractSystem);
    }

    public bool TryPlacePickable(IPickable pickable)
    {
        if (_pickable != null)
            return false;

        if (pickable.CanPlaceOn(_type) == false)
            return false;

        pickable.SetParent(_holdPoint);
        _pickable = pickable;

        if (pickable is ICookable cookable)
        {
            CookableSet?.Invoke(cookable);
        }

        return true;
    }

    private bool TryPlaceItem(PlayerObjectInteract objectInteractSystem)
    {
        if (_pickable != null || objectInteractSystem.HasPickable == false)
        {
            return false;
        }

        if (objectInteractSystem.CanPlacePickable(_type) == false)
            return false;

        if (objectInteractSystem.TryGetPickableType(out ICookable cookable))
        {
            CookableSet?.Invoke(cookable);
        }

        objectInteractSystem.TryTakePickable(out IPickable pickable);
        
        _pickable = pickable;
        pickable.SetParent(_holdPoint);

        return true;
    }

    private bool TryInteractWithItem(PlayerObjectInteract objectInteractSystem)
    {
        if (_pickable == null)
            return false;

        if (objectInteractSystem.HasPickable)
        {
            _pickable.Interact(objectInteractSystem);
            return true;
        }
        else if (objectInteractSystem.TryGivePickable(_pickable))
        {
            _pickable = null;
            CookableRemoved?.Invoke();
            return true;
        }

        return false;
    }
}
