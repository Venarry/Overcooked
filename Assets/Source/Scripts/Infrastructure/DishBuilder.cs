using UnityEngine;

public class DishBuilder
{
    private readonly IngredientsCombineSO _ingredientsCombineSO = Resources.Load<IngredientsCombineSO>(AssetsPath.DishCombinesSO);
    private readonly IngredientsIconSO _ingredientsIcon = Resources.Load<IngredientsIconSO>(AssetsPath.IngredientsIconSO);

    public void Build(DishView dishView, int maxCookables)
    {
        CookableHolderInteractModel cookableHolderInteractModel = new(dishView, dishView, dishView.HoldPoint, maxCookables);
        CombineIngredientShower combineIngredientShower = new(dishView.HoldPoint, _ingredientsCombineSO.GetCombines());
        HolderIngredientsIconShower holderIngredientsIconShower = new(dishView.IngredientsIconPoint, _ingredientsIcon.GetIngredientsIcon());
        CookableHolderInteractPresenter cookableHolderInteractPresenter = new(cookableHolderInteractModel, 
            combineIngredientShower, 
            holderIngredientsIconShower);

        dishView.Init(cookableHolderInteractPresenter);
    }
}
