using System;
using UnityEngine;
using UnityEngine.UIElements;

public class CounterInteractModel : IInteractable
{
    private readonly Transform _holdPoint;
    private readonly KitchenObjectType _type;
    private IPickable _pickedItem;

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

    public bool TryPlaceItem(IPickable item)
    {
        if (_pickedItem != null)
            return false;

        if (item.CanPlaceOn(_type) == false)
            return false;

        item.SetParent(_holdPoint);
        _pickedItem = item;

        if (item is ICookable cookable)
        {
            CookableSet?.Invoke(cookable);
        }

        return true;
    }

    private bool TryPlaceItem(PlayerObjectInteract objectInteractSystem)
    {
        if (_pickedItem != null || objectInteractSystem.HasPickable == false)
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
        
        _pickedItem = pickable;
        pickable.SetParent(_holdPoint);

        return true;
    }

    private bool TryInteractWithItem(PlayerObjectInteract objectInteractSystem)
    {
        if (_pickedItem == null)
            return false;

        if (objectInteractSystem.HasPickable)
        {
            _pickedItem.Interact(objectInteractSystem);
            return true;
        }
        else if (objectInteractSystem.TryGivePickable(_pickedItem))
        {
            _pickedItem = null;
            CookableRemoved?.Invoke();
            return true;
        }

        return false;
    }
}
