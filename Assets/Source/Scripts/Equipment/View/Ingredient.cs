using System.Linq;
using UnityEngine;

[RequireComponent(typeof(InteractedObjectView))]
[RequireComponent(typeof(ProgressBar))]
public class Ingredient : MonoBehaviour, IPickable, ICookable
{
    [SerializeField] private CookableIngredientSO _cookStagesSO;
    [SerializeField] private MeshFilter _meshFilter;

    private InteractedObjectView _interactive;
    private CookingProcessPresenter _cookingProcessPresenter;

    public bool CanInteract => _interactive.CanInteract;
    public KitchenObjectType Type => _cookingProcessPresenter.Type;

    private void Awake()
    {
        _interactive = GetComponent<InteractedObjectView>();
        ProgressBar progressBar = GetComponent<ProgressBar>();
        _cookingProcessPresenter = new CookingProcessPresenter(_cookStagesSO, _meshFilter, progressBar);
    }

    private void OnEnable()
    {
        _cookingProcessPresenter.Enable();
    }

    private void OnDisable()
    {
        _cookingProcessPresenter.Disable();
    }

    public void Interact(PlayerObjectInteract objectInteractSystem)
    {
        if (objectInteractSystem.HasPickable)
            return;

        objectInteractSystem.TryGivePickable(this);
    }

    public bool CanPlaceOn(KitchenObjectType type) => _cookingProcessPresenter.AvailablePlaceTypes.Contains(type);

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
        _cookingProcessPresenter.Cook(step);
    }

    public void AddCookStage()
    {
        _cookingProcessPresenter.AddCookStage();
    }

    public void SubtractCookStage()
    {
        _cookingProcessPresenter.SubtractCookStage();
    }
}
