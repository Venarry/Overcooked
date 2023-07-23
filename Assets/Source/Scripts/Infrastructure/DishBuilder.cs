using UnityEngine;

public class DishBuilder
{
    private readonly IngredientsCombineSO _ingredientsCombineSO = Resources.Load<IngredientsCombineSO>(AssetsPath.DishCombinesSO);
    private readonly IngredientsIconSO _ingredientsIcon = Resources.Load<IngredientsIconSO>(AssetsPath.IngredientsIconSO);
    private readonly CookableIngredientSO _cookableIngredientSO = Resources.Load<CookableIngredientSO>("SO/Dish");

    public void Build(DishView dishView, int maxCookables)
    {
        ProgressBar progressBar = dishView.GetComponent<ProgressBar>();
        CookingProcessPresenter cookingProcessPresenter = new(_cookableIngredientSO, dishView.MeshFilter, progressBar);

        CookableHolderInteractModel cookableHolderInteractModel = new(
            dishView,
            cookingProcessPresenter,
            dishView.HoldPoint,
            maxCookables, 
            isVisiableCookables: false);

        HolderIngredientsIconShower holderIngredientsIconShower = new(dishView.IngredientsIconPoint, _ingredientsIcon.GetIngredientsIcon());
        CookableHolderInteractPresenter cookableHolderInteractPresenter = new(cookableHolderInteractModel, 
            holderIngredientsIconShower);

        CombineIngredientShower combineIngredientShower = new(dishView.HoldPoint,
            cookableHolderInteractPresenter,
            _ingredientsCombineSO.GetCombines());

        combineIngredientShower.Enable();

        dishView.Init(cookingProcessPresenter, cookableHolderInteractPresenter);
    }
}
