using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombineIngredientShower
{
    private readonly CookableHolderInteractPresenter _cookingPotInteractPresenter;
    private readonly MeshFilter _ingredientsModel;
    private readonly Dictionary<KitchenObjectType[], Mesh> _modelsTemplate;

    public CombineIngredientShower(Transform meshPoint, CookableHolderInteractPresenter cookingPanInteractPresenter, Dictionary<KitchenObjectType[], Mesh> modelsTemplate)
    {
        _cookingPotInteractPresenter = cookingPanInteractPresenter;
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
        _cookingPotInteractPresenter.CookableAdded += OnCookableChanged;
        _cookingPotInteractPresenter.CookableRemoved += OnCookableChanged;
    }

    private void OnCookableChanged()
    {
        RefreshModel(_cookingPotInteractPresenter.CookablesType);
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
