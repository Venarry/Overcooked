using System;

public interface IHolderInteractPresenter : ICookableHolder
{
    public event Action HolderCleared;
    public event Action CookableAdded;
    public event Action CookableRemoved;
    public float CookablesCookedTime { get; }
    public void Enable();
    public void Disable();
    public void Interact(PlayerObjectInteract objectInteractSystem);
    public void AddCookableCookStage();
    public void SubtractCookableCookStage();
    public void SetCookableOvercookedStage();
    public void RefreshIngredientsIcon();
    public bool CanPlaceOn(KitchenObjectType type);
}
