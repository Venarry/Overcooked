using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CombineIngredientShower
{
    private MeshFilter _ingredientsModel;
    private Dictionary<KitchenObjectType[], Mesh> _modelsTemplate;

    public CombineIngredientShower(Transform point, Dictionary<KitchenObjectType[], Mesh> modelsTemplate)
    {
        _modelsTemplate = modelsTemplate;

        GameObject modelObject = new GameObject("Model");

        Material material = Resources.Load("Palette") as Material;
        modelObject.AddComponent<MeshRenderer>().material = material;

        modelObject.transform.position = point.position;
        modelObject.transform.parent = point;

        _ingredientsModel = modelObject.AddComponent<MeshFilter>();
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

        Debug.Log("searching");

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
