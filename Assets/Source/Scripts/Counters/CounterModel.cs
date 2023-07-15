using UnityEngine;

public class CounterModel<T> : IInteractable
{
    private Transform _holdPoint;
    private KitchenObjectType _type;
    private T _pickedItem;

    public CounterModel(Transform holdPoint, KitchenObjectType type)
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

            if (objectInteractSystem.TryGivePickable(pickable))
            {
                _pickedItem = default;
                return true;
            }
        }

        return false;
    }
}
