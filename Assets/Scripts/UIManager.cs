using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("������")]
    [SerializeField, Tooltip("������ ������ ������")]
    private GameObject _winPanel;

    [SerializeField, Tooltip("������ ������ ���������")]
    private GameObject _losePanel;

    [Space]
    [SerializeField, Tooltip("������ ����������")]
    private Button _shuffleButton;

    [Header("��������� ��������")]
    [SerializeField, Range(0.1f, 1f), Tooltip("������������ �������� �������� ������")]
    private float _animationDuration = 0.5f;

    [SerializeField, Tooltip("������ �������� ��������������� ������")]
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
            Debug.LogError($"������ �� ������ �� �����������.");
            return;
        }

        if (_shuffleButton != null)
            _shuffleButton.interactable = false;
        else
            Debug.LogWarning($"C����� �� ������ ���������� �� �����������.");

        panel.SetActive(true);
        panel.transform.localScale = Vector3.zero;

        panel.transform
            .DOScale(Vector3.one, _animationDuration)
            .SetEase(_scaleCurve);

        AudioSource audioSource = panel.GetComponent<AudioSource>();
        if (audioSource != null)
            audioSource.Play();
        else
            Debug.LogWarning($"����������� AudioSource �� ������");
    }
}
