using System;
using UnityEngine;
using UnityEngine.UI;

public class ActionBarSlot : MonoBehaviour
{
    [Header("Отображение фигур")]
    [SerializeField] private Image _iconImage;
    [SerializeField] private Image _shapeImage;
    [SerializeField] private GameObject _background;

    private const string ShapesFolder = "Shapes";
    private const string IconsFolder = "Icons";

    public FigureData Data { get; private set; }

    public void Initialize(FigureData data)
    {
        if (data == null)
            throw new ArgumentNullException(nameof(data));

        Data = data;

        _shapeImage.gameObject.SetActive(true);
        _iconImage.gameObject.SetActive(true);
        _background.SetActive(false);

        _shapeImage.sprite = LoadSprite(
            $"{ShapesFolder}/{Data.Shape}_{Data.Color}",
            $"Shape sprite not found for path: '{ShapesFolder}/{Data.Shape}_{Data.Color}'"
        );

        _iconImage.sprite = LoadSprite(
            $"{IconsFolder}/{Data.Animal}",
            $"Icon sprite not found for path: '{IconsFolder}/{Data.Animal}'"
        );

        if (_iconImage.sprite != null)
            _iconImage.SetNativeSize();
    }

    public void Clear()
    {
        _shapeImage.gameObject.SetActive(false);
        _iconImage.gameObject.SetActive(false);
        _background.SetActive(true);
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
            Debug.LogWarning(warningMessage);

        return sprite;
    }
}
