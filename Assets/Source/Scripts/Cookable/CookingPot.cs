using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(ObjectInteractive))]
[RequireComponent(typeof(CookingProcessModel))]
public class CookingPot : MonoBehaviour, ICookableHolder, ICookable, IPickable
{
    [SerializeField] private Transform _holdPoint;
    [SerializeField] private int _maxCookables = 1;

    private List<ICookable> _cookables = new List<ICookable>();

    private ObjectInteractive _interactive;
    private CookingProcessModel _cookingProcess;

    public int CookablesCount => _cookables.Count;
    public bool CanInteract => _interactive.HasParent == false;
    public KitchenObjectType Type => _cookingProcess.Type;
    public KitchenObjectType[] AvaiablePlaceTypes => _cookingProcess.AvailablePlaceTypes;

    private void Awake()
    {
        _interactive = GetComponent<ObjectInteractive>();
        _cookingProcess = GetComponent<CookingProcessModel>();
    }

    public void Interact(PlayerInteracter interactSystem)
    {
        if (interactSystem.HasPickable)
        {
            // если нет типа то нельзя взаимодействовать
            if (TryInteractWithInteracterHolder(interactSystem))
                return;

            if (TryGetCookableFromInteracter(interactSystem))
                return;
        }
        else
        {
            interactSystem.TryGivePickable(this);
        }
    }

    public bool CanPlace(KitchenObjectType type) => AvaiablePlaceTypes.Contains(type);

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

    private bool TryGetCookableFromInteracter(PlayerInteracter interactSystem)
    {
        if (interactSystem.TryGetPickableType(out ICookable cookable) == false)
            return false;

        if (interactSystem.CanPlacePickable(Type) == false)
            return false;

        if (_cookables.Count < _maxCookables == false)
            return false;

        interactSystem.RemovePickableRoot();
        TryAddCookable(cookable);

        return true;
    }

    private bool TryInteractWithInteracterHolder(PlayerInteracter interactSystem)
    {
        if (interactSystem.TryGetPickableType(out ICookableHolder cookableHolder) == false)
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
