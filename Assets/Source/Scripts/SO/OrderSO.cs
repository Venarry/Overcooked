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

        foreach (KitchenObjectType type in inputIngredients)
        {
            if(_orderTypes.Contains(type) == false)
            {
                return false;
            }
        }

        return true;
    }
}
