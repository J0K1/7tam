using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField, Tooltip("Префаб фигуры для спавна")]
    private GameObject _figurePrefab;

    [SerializeField, Tooltip("Где будут появляться фигуры")]
    private Transform _spawnParent;

    [Header("Настройки поля")]
    [SerializeField, Tooltip("Максимальное количество фигур (будет скорректировано до кратного 3)")]
    private int _maxFigureCount = 30;

    [SerializeField, Range(0.05f, 1f), Tooltip("Задержка между спавнами отдельных фигур")]
    private float _spawnDelay = 0.25f;

    [Header("Сила толчока")]
    [SerializeField, Tooltip("Сила импульса при появлении фигуры")]
    private float _impulseStrength = 5f;

    private readonly List<Figure> _figures = new List<Figure>();

    private List<FigureData> _tripleFigureData;
    private const int _figuresPerGroup = 3;

    public bool IsEmpty => _figures.Count == 0;

    private void Awake()
    {
        if (_figurePrefab == null)
            Debug.LogError($"FigurePrefab не задан в инспекторе.");

        if (_spawnParent == null)
            Debug.LogError($"SpawnParent не задан в инспекторе.");
    }

    public void GenerateField()
    {
        ClearExistingFigures();

        GenerateTripleDataList();
        StartCoroutine(SpawnWithGravityCoroutine());
    }

    private void ClearExistingFigures()
    {
        for (int currentIndex = _figures.Count - 1; currentIndex >= 0; currentIndex--)
        {
            Figure fig = _figures[currentIndex];
            if (fig != null)
                Destroy(fig.gameObject);
        }
        _figures.Clear();
    }

    private void GenerateTripleDataList()
    {
        _tripleFigureData = new List<FigureData>();

        int remainder = _maxFigureCount % _figuresPerGroup;
        if (remainder != 0)
            _maxFigureCount += _figuresPerGroup - remainder;

        int uniqueCount = _maxFigureCount / _figuresPerGroup;

        for (int currentUniqueCount = 0; currentUniqueCount < uniqueCount; currentUniqueCount++)
        {
            var data = CreateRandomFigureData();

            for (int currentCount = 0; currentCount < _figuresPerGroup; currentCount++)
                _tripleFigureData.Add(data);
        }

        ShuffleList(_tripleFigureData);
    }
    private FigureData CreateRandomFigureData()
    {
        var allShapes = Enum.GetValues(typeof(ShapeType)).Cast<ShapeType>().ToArray();
        var allAnimals = Enum.GetValues(typeof(AnimalType)).Cast<AnimalType>().ToArray();
        var allColors = Enum.GetValues(typeof(ShapeColor)).Cast<ShapeColor>().ToArray();

        return new FigureData
        {
            Shape = allShapes[UnityEngine.Random.Range(0, allShapes.Length)],
            Animal = allAnimals[UnityEngine.Random.Range(0, allAnimals.Length)],
            Color = allColors[UnityEngine.Random.Range(0, allColors.Length)]
        };
    }

    private IEnumerator SpawnWithGravityCoroutine()
    {
        if (_figurePrefab == null || _spawnParent == null)
            yield break;

        foreach (var data in _tripleFigureData)
        {
            Vector2 spawnPosition = _spawnParent.position;
            GameObject go = Instantiate(_figurePrefab, spawnPosition, Quaternion.identity, _spawnParent);
            Figure figureComponent = go.GetComponent<Figure>();

            if (figureComponent != null)
            {
                figureComponent.Initialize(data);
                _figures.Add(figureComponent);
            }
            else
            {
                Debug.LogError($"У префаба нет компонента Figure.");
            }

            Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                float randomX = UnityEngine.Random.Range(-1f, 1f);
                float randomY = UnityEngine.Random.Range(-0.5f, 0f);
                Vector2 impulse = new Vector2(randomX, randomY) * _impulseStrength;
                rb.AddForce(impulse, ForceMode2D.Impulse);
            }

            yield return new WaitForSeconds(_spawnDelay);
        }
    }

    public void RemoveFigure(Figure figure)
    {
        if (figure == null)
            return;

        if (_figures.Remove(figure))
        {
            Destroy(figure.gameObject);
        }
        else
        {
            Debug.LogWarning($"Попытка удалить фигуру, которой нет в списке.");
            Destroy(figure.gameObject);
        }
    }

    public void Shuffle()
    {
        StopAllCoroutines();

        int currentCount = _figures.Count;

        ClearExistingFigures();

        int remainder = currentCount % _figuresPerGroup;
        if (remainder != 0)
        {
            currentCount += _figuresPerGroup - remainder;
        }

        _maxFigureCount = Mathf.Max(currentCount, _figuresPerGroup);

        GenerateField();
    }


    private void ShuffleList(List<FigureData> list)
    {
        int count = list.Count;
        for (int currentIndex = 0; currentIndex < count - 1; currentIndex++)
        {
            int randomIndex = UnityEngine.Random.Range(currentIndex, count);
            FigureData tempFigure = list[currentIndex];
            list[currentIndex] = list[randomIndex];
            list[randomIndex] = tempFigure;
        }
    }
}
