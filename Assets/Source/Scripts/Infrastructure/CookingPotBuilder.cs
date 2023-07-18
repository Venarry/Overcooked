using UnityEngine;

public class CookingPotBuilder
{
    private readonly CookableIngredientSO _ingredientSO;
    private readonly IngredientsCombineSO _ingredientsCombineSO;

    public CookingPotBuilder()
    {
        _ingredientSO = Resources.Load<CookableIngredientSO>(AssetsPath.CookingPotSO);
        _ingredientsCombineSO = Resources.Load<IngredientsCombineSO>(AssetsPath.CookingPotCombinesSO);
    }

    public void Build(CookingPotView cookingPotView, int maxCookablesCount)
    {
        ProgressBar progressBar = cookingPotView.GetComponent<ProgressBar>();

        CookingProcessPresenter cookingProcessPresenter = new(_ingredientSO, cookingPotView.MeshFilter, progressBar);

        CookableHolderInteractModel cookableHolderInteractModel = new(cookingPotView, 
            cookingProcessPresenter, 
            cookingPotView.HoldTransform, 
            maxCookablesCount);

        CombineIngredientShower combineIngredientShower = new(cookingPotView.HoldTransform, _ingredientsCombineSO.GetCombines());
        CookableHolderInteractPresenter cookableHolderInteractPresnter = new(cookableHolderInteractModel, combineIngredientShower);

        cookingPotView.Init(cookingProcessPresenter, cookableHolderInteractPresnter);
    }
}
