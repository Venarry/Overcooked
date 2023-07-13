using UnityEngine;

public interface IPickable : IInteractable
{
    public bool CanPlace(KitchenObjectType type);
    public void SetParent(Transform point, bool isVisiable = true);
    public void RemoveParent();
}
