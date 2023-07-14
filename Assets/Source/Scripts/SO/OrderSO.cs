using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New order", menuName = "Order/Create new order")]
public class OrderSO : ScriptableObject
{
    [SerializeField] private KitchenObjectType[] _orderTypes;
    [SerializeField] private Sprite[] _ingredientIcons;
    [SerializeField] private Sprite _orderImage;

    public Texture[] IngredientTextures => _ingredientIcons.Select(currentSprite => currentSprite.texture).ToArray();
    public Texture OrderTexture => _orderImage.texture;

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
