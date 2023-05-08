using UnityEngine;

public class IngredientDistributorCounter : MonoBehaviour, IInteractable
{
    [SerializeField] private Ingredient _template;

    public void Interact(InteractSystem interactSystem)
    {
        if (interactSystem.HasPickable)
            return;

        interactSystem.TryGivePickable(Instantiate(_template));
    }
}
