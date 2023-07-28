using System;
using UnityEngine;

[RequireComponent(typeof(InteractedObjectView))]
[RequireComponent(typeof(ProgressBar))]
public class CookingHolderView : MonoBehaviour, ICookableHolder, ICookable, IPickable
{
    [SerializeField] private Transform _holdPoint;
    [SerializeField] private Transform _ingredientsIconPoint;
    [SerializeField] private MeshFilter _meshFilter;

    private InteractedObjectView _interactive;
    private CookingProcessPresenter _cookingProcessPresenter;
    private CookableHolderInteractPresenter _interactPresenter;
    private bool _isInitialized;

    public KitchenObjectType Type => _cookingProcessPresenter.Type;
    public KitchenObjectType[] IngredientsType => _interactPresenter.CookablesType;
    public int CookablesCount => _interactPresenter.CookablesCount;
    public float CookablesCookedTime => _interactPresenter.CookablesCookedTime;
    public float MaxCookedTime => _cookingProcessPresenter.MaxCookedTime;
    public bool CanInteract => _interactive.CanInteract;
    public Transform HoldPoint => _holdPoint;
    public Transform IngredientsIconPoint => _ingredientsIconPoint;
    public MeshFilter MeshFilter => _meshFilter;

    public event Action CookStageAdded;

    private void Awake()
    {
        _interactive = GetComponent<InteractedObjectView>();
    }

    public void Init(CookingProcessPresenter cookingProcessPresenter,
        CookableHolderInteractPresenter interactPresenter)
    {
        gameObject.SetActive(false);

        _cookingProcessPresenter = cookingProcessPresenter;
        _interactPresenter = interactPresenter;
        _isInitialized = true;

        gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        if (_isInitialized == false)
            return;

        _cookingProcessPresenter.CookStageAdded += OnCookStageAdded;
        _cookingProcessPresenter.CookStageSubtracted += OnCookStageSubtracted;
        _cookingProcessPresenter.MaxStageReached += OnMaxStageReached;

        _interactPresenter.HolderCleared += OnHolderCleared;
        _interactPresenter.CookableAdded += OnCookableAdded;

        _cookingProcessPresenter.Enable();
        _interactPresenter.Enable();
    }

    private void OnDisable()
    {
        if (_isInitialized == false)
            return;

        _cookingProcessPresenter.CookStageAdded -= OnCookStageAdded;
        _cookingProcessPresenter.CookStageSubtracted -= OnCookStageSubtracted;
        _cookingProcessPresenter.MaxStageReached -= OnMaxStageReached;

        _interactPresenter.HolderCleared -= OnHolderCleared;
        _interactPresenter.CookableAdded -= OnCookableAdded;

        _cookingProcessPresenter.Disable();
        _interactPresenter.Disable();
    }

    public void Interact(PlayerObjectInteract objectInteractSystem)
    {
        _interactPresenter.Interact(objectInteractSystem);
    }

    public bool CanPlaceOn(KitchenObjectType type) =>
        _interactPresenter.CanPlaceOn(type);

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
        if(_interactPresenter.CookablesCount > 0)
            _cookingProcessPresenter.Cook(step);
    }

    public bool TryAddCookable(ICookable cookable) =>
        _interactPresenter.TryAddCookable(cookable);

    public void GiveCookablesInOutHolder(ICookableHolder cookableHolder)
    {
        _interactPresenter.GiveCookablesInOutHolder(cookableHolder);
    }

    public void AddCookStage()
    {
        _cookingProcessPresenter.AddCookStage();
    }

    public void SubtractCookStage()
    {
        _cookingProcessPresenter.SubtractCookStage();
    }

    public void SetOverCookedStage()
    {
        _cookingProcessPresenter.SetOvercookedStage();
    }
    private void OnHolderCleared()
    {
        _cookingProcessPresenter.ResetStages();
    }

    private void OnCookStageAdded()
    {
        _interactPresenter.AddCookableCookStage();
        _interactPresenter.RefreshIngredientsIcon();
        _cookingProcessPresenter.SetMaxCookedTime(CookablesCookedTime);
        CookStageAdded?.Invoke();
    }

    private void OnCookStageSubtracted()
    {
        _interactPresenter.SubtractCookableCookStage();
        _interactPresenter.RefreshIngredientsIcon();
        _cookingProcessPresenter.SetMaxCookedTime(CookablesCookedTime);
    }

    private void OnMaxStageReached()
    {
        _interactPresenter.SetCookableOvercookedStage();
        _interactPresenter.RefreshIngredientsIcon();
    }

    private void OnCookableAdded()
    {
        _cookingProcessPresenter.RecalculateCookedTime(CookablesCookedTime);
    }
}
