using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UIElements;

[RequireComponent(typeof(InteractiveObjectView))]
[RequireComponent(typeof(ProgressBar))]
public class CookingPot : MonoBehaviour, ICookableHolder, ICookable, IPickable
{
    [SerializeField] private Transform _holdPoint;
    [SerializeField] private int _maxCookables = 1;
    [SerializeField] private CookStagesSO _cookStages;
    [SerializeField] private MeshFilter _meshFilter;
    [SerializeField] private IngredientsCombineSO _ingredientsCombineSO;

    private InteractiveObjectView _interactive;
    private CookingProcessPresenter _cookingProcessPresenter;
    private CookableHolder _cookableHolder;

    public KitchenObjectType Type => _cookingProcessPresenter.Type;
    public int CookablesCount => _cookableHolder.CookableCount;
    public bool CanInteract => _interactive.HasParent == false;

    private void Awake()
    {
        _interactive = GetComponent<InteractiveObjectView>();
        ProgressBar progressBar = GetComponent<ProgressBar>();
        _cookingProcessPresenter = new CookingProcessPresenter(_cookStages, _meshFilter, progressBar);
        _cookableHolder = new CookableHolder(_holdPoint, _maxCookables, this, _ingredientsCombineSO.GetCombines());
    }

    private void OnEnable()
    {
        _cookingProcessPresenter.CookStageChanged += _cookableHolder.OnCookStageChanged;
        _cookableHolder.HolderCleared += _cookingProcessPresenter.ResetStages;
        _cookingProcessPresenter.Enable();
    }

    private void OnDisable()
    {
        _cookingProcessPresenter.CookStageChanged -= _cookableHolder.OnCookStageChanged;
        _cookableHolder.HolderCleared -= _cookingProcessPresenter.ResetStages;
        _cookingProcessPresenter.Disable();
    }

    public void Interact(PlayerObjectInteract objectInteractSystem)
    {
        _cookableHolder.Interact(objectInteractSystem, _cookingProcessPresenter.Type);
    }

    public bool CanPlace(KitchenObjectType type) => _cookingProcessPresenter.AvailablePlaceTypes.Contains(type);

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
        if(_cookableHolder.CookableCount > 0)
            _cookingProcessPresenter.Cook(step);
    }

    public bool TryAddCookable(ICookable cookable) =>
        _cookableHolder.TryAddCookable(cookable, _cookingProcessPresenter.Type);

    public void GiveCookablesInOutHolder(ICookableHolder cookableHolder)
    {
        _cookableHolder.GiveCookablesInOutHolder(cookableHolder);
    }

    public void AddCookStage()
    {
        _cookingProcessPresenter.AddCookStage();
    }
}
