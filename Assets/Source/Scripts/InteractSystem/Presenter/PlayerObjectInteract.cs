using UnityEngine;

[RequireComponent(typeof(ObjectInteractView))]
public class PlayerObjectInteract : MonoBehaviour
{
    [SerializeField] private KeyCode _interactKey = KeyCode.E;
    //[SerializeField] private KeyCode _alternateInteractKey = KeyCode.R;
    [SerializeField] private KeyCode _dropKey = KeyCode.G;
    [SerializeField] private float _interactRange = 2f;
    [SerializeField] private Transform _grabTransform;

    private IPickable _pickable;
    private InteractObjectFinder _interactObjectFinder;
    private ObjectInteractView _objectInteractView;

    public bool HasPickable => _pickable != null;

    private void Awake()
    {
        _objectInteractView = GetComponent<ObjectInteractView>();
        _interactObjectFinder = new InteractObjectFinder(_grabTransform, _interactRange);
        _objectInteractView.Init(_interactObjectFinder);
    }

    private void Update()
    {
        if (Input.GetKeyDown(_interactKey))
        {
            Interact();
        }

        if (Input.GetKeyDown(_dropKey))
        {
            TryDrop();
        }
    }

    public bool TryGetPickableType<T>(out T component)
    {
        component = default;

        if (_pickable == null)
            return false;

        if (_pickable is T type)
        {
            component = type;
            return true;
        }

        return false;
    }

    public bool CanPlacePickable(KitchenObjectType holderType) => 
        _pickable?.CanPlace(holderType) ?? true;

    public void TryRemoveIPickable()
    {
        if (_pickable == null)
            return;

        _pickable.RemoveParent();
        _pickable = null;
    }

    public bool TryTakePickable(out IPickable pickable)
    {
        pickable = null;

        if (HasPickable)
        {
            pickable = _pickable;
            _pickable = null;
            return true;
        }

        return false;
    }

    public bool TryGivePickable(IPickable pickable)
    {
        if(_pickable == null)
        {
            SetFood(pickable);
            return true;
        }

        return false;
    }

    public void RemovePickableRoot()
    {
        _pickable = null;
    }

    private void Interact()
    {
        if(_interactObjectFinder.TryFindInteractive(out IInteractable interactable))
        {
            interactable.Interact(this);
            return;
        }

        TryDrop();
    }

    private void TryDrop()
    {
        if (_pickable == null)
        {
            return;
        }

        _pickable.RemoveParent();
        _pickable = null;
    }

    private void SetFood(IPickable pickable)
    {
        _pickable = pickable;
        _pickable.SetParent(_grabTransform);
    }
}
