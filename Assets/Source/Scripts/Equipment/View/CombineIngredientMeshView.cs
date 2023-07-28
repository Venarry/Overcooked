using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombineIngredientMeshView
{
    private readonly CookableHolderInteractPresenter _interactPresenter;
    private readonly MeshFilter _ingredientMeshFilter;
    private readonly Dictionary<KitchenObjectType[], Mesh> _modelsTemplate;

    public CombineIngredientMeshView(Transform meshPoint, CookableHolderInteractPresenter interactPresenter, Dictionary<KitchenObjectType[], Mesh> modelsTemplate)
    {
        _interactPresenter = interactPresenter;
        _modelsTemplate = modelsTemplate;

        GameObject modelObject = new("Model");

        Material material = new PaletteFactory().Create();
        modelObject.AddComponent<MeshRenderer>().material = material;

        modelObject.transform.parent = meshPoint;
        modelObject.transform.position = meshPoint.position;

        _ingredientMeshFilter = modelObject.AddComponent<MeshFilter>();

        _interactPresenter.Enabled += Enable;
        _interactPresenter.Disabled += Disable;
    }

    private void Enable()
    {
        _interactPresenter.CookableAdded += OnCookableChanged;
        _interactPresenter.CookableRemoved += OnCookableChanged;
    }

    private void Disable()
    {
        _interactPresenter.CookableAdded -= OnCookableChanged;
        _interactPresenter.CookableRemoved -= OnCookableChanged;
    }

    private void OnCookableChanged()
    {
        RefreshModel(_interactPresenter.CookablesType);
    }

    public void RefreshModel(KitchenObjectType[] inputTypes)
    {
        if (inputTypes.Length == 0)
        {
            _ingredientMeshFilter.sharedMesh = null;
            return;
        }

        if (_modelsTemplate.Count == 1)
        {
            _ingredientMeshFilter.sharedMesh = _modelsTemplate.ElementAt(0).Value;
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

                _ingredientMeshFilter.sharedMesh = modelTemplate.Value;
            }
        }
    }
}
