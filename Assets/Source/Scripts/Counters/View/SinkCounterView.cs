using UnityEngine;

public class SinkCounterView : MonoBehaviour, IInteractable
{
    public bool CanInteract => throw new System.NotImplementedException();

    public void Interact(PlayerObjectInteract objectInteractSystem)
    {
        throw new System.NotImplementedException();
    }
}
