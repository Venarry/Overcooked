using UnityEngine;

public interface IInteractable
{
    public bool CanInteract => true;
    public void Interact(InteractSystem interactSystem);
}
