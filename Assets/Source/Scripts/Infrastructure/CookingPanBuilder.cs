using UnityEngine;

public class CookingPanBuilder
{
    private readonly CookableIngredientSO _ingredientSO = Resources.Load<CookableIngredientSO>(AssetsPath.CookingPanSO);
    private readonly IngredientsIconSO _ingredientsIcon = Resources.Load<IngredientsIconSO>(AssetsPath.IngredientsIconSO);

    public void Build(CookingHolderView cookingPanView, int maxCookablesCount)
    {
        ProgressBar progressBar = cookingPanView.GetComponent<ProgressBar>();
        CookingProcessPresenter cookingProcessPresenter = new(_ingredientSO, cookingPanView.MeshFilter, progressBar);

        CookableHolderInteractModel cookableHolderInteractModel = new(cookingPanView,
            cookingProcessPresenter,
            cookingPanView.HoldPoint,
            maxCookablesCount,
            isVisiableCookables: true);

        HolderIngredientsIconShower holderIngredientsIconShower = new(cookingPanView.IngredientsIconPoint, _ingredientsIcon.GetIngredientsIcon());
        CookableHolderInteractPresenter cookableHolderInteractPresenter = new(cookableHolderInteractModel,
            holderIngredientsIconShower);

        cookingPanView.Init(cookingProcessPresenter, cookableHolderInteractPresenter);
    }
}
