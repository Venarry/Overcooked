using UnityEngine;

public class DishesCounterView : MonoBehaviour, IInteractable, ITypeProvider
{
    [SerializeField] private KitchenObjectType _type;
    [SerializeField] private Transform _startDishesPoint;
    private Transform[] _dishPoints;
    private int _currentDishPoint;

    private DishesCounterInteractPresenter _interactPresenter;

    public int MaxDishesCount => _interactPresenter.MaxDishesCount;
    public bool CanInteract => _interactPresenter.CanInteract;
    public KitchenObjectType Type => _type;
    public KitchenObjectType[] AvailablePlaceTypes => new KitchenObjectType[0];

    private void Awake()
    {
        int maxDishesCount = 6;
        DishesCounterInteractModel interactModel = new(maxDishesCount, this);
        DishesCounterInteractPresenter interactPresenter = new(interactModel);

        Init(interactPresenter, maxDishesCount);
        Enable();
    }

    public void Init(DishesCounterInteractPresenter interactPresenter, int maxDishesCount)
    {
        _interactPresenter = interactPresenter;

        _dishPoints = new Transform[maxDishesCount];
        float pointsSpacing = 0.1f;

        for (int i = 0; i < _dishPoints.Length; i++)
        {
            Transform newPoint = new GameObject("DishPoint").GetComponent<Transform>();
            newPoint.SetParent(transform);
            newPoint.localPosition = _startDishesPoint.localPosition + new Vector3(0, pointsSpacing * i, 0);
            newPoint.localScale = Vector3.one;

            _dishPoints[i] = newPoint;
        }
    }

    public void Enable()
    {
        _interactPresenter.Enable();
        _interactPresenter.DishAdded += ShowDish;
        _interactPresenter.DishRemoved += RemoveDish;
    }

    public void Disable()
    {
        _interactPresenter.Disable();
    }

    public void Interact(PlayerObjectInteract objectInteractSystem)
    {
        _interactPresenter.Interact(objectInteractSystem);
    }

    public void TryAddDish(DishView dishView)
    {
        _interactPresenter.TryAddDish(dishView);
    }

    private void ShowDish(IPickable pickable)
    {
        pickable.SetParent(_dishPoints[_currentDishPoint]);
        _currentDishPoint++;
    }

    private void RemoveDish(IPickable pickable)
    {
        _currentDishPoint--;
    }
}
