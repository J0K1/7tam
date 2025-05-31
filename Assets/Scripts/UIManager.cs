using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Панели")]
    [SerializeField, Tooltip("Панель экрана победы")]
    private GameObject _winPanel;

    [SerializeField, Tooltip("Панель экрана поражения")]
    private GameObject _losePanel;

    [Space]
    [SerializeField, Tooltip("Кнопка пересброса")]
    private Button _shuffleButton;

    [Header("Настройки анимации")]
    [SerializeField, Range(0.1f, 1f), Tooltip("Длительность анимации открытия панели")]
    private float _animationDuration = 0.5f;

    [SerializeField, Tooltip("Кривая анимации масштабирования панели")]
    private AnimationCurve _scaleCurve;

    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void ShowWinScreen()
    {
        ShowPanel(_winPanel);
    }

    public void ShowLoseScreen()
    {
        ShowPanel(_losePanel);
    }

    private void ShowPanel(GameObject panel)
    {
        if (panel == null)
        {
            Debug.LogError($"Ссылка на панель не установлена.");
            return;
        }

        if (_shuffleButton != null)
            _shuffleButton.interactable = false;
        else
            Debug.LogWarning($"Cсылка на кнопку пересброса не установлена.");

        panel.SetActive(true);
        panel.transform.localScale = Vector3.zero;

        panel.transform
            .DOScale(Vector3.one, _animationDuration)
            .SetEase(_scaleCurve);

        AudioSource audioSource = panel.GetComponent<AudioSource>();
        if (audioSource != null)
            audioSource.Play();
        else
            Debug.LogWarning($"Отсутствует AudioSource на панели");
    }
}
