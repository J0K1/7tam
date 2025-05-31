using System;
using UnityEngine;

public class Figure : MonoBehaviour
{
    [Header("Sprite Renderers")]
    [SerializeField] private SpriteRenderer _iconRenderer;
    [SerializeField] private SpriteRenderer _shapeRenderer;

    private const string ShapesFolder = "Shapes";
    private const string IconsFolder = "Icons";

    public bool IsClickable { get; set; } = true;
    public FigureData Data { get; private set; }

    public void Initialize(FigureData newData)
    {
        Data = newData ?? throw new ArgumentNullException(nameof(newData));

        _shapeRenderer.sprite = LoadSprite(
            $"{ShapesFolder}/{Data.Shape}_{Data.Color}",
            $"Shape sprite not found for path: '{ShapesFolder}/{Data.Shape}_{Data.Color}'"
        );

        _iconRenderer.sprite = LoadSprite(
            $"{IconsFolder}/{Data.Animal}",
            $"Icon sprite not found for path: '{IconsFolder}/{Data.Animal}'"
        );
    }

    private void OnMouseDown()
    {
        if (!IsClickable)
            return;

        GameManager.Instance.OnFigureClicked(this);
    }

    private Sprite LoadSprite(string resourcePath, string warningMessage)
    {
        if (string.IsNullOrEmpty(resourcePath))
        {
            Debug.LogWarning($"LoadSprite: путь не может быть пустым.");
            return null;
        }

        Sprite sprite = Resources.Load<Sprite>(resourcePath);
        if (sprite == null)
        {
            Debug.LogWarning(warningMessage);
        }
        return sprite;
    }
}
