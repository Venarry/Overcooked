using System;

public class CookableHolderInteractPresenter
{
    private CombineIngredientShower _combineIngredientShower;
    private HolderIngredientsIconShower _holderIngredientsIconShower;
    private CookableHolderInteractModel _cookableHolderInteractModel;

    public int CookableCount => _cookableHolderInteractModel.CookableCount;
    public KitchenObjectType[] CookablesType => _cookableHolderInteractModel.CookablesType;

    public event Action HolderCleared;

    public CookableHolderInteractPresenter(CookableHolderInteractModel cookableHolderInteractModel,
        CombineIngredientShower combineIngredientShower,
        HolderIngredientsIconShower holderIngredientsIconShower)
    {
        _combineIngredientShower = combineIngredientShower;
        _cookableHolderInteractModel = cookableHolderInteractModel;
        _holderIngredientsIconShower = holderIngredientsIconShower;
    }

    public void Enable()
    {
        _cookableHolderInteractModel.CookablesChanged += OnCookablesChanged;
        _cookableHolderInteractModel.CookableAdded += OnCookableAdded;
        _cookableHolderInteractModel.CookableRemoved += OnCookableRemoved;
        _cookableHolderInteractModel.HolderCleared += OnHolderClear;
    }

    public void Disable()
    {
        _cookableHolderInteractModel.CookablesChanged -= OnCookablesChanged;
        _cookableHolderInteractModel.CookableAdded -= OnCookableAdded;
        _cookableHolderInteractModel.CookableRemoved -= OnCookableRemoved;
        _cookableHolderInteractModel.HolderCleared -= OnHolderClear;
    }

    public void AddCookableCookStage()
    {
        _cookableHolderInteractModel.AddCookableCookStages();
    }

    public void SubtractCookableCookStage()
    {
        _cookableHolderInteractModel.AddCookableCookStages();
    }

    public void Interact(PlayerObjectInteract objectInteractSystem)
    {
        _cookableHolderInteractModel.Interact(objectInteractSystem);
    }

    public void RefreshIngredientsIcon()
    {
        _holderIngredientsIconShower.RefreshIngredientsIcon();
    }

    public bool CanPlaceOn(KitchenObjectType type) =>
        _cookableHolderInteractModel.CanPlaceOn(type);

    public bool TryAddCookable(ICookable cookable) =>
        _cookableHolderInteractModel.TryAddCookable(cookable);

    public void GiveCookablesInOutHolder(ICookableHolder cookableHolder) =>
        _cookableHolderInteractModel.GiveCookablesInOutHolder(cookableHolder);

    public void OnCookablesChanged(KitchenObjectType[] types)
    {
        _combineIngredientShower.RefreshModel(types);
    }

    public void OnCookableAdded(ICookable cookable)
    {
        _holderIngredientsIconShower.AddIngredient(cookable);
    }

    public void OnCookableRemoved(ICookable cookable)
    {
        _holderIngredientsIconShower.RemoveIngredient(cookable);
    }

    private void OnHolderClear()
    {
        HolderCleared?.Invoke();
    }
}
