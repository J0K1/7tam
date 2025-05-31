using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ActionBar actionBar;
    [SerializeField] private Spawner spawner;

    [NonSerialized] public static GameManager Instance;

    private void Awake() => Instance = this;

    private void Start() => StartLevel();

    private void StartLevel()
    {
        spawner.GenerateField();
    }

    public void OnFigureClicked(Figure figure)
    {
        actionBar.AddFigure(figure);
        spawner.RemoveFigure(figure);
        CheckWin();
    }

    public void LoseGame()
    {
        UIManager.Instance.ShowLose();
        Debug.Log("Lose");
    }

    private void WinGame()
    {
        UIManager.Instance.ShowWin();
        Debug.Log("Win");
    }

    private void CheckWin()
    {
        if(spawner.IsEmpty)
            WinGame();
    }

    public void ShuffleField()
    {
        spawner.Shuffle();
        actionBar.ClearAllSlots();
    }

    public void RestartScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
