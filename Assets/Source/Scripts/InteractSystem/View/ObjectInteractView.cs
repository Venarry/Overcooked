using UnityEngine;

public class ObjectInteractView : MonoBehaviour
{
    private readonly Color _baseColor = Color.white;
    private readonly Color _targetColor = Color.gray;
    
    private Collider _currentObject;
    private InteractObjectFinder _objectFinder;

    public void Init(InteractObjectFinder objectFinder)
    {
        _objectFinder = objectFinder;
        _objectFinder.InteractObjectFound += ShowInteractObject;
    }

    private void FixedUpdate()
    {
        if (_objectFinder.TryFindInteractive(out _) != false || _currentObject == null) 
            return;
        
        SetColor(_currentObject, _baseColor);
        _currentObject = null;
    }

    private void ShowInteractObject(Collider collider)
    {
        if(collider == null)
            return;
        
        if(_currentObject != null)
            SetColor(_currentObject, _baseColor);
        
        _currentObject = collider;
        SetColor(_currentObject, _targetColor);
    }
    
    private void SetColor(Collider collider, Color color)
    {
        MeshRenderer[] meshRenderers = collider.GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer meshRenderer in meshRenderers)
        {
            meshRenderer.material.color = color;
        }
    }
}
