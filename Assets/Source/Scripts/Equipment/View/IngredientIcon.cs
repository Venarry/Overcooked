using UnityEngine;
using UnityEngine.UI;

public class IngredientIcon : MonoBehaviour
{
    [SerializeField] private Image _image;

    public void SetImage(Sprite sprite)
    {
        _image.sprite = sprite;
    }
}
