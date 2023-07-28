using UnityEngine;
using UnityEngine.UIElements;

public class CookingPotBuilder
{
    private readonly CookableIngredientSO _ingredientSO = Resources.Load<CookableIngredientSO>(AssetsPath.CookingPotSO);
    private readonly IngredientsCombineSO _ingredientsCombineSO = Resources.Load<IngredientsCombineSO>(AssetsPath.CookingPotCombinesSO);
    private readonly IngredientsIconSO _ingredientsIcon = Resources.Load<IngredientsIconSO>(AssetsPath.IngredientsIconSO);

    public void Build(CookingHolderView cookingPotView, int maxCookablesCount)
    {
        ProgressBar progressBar = cookingPotView.GetComponent<ProgressBar>();

        CookingProcessPresenter cookingProcessPresenter = new(_ingredientSO, cookingPotView.MeshFilter, progressBar);

        CookableHolderInteractModel cookableHolderInteractModel = new(cookingPotView, 
            cookingProcessPresenter, 
            cookingPotView.HoldPoint, 
            maxCookablesCount,
            isVisiableCookables: false);

        HolderIngredientsIconShower holderIngredientsIconShower = new(cookingPotView.IngredientsIconPoint, _ingredientsIcon.GetIngredientsIcon());
        CookableHolderInteractPresenter cookableHolderInteractPresenter = new(cookableHolderInteractModel, 
            holderIngredientsIconShower);

        CombineIngredientMeshView combineIngredientShower = new(cookingPotView.HoldPoint, 
            cookableHolderInteractPresenter, 
            _ingredientsCombineSO.GetCombines());

        combineIngredientShower.Enable();

        cookingPotView.Init(cookingProcessPresenter, cookableHolderInteractPresenter);
    }
}
