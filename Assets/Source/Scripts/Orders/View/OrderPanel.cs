using UnityEngine;
using UnityEngine.UI;

public class OrderPanel : MonoBehaviour
{
    [SerializeField] private Image _orderImage;
    [SerializeField] private Image[] _ingredientsImages;

    public void SetOrderImage(Sprite sprite)
    {
        _orderImage.sprite = sprite;
    }

    public void SetIngredientsImages(Sprite[] sprite)
    {
        for (int i = 0; i < sprite.Length; i++)
        {
            _ingredientsImages[i].sprite = sprite[i];
        }
    }
}
