using System.Linq;
using UnityEngine;

[RequireComponent(typeof(InteractiveObject))]
[RequireComponent(typeof(ProgressBar))]
public class Ingredient : MonoBehaviour, IPickable, ICookable
{
    [SerializeField] private CookStagesSO _cookStagesSO;
    [SerializeField] private MeshFilter _meshFilter;

    private ProgressBar _progressBar;
    private InteractiveObject _interactive;
    private CookingProcessPresenter _cookingProcessPresenter;

    public bool CanInteract => _interactive.HasParent == false;
    public KitchenObjectType Type => _cookingProcessPresenter.Type;

    private void Awake()
    {
        _interactive = GetComponent<InteractiveObject>();
        _progressBar = GetComponent<ProgressBar>();
        _cookingProcessPresenter = new CookingProcessPresenter(_cookStagesSO, _meshFilter, _progressBar);
    }

    public void Interact(PlayerObjectInteract objectInteractSystem)
    {
        if (objectInteractSystem.HasPickable)
            return;

        objectInteractSystem.TryGivePickable(this);
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
        _cookingProcessPresenter.Cook(step);
    }

    public void AddCookStage()
    {
        _cookingProcessPresenter.AddCookStage();
    }
}
