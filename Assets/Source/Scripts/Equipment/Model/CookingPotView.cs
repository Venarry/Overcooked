using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UIElements;

[RequireComponent(typeof(InteractedObjectView))]
[RequireComponent(typeof(ProgressBar))]
public class CookingPotView : MonoBehaviour, ICookableHolder, ICookable, IPickable
{
    [SerializeField] private Transform _holdPoint;
    [SerializeField] private int _maxCookables = 1;
    [SerializeField] private CookableIngredientSO _cookStages;
    [SerializeField] private MeshFilter _meshFilter;
    [SerializeField] private IngredientsCombineSO _ingredientsCombineSO;

    private InteractedObjectView _interactive;
    private CookingProcessPresenter _cookingProcessPresenter;
    private CookableHolderInteractPresenter _cookableHolderInteractPresnter;

    public KitchenObjectType Type => _cookingProcessPresenter.Type;
    public int CookablesCount => _cookableHolderInteractPresnter.CookableCount;
    public bool CanInteract => _interactive.CanInteract;

    private void Awake()
    {
        _interactive = GetComponent<InteractedObjectView>();
        ProgressBar progressBar = GetComponent<ProgressBar>();
        _cookingProcessPresenter = new CookingProcessPresenter(_cookStages, _meshFilter, progressBar);

        CookableHolderInteractModel cookableHolderInteractModel = new(this, _cookingProcessPresenter, _holdPoint, _maxCookables);
        CombineIngredientShower combineIngredientShower = new(_holdPoint, _ingredientsCombineSO.GetCombines());

        _cookableHolderInteractPresnter = new CookableHolderInteractPresenter(cookableHolderInteractModel, combineIngredientShower);
    }

    private void OnEnable()
    {
        _cookingProcessPresenter.CookStageChanged += _cookableHolderInteractPresnter.OnCookStageChanged;
        _cookableHolderInteractPresnter.HolderCleared += _cookingProcessPresenter.ResetStages;

        _cookingProcessPresenter.Enable();
        _cookableHolderInteractPresnter.Enable();
    }

    private void OnDisable()
    {
        _cookingProcessPresenter.CookStageChanged -= _cookableHolderInteractPresnter.OnCookStageChanged;
        _cookableHolderInteractPresnter.HolderCleared -= _cookingProcessPresenter.ResetStages;

        _cookingProcessPresenter.Disable();
        _cookableHolderInteractPresnter.Disable();
    }

    public void Interact(PlayerObjectInteract objectInteractSystem)
    {
        _cookableHolderInteractPresnter.Interact(objectInteractSystem);
    }

    public bool CanPlaceOn(KitchenObjectType type) =>
        _cookableHolderInteractPresnter.CanPlaceOn(type);

    public void SetParent(Transform point, bool isVisiable)
    {
        _interactive.SetParent(point, isVisiable);
    }

    public void RemoveParent()
    {
        _interactive.RemoveParent();
    }

    public void Cook(float step = 0)
    {
        if(_cookableHolderInteractPresnter.CookableCount > 0)
            _cookingProcessPresenter.Cook(step);
    }

    public bool TryAddCookable(ICookable cookable) =>
        _cookableHolderInteractPresnter.TryAddCookable(cookable, _cookingProcessPresenter.Type);

    public void GiveCookablesInOutHolder(ICookableHolder cookableHolder)
    {
        _cookableHolderInteractPresnter.GiveCookablesInOutHolder(cookableHolder);
    }

    public void AddCookStage()
    {
        _cookingProcessPresenter.AddCookStage();
    }
}
