using System;

public class CookableHolderInteractPresenter
{
    private readonly HolderIngredientsIconShower _holderIngredientsIconShower;
    private readonly CookableHolderInteractModel _cookableHolderInteractModel;

    public int CookablesCount => _cookableHolderInteractModel.CookableCount;
    public KitchenObjectType[] CookablesType => _cookableHolderInteractModel.CookablesType;
    public float CookablesCookedTime => _cookableHolderInteractModel.CookablesCookedTime;

    public event Action HolderCleared;
    public event Action CookableAdded;
    public event Action CookableRemoved;

    public event Action Enabled;
    public event Action Disabled;

    public CookableHolderInteractPresenter(CookableHolderInteractModel cookableHolderInteractModel,
        HolderIngredientsIconShower holderIngredientsIconShower)
    {
        _cookableHolderInteractModel = cookableHolderInteractModel;
        _holderIngredientsIconShower = holderIngredientsIconShower;
    }

    public void Enable()
    {
        _cookableHolderInteractModel.CookableAdded += OnCookableAdded;
        _cookableHolderInteractModel.CookableRemoved += OnCookableRemoved;
        _cookableHolderInteractModel.HolderCleared += OnHolderClear;

        Enabled?.Invoke();
    }

    public void Disable()
    {
        _cookableHolderInteractModel.CookableAdded -= OnCookableAdded;
        _cookableHolderInteractModel.CookableRemoved -= OnCookableRemoved;
        _cookableHolderInteractModel.HolderCleared -= OnHolderClear;

        Disabled?.Invoke();
    }

    public void AddCookableCookStage()
    {
        _cookableHolderInteractModel.AddCookableCookStages();
    }

    public void SubtractCookableCookStage()
    {
        _cookableHolderInteractModel.SubtractCookableCookStages();
    }

    public void SetCookableOvercookedStage()
    {
        _cookableHolderInteractModel.SetCookableOvercookedStage();
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

    private void OnCookableAdded(ICookable cookable)
    {
        _holderIngredientsIconShower.AddIngredient(cookable);
        //_combineIngredientShower.RefreshModel(CookablesType);
        CookableAdded?.Invoke();
    }

    private void OnCookableRemoved(ICookable cookable)
    {
        _holderIngredientsIconShower.RemoveIngredient(cookable);
        //_combineIngredientShower.RefreshModel(CookablesType);
        CookableRemoved?.Invoke();
    }

    private void OnHolderClear()
    {
        HolderCleared?.Invoke();
    }
}
