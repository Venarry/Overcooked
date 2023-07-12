using UnityEngine;

public class CookStageMeshShower
{
    private MeshFilter _model;

    public CookStageMeshShower(MeshFilter model)
    {
        _model = model;
    }

    public void ShowMesh(Mesh mesh)
    {
        _model.mesh = mesh;
    }
}
