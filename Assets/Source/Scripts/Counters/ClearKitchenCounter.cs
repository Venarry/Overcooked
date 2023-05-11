using System.Linq;
using UnityEngine;

public class ClearKitchenCounter : MonoBehaviour, IInteractable
{
    [SerializeField] private KitchenObjectType _type;
    [SerializeField] private Transform _holdPoint;
    private IPickable _pickable;

    public void Interact(PlayerInteracter interactSystem)
    {
        if (TryGetPickable(interactSystem))
            return;

        TryInteractWithPickable(interactSystem);
    }

    private bool TryGetPickable(PlayerInteracter interactSystem)
    {
        if (_pickable == null && interactSystem.HasPickable)
        {
            if (interactSystem.CanPlacePickable(_type) == false)
                return false;

            interactSystem.TryTakePickable(out IPickable pickable);

            _pickable = pickable;
            _pickable.SetParent(_holdPoint);
            return true;
        }

        return false;
    }

    private bool TryInteractWithPickable(PlayerInteracter interactSystem)
    {
        if (_pickable == null)
            return false;
        
        if (interactSystem.HasPickable)
        {
            _pickable.Interact(interactSystem);
        }
        else
        {
            if (interactSystem.TryGivePickable(_pickable))
            {
                _pickable = null;
            }
        }

        return true;
    }
}
