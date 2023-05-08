using UnityEngine;

public interface IInteractSystem
{
    public IPickable GetIPickable();
    public bool TryTakeIPickable(out IPickable pickable);
}
