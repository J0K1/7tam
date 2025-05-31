using DG.Tweening;
using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _loseScreen;
    [SerializeField, Range(0.1f, 1f)] private float _animationDuration = 0.5f;
    [SerializeField] private AnimationCurve _scaleCurve;

    [NonSerialized] public static UIManager Instance;

    private void Awake() => Instance = this;

    public void ShowWin()
    {
        ShowPanel(_winScreen);
    }

    public void ShowLose()
    {
        ShowPanel(_loseScreen);
    }

    private void ShowPanel(GameObject panel)
    {
        panel.SetActive(true);
        panel.transform.localScale = Vector3.zero;

        panel.transform
            .DOScale(Vector2.one, _animationDuration)
            .SetEase(_scaleCurve);
    }
}
