using UnityEngine;

public class CuttingBoardCounter : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform _holdPoint;
    [SerializeField] private KitchenObjectType _type;

    private CounterInteractModel<ICookable> _counterModel;

    public bool CanInteract => _counterModel.CanInteract;

    private void Start()
    {
        _counterModel = new CounterInteractModel<ICookable>(_holdPoint, _type);
    }

    private void Update()
    {
        // обработка нажатия и приготовление
    }

    public void Interact(PlayerObjectInteract objectInteractSystem)
    {
        _counterModel.Interact(objectInteractSystem);
    }
}
