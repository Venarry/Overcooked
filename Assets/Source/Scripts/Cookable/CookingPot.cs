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

    public int MaxCookables => _maxCookables;
    public int CookablesCount => _cookables.Count;
    public bool CanInteract => _interactive.HasParent == false;
    public KitchenObjectType Type => _cookingProcess.Type;
    public KitchenObjectType[] AvaiablePlaceTypes => _cookingProcess.AvailablePlaceTypes;

    private void Awake()
    {
        _interactive = GetComponent<ObjectInteractive>();
        _cookingProcess = GetComponent<CookingProcessModel>();
    }

    public void Interact(InteractSystem interactSystem)
    {
        if (interactSystem.HasPickable)
        {
            if (TryInteractWithHolder(interactSystem))
                return;

            if (TryGetCookableIntoInteract(interactSystem))
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
        if (_cookables.Count >= _maxCookables)
            return false;

        _cookables.Add(cookable);

        if (cookable is IPickable pickable)
        {
            pickable.SetParent(_holdPoint);
        }

        return true;
    }

    public bool TryAddCookables(ICookable[] cookables)
    {
        foreach (ICookable cookable in cookables)
        {
            if (TryAddCookable(cookable) == false)
            {
                return false;
            }
        }

        return true;
    }

    public ICookable[] TakeCookables()
    {
        if (_cookables.Count == 0)
            return new ICookable[0];

        ICookable[] cookables = _cookables.ToArray();
        _cookables.Clear();
        _cookingProcess.ResetStages();

        return cookables;
    }

    private bool TryGetCookableIntoInteract(InteractSystem interactSystem)
    {
        if (interactSystem.CanPlacePickable(Type) == false)
            return false;

        if (interactSystem.TryGetPickableType(out ICookable cookable) == false)
            return false;

        if (_cookables.Count < _maxCookables == false)
            return false;

        interactSystem.TryTakePickable(out IPickable pickable);
        TryAddCookable(cookable);

        return true;
    }

    private bool TryInteractWithHolder(InteractSystem interactSystem)
    {
        if (interactSystem.TryGetPickableType(out ICookableHolder cookableHolder) == false)
            return false;

        if (cookableHolder.CookablesCount == 0)
        {
            GiveCookablesInOutHolder(cookableHolder);
        }
        else
        {
            if (_maxCookables >= cookableHolder.CookablesCount + CookablesCount)
                TryAddCookables(cookableHolder.TakeCookables());
        }

        return true;
    }

    private void GiveCookablesInOutHolder(ICookableHolder cookableHolder)
    {
        for (int i = _cookables.Count - 1; i >= 0; i--)
        {
            if (cookableHolder.TryAddCookable(_cookables[i]))
            {
                _cookables.Remove(_cookables[i]);
            }
        }

        /*if (cookableHolder.MaxCookables >= cookableHolder.CookablesCount + _cookables.Count)
                    {
                        if (cookableHolder.TryAddCookables(_cookables.ToArray()))
                        {
                            _cookables.Clear();
                            return;
                        }
                    }*/
    }
}
