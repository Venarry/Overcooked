using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombineIngredientMeshView
{
    private readonly CookableHolderInteractPresenter _interactPresenter;
    private readonly MeshFilter _ingredientsModel;
    private readonly Dictionary<KitchenObjectType[], Mesh> _modelsTemplate;

    public CombineIngredientMeshView(Transform meshPoint, CookableHolderInteractPresenter interactPresenter, Dictionary<KitchenObjectType[], Mesh> modelsTemplate)
    {
        _interactPresenter = interactPresenter;
        _modelsTemplate = modelsTemplate;

        GameObject modelObject = new("Model");

        Material material = Resources.Load<Material>(AssetsPath.PaletteMaterial);
        modelObject.AddComponent<MeshRenderer>().material = material;

        modelObject.transform.parent = meshPoint;
        modelObject.transform.position = meshPoint.position;

        _ingredientsModel = modelObject.AddComponent<MeshFilter>();
    }

    public void Enable()
    {
        _interactPresenter.CookableAdded += OnCookableChanged;
        _interactPresenter.CookableRemoved += OnCookableChanged;
    }

    private void OnCookableChanged()
    {
        RefreshModel(_interactPresenter.CookablesType);
    }

    public void RefreshModel(KitchenObjectType[] inputTypes)
    {
        if (inputTypes.Length == 0)
        {
            _ingredientsModel.sharedMesh = null;
            return;
        }

        if (_modelsTemplate.Count == 1)
        {
            _ingredientsModel.sharedMesh = _modelsTemplate.ElementAt(0).Value;
            return;
        }

        foreach (KeyValuePair<KitchenObjectType[], Mesh> modelTemplate in _modelsTemplate)
        {
            if(modelTemplate.Key.Length != inputTypes.Length)
                continue;

            foreach (KitchenObjectType inputModel in inputTypes)
            {
                if(modelTemplate.Key.Contains(inputModel) == false)
                {
                    break;
                }

                _ingredientsModel.sharedMesh = modelTemplate.Value;
            }
        }
    }
}
