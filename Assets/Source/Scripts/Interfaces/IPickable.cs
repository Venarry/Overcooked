using UnityEngine;

public interface IPickable : IInteractable
{
    public bool CanPlace(KitchenObjectType type);
    public void SetParent(Transform point);
    public void RemoveParent();
}
