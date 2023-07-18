using UnityEngine;

public class CookingPotFactory : MonoBehaviour
{
    [SerializeField] private CookingPotView _template;
    [SerializeField] private CookableIngredientSO _ingredientSO;
    [SerializeField] private IngredientsCombineSO _ingredientsCombineSO;
    [SerializeField] private int _maxCookables = 1;

    public CookingPotView Create()
    {
        CookingPotView cookingPotView = Instantiate(_template);
        ProgressBar progressBar = cookingPotView.GetComponent<ProgressBar>();

        CookingProcessPresenter cookingProcessPresenter = new(_ingredientSO, cookingPotView.MeshFilter, progressBar);
        CookableHolderInteractModel cookableHolderInteractModel = new(cookingPotView, cookingProcessPresenter, cookingPotView.HoldTransform, _maxCookables);
        CombineIngredientShower combineIngredientShower = new(cookingPotView.HoldTransform, _ingredientsCombineSO.GetCombines());
        CookableHolderInteractPresenter cookableHolderInteractPresnter = new(cookableHolderInteractModel, combineIngredientShower);

        cookingPotView.Init(cookingProcessPresenter, cookableHolderInteractPresnter);
        cookingPotView.Enable();

        return cookingPotView;
    }
}
