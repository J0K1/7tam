using System.Collections.Generic;
using UnityEngine;

public class ActionBar : MonoBehaviour
{
    [Header("Настройки слотов")]
    [SerializeField, Tooltip("Слоты, в которые будут добавляться фигуры")]
    private ActionBarSlot[] _slots;

    [Header("Аудио")]
    [SerializeField, Tooltip("Звук при совпадении трёх фигур")]
    private AudioSource _matchAudioSource;

    [SerializeField, Tooltip("Звук при добавлении фигуры в слот")]
    private AudioSource _addAudioSource;

    private readonly List<ActionBarSlot> _filledSlots = new List<ActionBarSlot>();

    private bool _isLocked = false;

    public bool AddFigure(Figure figure)
    {
        if (_isLocked)
            return false;

        if (figure == null)
        {
            Debug.LogError($"Figure == null.");
            return false;
        }

        if (_filledSlots.Count >= _slots.Length)
        {
            HandleLose();
            return false;
        }

        ActionBarSlot targetSlot = _slots[_filledSlots.Count];
        targetSlot.Initialize(figure.Data);
        targetSlot.gameObject.SetActive(true);

        _filledSlots.Add(targetSlot);
        PlayAddSound();

        CheckForMatch();
        CheckForLose();

        return true;
    }

    private void CheckForMatch()
    {
        if (_filledSlots.Count < 3)
            return;

        int count = _filledSlots.Count;
        ActionBarSlot slotA = _filledSlots[count - 3];
        ActionBarSlot slotB = _filledSlots[count - 2];
        ActionBarSlot slotC = _filledSlots[count - 1];

        FigureData dataA = slotA.Data;
        FigureData dataB = slotB.Data;
        FigureData dataC = slotC.Data;

        bool isMatch = dataA.IsPartialMatch(dataB) && dataA.IsPartialMatch(dataC);

        if (!isMatch)
            return;

        slotA.Clear();
        slotB.Clear();
        slotC.Clear();

        _filledSlots.RemoveAt(count - 1);
        _filledSlots.RemoveAt(count - 2);
        _filledSlots.RemoveAt(count - 3);

        RepackSlots();
        PlayMatchSound();
    }

    private void RepackSlots()
    {
        for (int currentIndex = 0; currentIndex < _slots.Length; currentIndex++)
        {
            if (currentIndex < _filledSlots.Count)
            {
                ActionBarSlot slot = _filledSlots[currentIndex];
                slot.transform.SetSiblingIndex(currentIndex);
            }
            else
            {
                _slots[currentIndex].Clear();
            }
        }
    }

    private void CheckForLose()
    {
        if (_filledSlots.Count < _slots.Length)
            return;

        HandleLose();
    }

    private void HandleLose()
    {
        if (_isLocked)
            return;

        _isLocked = true;
        GameManager.Instance?.LoseGame();
    }

    public void ClearAllSlots()
    {
        foreach (ActionBarSlot slot in _slots)
        {
            slot.Clear();
        }

        _filledSlots.Clear();
        _isLocked = false;
    }

    private void PlayAddSound()
    {
        if (_addAudioSource != null)
        {
            _addAudioSource.Play();
        }
        else
        {
            Debug.LogWarning($"AddAudioSource не установлен.");
        }
    }

    private void PlayMatchSound()
    {
        if (_matchAudioSource != null)
        {
            _matchAudioSource.Play();
        }
        else
        {
            Debug.LogWarning($"MatchAudioSource не установлен.");
        }
    }
}
