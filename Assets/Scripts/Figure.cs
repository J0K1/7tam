using System;
using UnityEngine;

public class Figure : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _iconRenderer;
    [SerializeField] private SpriteRenderer _shapeRenderer;

    [NonSerialized] public bool isClickable = true;
    public FigureData Data { get; private set; }

    public void Initialize(FigureData newData)
    {
        Data = newData;

        string shapePath = $"Shapes/{Data.shape}_{Data.color}";
        Sprite shapeSprite = Resources.Load<Sprite>(shapePath);
        if (shapeSprite == null)
        {
            Debug.Log("Shape sprite not found");
            Debug.Log("Shape path: " + shapePath);
        }
        _shapeRenderer.sprite = shapeSprite;

        string iconPath = $"Icons/{Data.animal}";
        Sprite iconSprite = Resources.Load<Sprite>(iconPath);
        if (iconSprite == null)
        {
            Debug.Log("Icon sprite not found");
            Debug.Log("Icon path: " + iconPath);

        }
        _iconRenderer.sprite = iconSprite;
    }

    private void OnMouseDown()
    {
        if (!isClickable)
            return;

        GameManager.Instance.OnFigureClicked(this);
    }
}
