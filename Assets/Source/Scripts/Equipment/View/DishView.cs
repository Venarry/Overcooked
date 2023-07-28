using System;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(InteractedObjectView))]
public class DishView : MonoBehaviour, ICookableHolder, IPickable, IServiceHolder
{
    [SerializeField] private Transform _holdPoint;
    [SerializeField] private Transform _ingredientsIconPoint;
    [SerializeField] private MeshFilter _meshFilter;

    private InteractedObjectView _interactive;
    private CookableHolderInteractPresenter _cookableHolderInteractPresenter;
    private CookingProcessPresenter _cookingProcessPresenter;
    private bool _isInitialized;

    public event Action DishWashed;

    public bool CanInteract => _interactive.CanInteract;
    public int CookablesCount => _cookableHolderInteractPresenter.CookablesCount;
    public KitchenObjectType[] IngredientsType => _cookableHolderInteractPresenter.CookablesType;
    public Transform HoldPoint => _holdPoint;
    public MeshFilter MeshFilter => _meshFilter;
    public Transform IngredientsIconPoint => _ingredientsIconPoint;

    private void Awake()
    {
        _interactive = GetComponent<InteractedObjectView>();
    }

    public void Init(CookingProcessPresenter cookingProcessPresenter, CookableHolderInteractPresenter cookableHolderInteractPresenter)
    {
        gameObject.SetActive(false);

        _cookingProcessPresenter = cookingProcessPresenter;
        _cookableHolderInteractPresenter = cookableHolderInteractPresenter;
        _isInitialized = true;

        gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        if (_isInitialized == false)
            return;

        _cookableHolderInteractPresenter.Enable();
        _cookingProcessPresenter.Enable();

        _cookableHolderInteractPresenter.HolderCleared += OnHolderCleared;
        _cookingProcessPresenter.MaxStageReached += OnDishWashed;
    }

    private void OnDisable()
    {
        if (_isInitialized == false)
            return;

        _cookableHolderInteractPresenter.Disable();
        _cookingProcessPresenter.Disable();

        _cookableHolderInteractPresenter.HolderCleared -= OnHolderCleared;
    }

    public void Interact(PlayerObjectInteract objectInteractSystem)
    {
        _cookableHolderInteractPresenter.Interact(objectInteractSystem);
    }

    public void WashDish()
    {
        _cookingProcessPresenter.SetOvercookedStage();
    }

    public void Wash(float step = 0)
    {
        _cookingProcessPresenter.Cook(step);
    }

    public void ResetCookingStageProcess()
    {
        _cookingProcessPresenter.ResetStageProgress();
    }

    public void RemoveObject()
    {
        Destroy(gameObject);
    }

    public bool CanPlaceOn(KitchenObjectType type) =>
        _cookableHolderInteractPresenter.CanPlaceOn(type);

    public void GiveCookablesInOutHolder(ICookableHolder cookableHolder)
    {
        _cookableHolderInteractPresenter.GiveCookablesInOutHolder(cookableHolder);
    }

    public void RemoveParent()
    {
        _interactive.RemoveParent();
    }

    public void SetParent(Transform point, bool isVisiable = true)
    {
        _interactive.SetParent(point, isVisiable);
    }

    public bool TryAddCookable(ICookable cookable) =>
        _cookableHolderInteractPresenter.TryAddCookable(cookable);

    private void OnHolderCleared()
    {
        _cookingProcessPresenter.ResetStages();
    }

    private void OnDishWashed()
    {
        DishWashed?.Invoke();
    }
}
