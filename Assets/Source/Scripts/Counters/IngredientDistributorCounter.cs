using UnityEngine;

public class IngredientDistributorCounter : MonoBehaviour, IInteractable
{
    [SerializeField] private Ingredient _template;

    public bool CanInteract => true;

    public void Interact(PlayerInteracter interactSystem)
    {
        if (interactSystem.HasPickable)
            return;

        interactSystem.TryGivePickable(Instantiate(_template));
    }
}
