using UnityEngine;

public class CanvasObjectRotator : MonoBehaviour
{
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void LateUpdate()
    {
        transform.rotation = _camera.transform.rotation;
    }
}
