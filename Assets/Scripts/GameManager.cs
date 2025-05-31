using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Зависимости")]
    [SerializeField, Tooltip("Для добавления фигур в слот")]
    private ActionBar _actionBar;

    [SerializeField, Tooltip("Для генерации и управления фигурами")]
    private Spawner _spawner;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        if (_actionBar == null)
            Debug.LogError($"Не задан ActionBar в инспекторе.");
        if (_spawner == null)
            Debug.LogError($"Не задан Spawner в инспекторе.");
    }

    private void Start()
    {
        StartLevel();
    }

    private void StartLevel()
    {
        if (_spawner == null)
            return;

        _spawner.GenerateField();
    }

    public void OnFigureClicked(Figure figure)
    {
        if (figure == null)
            return;

        if (_actionBar == null || !_actionBar.AddFigure(figure))
            return;

        if (_spawner != null)
            _spawner.RemoveFigure(figure);

        CheckWinCondition();
    }

    private void CheckWinCondition()
    {
        if (_spawner != null && _spawner.IsEmpty)
        {
            WinGame();
        }
    }

    public void LoseGame()
    {
        UIManager.Instance?.ShowLoseScreen();
    }

    private void WinGame()
    {
        UIManager.Instance?.ShowWinScreen();
    }

    public void ShuffleField()
    {
        if (_spawner == null)
            return;

        _spawner.Shuffle();

        if (_actionBar != null)
            _actionBar.ClearAllSlots();
    }

    public void RestartScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
