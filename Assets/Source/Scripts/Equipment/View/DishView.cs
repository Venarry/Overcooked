using System.Linq;
using UnityEngine;

[RequireComponent(typeof(InteractedObjectView))]
public class DishView : MonoBehaviour, ICookableHolder, IPickable, IServiceHolder, ITypeProvider
{
    [SerializeField] private Transform _holdPoint;
    [SerializeField] private Transform _ingredientsIconPoint;
    [SerializeField] private KitchenObjectType _type;
    [SerializeField] private KitchenObjectType[] _availablePlaceTypes;

    private InteractedObjectView _interactive;
    private CookableHolderInteractPresenter _cookableHolderInteractPresenter;

    public bool CanInteract => _interactive.CanInteract;
    public int CookablesCount => _cookableHolderInteractPresenter.CookablesCount;
    public KitchenObjectType[] IngredientsType => _cookableHolderInteractPresenter.CookablesType;
    public KitchenObjectType Type => _type;
    public KitchenObjectType[] AvailablePlaceTypes => _availablePlaceTypes.ToArray();
    public Transform HoldPoint => _holdPoint;
    public Transform IngredientsIconPoint => _ingredientsIconPoint;


    private void Awake()
    {
        _interactive = GetComponent<InteractedObjectView>();
    }

    public void Enable()
    {
        _cookableHolderInteractPresenter.Enable();
    }

    public void Disable()
    {
        _cookableHolderInteractPresenter.Disable();
    }

    public void Init(CookableHolderInteractPresenter cookableHolderInteractPresenter)
    {
        _cookableHolderInteractPresenter = cookableHolderInteractPresenter;
    }

    public void Interact(PlayerObjectInteract objectInteractSystem)
    {
        _cookableHolderInteractPresenter.Interact(objectInteractSystem);
    }

    public void RemoveObject()
    {
        Destroy(gameObject);
    }

    public bool CanPlaceOn(KitchenObjectType type) =>
        _availablePlaceTypes.Contains(type);

    public void GiveCookablesInOutHolder(ICookableHolder cookableHolder)
    {
        _cookableHolderInteractPresenter.GiveCookablesInOutHolder(cookableHolder);
    }

    public void RemoveParent()
    {
        _interactive.RemoveParent();
    }

    public void SetParent(Transform point, bool isVisiable)
    {
        _interactive.SetParent(point, isVisiable);
    }

    public bool TryAddCookable(ICookable cookable) =>
        _cookableHolderInteractPresenter.TryAddCookable(cookable);
}
