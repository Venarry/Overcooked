using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New order", menuName = "Order/Create new order")]
public class OrderSO : ScriptableObject
{
    [SerializeField] private KitchenObjectType[] _orderTypes;
    [SerializeField] private Sprite[] _ingredientIcons;
    [SerializeField] private Sprite _orderImage;

    public Sprite[] IngredientTextures => _ingredientIcons.ToArray();
    public Sprite OrderImage => _orderImage;

    public bool CorrectOrder(KitchenObjectType[] inputIngredients)
    {
        if (inputIngredients.Length != _orderTypes.Length)
            return false;

        List<KitchenObjectType> bufferTypes = new(_orderTypes);

        foreach (KitchenObjectType type in inputIngredients)
        {
            if(bufferTypes.Contains(type) == false) // починить
            {
                return false;
            }

            bufferTypes.Remove(type);
        }

        return true;
    }
}
