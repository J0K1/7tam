using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActionBar : MonoBehaviour
{
    [SerializeField] private ActionBarSlot[] slots;

    private readonly List<ActionBarSlot> _filledSlots = new List<ActionBarSlot>();

    public void AddFigure(Figure figure)
    {
        CheckLose();

        ActionBarSlot slotToFill = slots[_filledSlots.Count];
        slotToFill.Initialize(figure.Data);
        slotToFill.gameObject.SetActive(true);

        _filledSlots.Add(slotToFill);

        CheckLastThreeMatch();
        CheckLose();
    }

    private void CheckLastThreeMatch()
    {
        if (_filledSlots.Count < 3) return;

        var lastThree = _filledSlots.Skip(_filledSlots.Count - 3).Take(3).ToList();
        FigureData firstData = lastThree[0].Data;

        bool allMatch = lastThree.All(s =>
            s.Data.animal == firstData.animal &&
            s.Data.color == firstData.color && 
            s.Data.shape == firstData.shape);

        if (allMatch)
        {
            foreach (var matchedSlot in lastThree)
            {
                matchedSlot.Clear();
                _filledSlots.Remove(matchedSlot);
            }

            RepackSlots();
            Debug.Log("Match");
        }
    }

    private void RepackSlots()
    {
        for (int currentIndex = 0; currentIndex < slots.Length; currentIndex++)
        {
            if (currentIndex < _filledSlots.Count)
            {
                var slot = _filledSlots[currentIndex];
                slot.transform.SetSiblingIndex(currentIndex);
            }
            else
            {
                slots[currentIndex].Clear();
            }
        }
    }

    private void CheckLose()
    {
        if (_filledSlots.Count >= slots.Length)
        {
            GameManager.Instance.LoseGame();
        }
    }

    public void ClearAllSlots()
    {
        foreach (var slot in slots)
        {
            slot.Clear();
        }
        _filledSlots.Clear();
    }
}
