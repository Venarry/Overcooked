using UnityEngine;

public class DishBuilder
{
    private readonly IngredientsCombineSO _ingredientsCombineSO = Resources.Load<IngredientsCombineSO>(AssetsPath.DishCombinesSO);

    public void Build(DishView dishView, int maxCookables)
    {
        CookableHolderInteractModel cookableHolderInteractModel = new(dishView, dishView, dishView.HoldTransform, maxCookables);
        CombineIngredientShower combineIngredientShower = new(dishView.HoldTransform, _ingredientsCombineSO.GetCombines());
        CookableHolderInteractPresenter cookableHolderInteractPresenter = new(cookableHolderInteractModel, combineIngredientShower);

        dishView.Init(cookableHolderInteractPresenter);
    }
}
