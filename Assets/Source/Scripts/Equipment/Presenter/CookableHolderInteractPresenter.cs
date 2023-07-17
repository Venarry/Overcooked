using System;

public class CookableHolderInteractPresenter
{
    private CombineIngredientShower _combineIngredientShower;
    private CookableHolderInteractModel _cookableHolderInteractModel;

    public int CookableCount => _cookableHolderInteractModel.CookableCount;
    public KitchenObjectType[] CookablesType => _cookableHolderInteractModel.CookablesType;

    public event Action HolderCleared;

    public CookableHolderInteractPresenter(CookableHolderInteractModel cookableHolderInteractModel, 
        CombineIngredientShower combineIngredientShower)
    {
        _combineIngredientShower = combineIngredientShower;
        _cookableHolderInteractModel = cookableHolderInteractModel;
    }

    public void Enable()
    {
        _cookableHolderInteractModel.CookablesChanged += OnCookablesChanged;
        _cookableHolderInteractModel.HolderCleared += OnHolderClear;
    }

    public void Disable()
    {
        _cookableHolderInteractModel.CookablesChanged -= OnCookablesChanged;
        _cookableHolderInteractModel.HolderCleared -= OnHolderClear;
    }

    public void OnCookStageChanged()
    {
        _cookableHolderInteractModel.OnCookStageChanged();
    }

    public void Interact(PlayerObjectInteract objectInteractSystem)
    {
        _cookableHolderInteractModel.Interact(objectInteractSystem);
    }

    public bool CanPlaceOn(KitchenObjectType type) =>
        _cookableHolderInteractModel.CanPlaceOn(type);

    public bool TryAddCookable(ICookable cookable, KitchenObjectType type) =>
        _cookableHolderInteractModel.TryAddCookable(cookable, type);

    public void GiveCookablesInOutHolder(ICookableHolder cookableHolder) =>
        _cookableHolderInteractModel.GiveCookablesInOutHolder(cookableHolder);

    public void OnCookablesChanged(KitchenObjectType[] types)
    {
        _combineIngredientShower.RefreshModel(types);
    }

    private void OnHolderClear()
    {
        HolderCleared?.Invoke();
    }
}
