using UnityEngine;

public class IngredientDistributorCounter : MonoBehaviour, IInteractable
{
    [SerializeField] private Ingredient _template;

    public bool CanInteract => true;

    public void Interact(PlayerObjectInteract objectInteractSystem)
    {
        if (objectInteractSystem.HasPickable)
            return;

        objectInteractSystem.TryGivePickable(Instantiate(_template));
    }
}
