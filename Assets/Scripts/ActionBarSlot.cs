using UnityEngine;
using UnityEngine.UI;

public class ActionBarSlot : MonoBehaviour
{
    [SerializeField] private Image _iconImage;
    [SerializeField] private Image _shapeImage;
    [SerializeField] private GameObject _background;

    public FigureData Data { get; private set; }

    public void Initialize(FigureData data)
    {
        Data = data;

        _shapeImage.gameObject.SetActive(true);
        _iconImage.gameObject.SetActive(true);
        _background.SetActive(false);

        string shapePath = $"Shapes/{Data.shape}_{Data.color}";
        Sprite shapeSprite = Resources.Load<Sprite>(shapePath);
        if (shapeSprite == null)
        {
            Debug.Log("Shape sprite not found");
            Debug.Log("Shape path: " + shapePath);
        }
        _shapeImage.sprite = shapeSprite;

        string iconPath = $"Icons/{Data.animal}";
        Sprite iconSprite = Resources.Load<Sprite>(iconPath);
        if (iconSprite == null)
        {
            Debug.Log("Icon sprite not found");
            Debug.Log("Icon path: " + iconPath);

        }
        _iconImage.sprite = iconSprite;
        _iconImage.SetNativeSize();
    }

    public void Clear()
    {
        _shapeImage.gameObject.SetActive(false);
        _iconImage.gameObject.SetActive(false);
        _background.SetActive(true);
    }
}
