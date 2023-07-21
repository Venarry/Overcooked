using System;
using System.Collections.Generic;
using System.Linq;

public class CookableHolderInteractPresenter
{
    private readonly CombineIngredientShower _combineIngredientShower;
    private readonly HolderIngredientsIconShower _holderIngredientsIconShower;
    private readonly CookableHolderInteractModel _cookableHolderInteractModel;

    public int CookableCount => _cookableHolderInteractModel.CookableCount;
    public KitchenObjectType[] CookablesType => _cookableHolderInteractModel.CookablesType;
    public float CookablesCookedTime => _cookableHolderInteractModel.CookablesCookedTime;

    public event Action HolderCleared;
    public event Action<ICookable[]> CookablesChanged;
    public event Action<ICookable> CookableAdded;
    public event Action CookableRemoved;

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
        _cookableHolderInteractModel.SubtractCookableCookStages();
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

    public void OnCookablesChanged(ICookable[] cookables)
    {
        //List<KitchenObjectType> cookablesTypes = new();
        //cookablesTypes.AddRange(cookables.Select(cookable => cookable.Type));

        _combineIngredientShower.RefreshModel(_cookableHolderInteractModel.CookablesType);

        CookablesChanged?.Invoke(cookables);
    }

    public void OnCookableAdded(ICookable cookable)
    {
        _holderIngredientsIconShower.AddIngredient(cookable);
        CookableAdded?.Invoke(cookable);
    }

    public void OnCookableRemoved(ICookable cookable)
    {
        _holderIngredientsIconShower.RemoveIngredient(cookable);
        CookableRemoved?.Invoke();
    }

    private void OnHolderClear()
    {
        HolderCleared?.Invoke();
    }
}
