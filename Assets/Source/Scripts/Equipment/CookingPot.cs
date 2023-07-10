using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(InteractiveObject))]
[RequireComponent(typeof(CookingProcess))]
public class CookingPot : MonoBehaviour, ICookableHolder, ICookable, IPickable
{
    [SerializeField] private Transform _holdPoint;
    [SerializeField] private int _maxCookables = 1;

    private List<ICookable> _cookables = new List<ICookable>();

    private InteractiveObject _interactive;
    private CookingProcess _cookingProcess;

    public int CookablesCount => _cookables.Count;
    public bool CanInteract => _interactive.HasParent == false;
    public KitchenObjectType Type => _cookingProcess.Type;
    public KitchenObjectType[] AvailablePlaceTypes => _cookingProcess.AvailablePlaceTypes;

    private void Awake()
    {
        _interactive = GetComponent<InteractiveObject>();
        _cookingProcess = GetComponent<CookingProcess>();
    }

    public void Interact(PlayerObjectInteract objectInteractSystem)
    {
        if (objectInteractSystem.HasPickable)
        {
            // если нет типа то нельзя взаимодействовать
            if (TryInteractWithInteracterHolder(objectInteractSystem))
                return;

            if (TryGetCookableFromInteracter(objectInteractSystem))
                return;
        }
        else
        {
            objectInteractSystem.TryGivePickable(this);
        }
    }

    public bool CanPlace(KitchenObjectType type) => AvailablePlaceTypes.Contains(type);

    public void SetParent(Transform point)
    {
        _interactive.SetParent(point);
    }

    public void RemoveParent()
    {
        _interactive.RemoveParent();
    }

    public void Cook(float step = 0)
    {
        if(_cookables.Count > 0)
            _cookingProcess.CookNextStep();
    }

    public bool TryAddCookable(ICookable cookable)
    {
        if (cookable.CanPlace(Type) == false)
            return false;

        if (_cookables.Count >= _maxCookables)
            return false;

        if (cookable is IPickable pickable)
        {
            pickable.SetParent(_holdPoint);
        }

        _cookables.Add(cookable);

        return true;
    }

    public void GiveCookablesInOutHolder(ICookableHolder cookableHolder)
    {
        int startCounter = _cookables.Count - 1;

        for (int i = startCounter; i >= 0; i--)
        {
            if (cookableHolder.TryAddCookable(_cookables[i]))
            {
                _cookables.Remove(_cookables[i]);
            }
        }
    }

    private bool TryGetCookableFromInteracter(PlayerObjectInteract objectInteractSystem)
    {
        if (objectInteractSystem.TryGetPickableType(out ICookable cookable) == false)
            return false;

        if (objectInteractSystem.CanPlacePickable(Type) == false)
            return false;

        if (_cookables.Count < _maxCookables == false)
            return false;

        objectInteractSystem.RemovePickableRoot();
        TryAddCookable(cookable);

        return true;
    }

    private bool TryInteractWithInteracterHolder(PlayerObjectInteract objectInteractSystem)
    {
        if (objectInteractSystem.TryGetPickableType(out ICookableHolder cookableHolder) == false)
            return false;

        if (cookableHolder.CookablesCount == 0)
        {
            GiveCookablesInOutHolder(cookableHolder);
        }
        else
        {
            cookableHolder.GiveCookablesInOutHolder(this);
        }

        return true;
    }
}
