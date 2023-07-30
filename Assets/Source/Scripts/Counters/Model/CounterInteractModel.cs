using System;
using UnityEngine;

public class CounterInteractModel
{
    private readonly ITypeProvider _typeProvider;
    private IPickable _pickable;

    public event Action<ICookable> CookableSet;
    public event Action<IPickable> PickableSet;
    public event Action CookableRemoved;

    public CounterInteractModel(ITypeProvider typeProvider)
    {
        _typeProvider = typeProvider;
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

        if (pickable.CanPlaceOn(_typeProvider.Type) == false)
            return false;

        //pickable.SetParent(_holdPoint);
        _pickable = pickable;

        if (pickable is ICookable cookable)
        {
            CookableSet?.Invoke(cookable);
        }

        PickableSet?.Invoke(_pickable);

        return true;
    }

    private bool TryPlaceItem(PlayerObjectInteract objectInteractSystem)
    {
        if (_pickable != null || objectInteractSystem.HasPickable == false)
        {
            return false;
        }

        if (objectInteractSystem.CanPlacePickable(_typeProvider.Type) == false)
            return false;

        if (objectInteractSystem.TryGetPickableType(out ICookable cookable))
        {
            CookableSet?.Invoke(cookable);
        }

        objectInteractSystem.TryTakePickable(out IPickable pickable);
        
        _pickable = pickable;
        PickableSet?.Invoke(_pickable);
        //pickable.SetParent(_holdPoint);

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
