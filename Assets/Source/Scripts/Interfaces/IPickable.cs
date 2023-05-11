using UnityEngine;

public interface IPickable : IInteractable, IPlaceable
{
    public void SetParent(Transform point);
    public void RemoveParent();
}
