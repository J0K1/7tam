using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _figurePrefab;
    [SerializeField] private Transform _spawnArea;
    [SerializeField] private int _maxFigureCount = 30;
    [SerializeField] private float _spawnDelay = 0.25f;
    [SerializeField] private float _impulseStrength = 2f;

    private List<Figure> _figures;
    private List<FigureData> _tripleFigureData;

    public bool IsEmpty => _figures != null && _figures.Count == 0;

    private void Awake()
    {
        _figures = new List<Figure>();
    }

    public void GenerateField()
    {
        foreach (var fig in _figures)
            if (fig != null)
                Destroy(fig.gameObject);

        _figures.Clear();

        GenerateTripleFigures();
        StartCoroutine(SpawnWithGravity());
    }

    private void GenerateTripleFigures()
    {
        _tripleFigureData = new List<FigureData>();
        int uniqueCount = _maxFigureCount / 3;

        for (int currentIndex = 0; currentIndex < uniqueCount; currentIndex++)
        {
            FigureData data = new FigureData
            {
                shape = (ShapeType)Random.Range(0, 3),
                animal = (AnimalType)Random.Range(0, 3),
                color = (ShapeColor)Random.Range(0, 3) 
            };
            _tripleFigureData.AddRange(Enumerable.Repeat(data, 3));
        }

        _tripleFigureData = _tripleFigureData.OrderBy(x => Random.value).ToList();
    }

    private IEnumerator SpawnWithGravity()
    {
        foreach (var data in _tripleFigureData)
        {
            Vector2 spawnPosition = _spawnArea.position;
            GameObject go = Instantiate(_figurePrefab, spawnPosition, Quaternion.identity, _spawnArea);
            Figure fig = go.GetComponent<Figure>();
            fig.Initialize(data);

            _figures.Add(fig);

            Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                float randX = Random.Range(-1f, 1f);
                float randY = Random.Range(-0.5f, 0f);
                Vector2 impulse = new Vector2(randX, randY) * _impulseStrength;
                rb.AddForce(impulse, ForceMode2D.Impulse);
            }

            yield return new WaitForSeconds(_spawnDelay);
        }
    }

    public void RemoveFigure(Figure figure)
    {
        if (_figures.Contains(figure))
            _figures.Remove(figure);
        Destroy(figure.gameObject);
    }

    public void Shuffle()
    {
        StopAllCoroutines();

        int currentCount = _figures.Count;

        int remainder = currentCount % 3;
        int spawnCount = remainder == 0
            ? currentCount
            : currentCount + (3 - remainder);

        foreach (var fig in _figures)
            if (fig != null)
                Destroy(fig.gameObject);

        _figures.Clear();

        _maxFigureCount = spawnCount;
        GenerateField();
    }

}
