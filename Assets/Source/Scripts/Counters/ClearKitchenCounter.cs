using UnityEngine;

public class ClearKitchenCounter : MonoBehaviour, IInteractable
{
    [SerializeField] private KitchenObjectType _type;
    [SerializeField] private Transform _holdPoint;

    private IPickable _pickable;

    public bool CanInteract => true;

    public void Interact(PlayerObjectInteract objectInteractSystem)
    {
        if (TryGetPickable(objectInteractSystem))
            return;

        TryInteractWithPickable(objectInteractSystem);
    }

    private bool TryGetPickable(PlayerObjectInteract objectInteractSystem)
    {
        if (_pickable != null || objectInteractSystem.HasPickable == false)
        {
            return false;
        }

        if (objectInteractSystem.CanPlacePickable(_type) == false)
            return false;

        objectInteractSystem.TryTakePickable(out IPickable pickable);

        _pickable = pickable;
        _pickable.SetParent(_holdPoint);

        return true;
    }

    private bool TryInteractWithPickable(PlayerObjectInteract objectInteractSystem)
    {
        if (_pickable == null)
            return false;
        
        if (objectInteractSystem.HasPickable)
        {
            _pickable.Interact(objectInteractSystem);
            return true;
        }

        if (objectInteractSystem.TryGivePickable(_pickable))
        {
            _pickable = null;
            return true;
        }

        return false;
    }
}
