using UnityEngine;
using UnityEngine.UI;

public class OrderPanelView : MonoBehaviour
{
    private const float BaseSpacing = 70;

    [SerializeField] private Image _orderImage;
    [SerializeField] private Image _ingredientImagePrefab;
    [SerializeField] private Transform _ingredientsPoint;

    public void SetParent(Transform parent)
    {
        transform.SetParent(parent, false);
        transform.localScale = Vector3.one;
    }

    public void SetSprites(Sprite orderSprite, Sprite[] ingredientsSprites)
    {
        SetOrderImage(orderSprite);
        CreateIngredientsImages(ingredientsSprites);
    }

    private void SetOrderImage(Sprite sprite)
    {
        _orderImage.sprite = sprite;
    }

    private void CreateIngredientsImages(Sprite[] sprite)
    {
        float spriteCount = sprite.Length;
        float targetSpacing = BaseSpacing;
        Vector3 localScale = Vector3.one;

        float reduceMultiplier = 2.5f;

        if (spriteCount > 3)
        {
            float targetScaleMulltiplier = reduceMultiplier / spriteCount;

            localScale = new Vector3(targetScaleMulltiplier, targetScaleMulltiplier, targetScaleMulltiplier);
            targetSpacing = BaseSpacing * targetScaleMulltiplier;
        }

        float startPosition = Calculator.GetCenterdStartPoint(spriteCount, targetSpacing);

        for (int i = 0; i < spriteCount; i++)
        {
            Image ingredient = Instantiate(_ingredientImagePrefab);
            ingredient.transform.SetParent(_ingredientsPoint);
            ingredient.transform.localPosition = new Vector3(startPosition + targetSpacing * i, 0, 0);
            ingredient.sprite = sprite[i];
            ingredient.transform.localScale = localScale;
        }
    }
}
