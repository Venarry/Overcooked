using UnityEngine;
using UnityEngine.UIElements;

public class CookingPotBuilder
{
    private readonly CookableIngredientSO _ingredientSO = Resources.Load<CookableIngredientSO>(AssetsPath.CookingPotSO);
    private readonly IngredientsCombineSO _ingredientsCombineSO = Resources.Load<IngredientsCombineSO>(AssetsPath.CookingPotCombinesSO);
    private readonly IngredientsIconSO _ingredientsIcon = Resources.Load<IngredientsIconSO>(AssetsPath.IngredientsIconSO);

    public void Build(CookingPotView cookingPotView, int maxCookablesCount)
    {
        ProgressBar progressBar = cookingPotView.GetComponent<ProgressBar>();

        CookingProcessPresenter cookingProcessPresenter = new(_ingredientSO, cookingPotView.MeshFilter, progressBar);

        CookableHolderInteractModel cookableHolderInteractModel = new(cookingPotView, 
            cookingProcessPresenter, 
            cookingPotView.HoldTransform, 
            maxCookablesCount);

        CombineIngredientShower combineIngredientShower = new(cookingPotView.HoldTransform, _ingredientsCombineSO.GetCombines());
        HolderIngredientsIconShower holderIngredientsIconShower = new(cookingPotView.IngredientsIconPoint, _ingredientsIcon.GetIngredientsIcon());
        CookableHolderInteractPresenter cookableHolderInteractPresnter = new(cookableHolderInteractModel, 
            combineIngredientShower, 
            holderIngredientsIconShower);

        cookingPotView.Init(cookingProcessPresenter, cookableHolderInteractPresnter);
    }
}
