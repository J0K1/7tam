using System;
using UnityEngine;

[Serializable]
public class FigureData
{
    [Tooltip("��� ���������, ������������� �� ������")]
    public AnimalType Animal;

    [Tooltip("����� ������")]
    public ShapeType Shape;

    [Tooltip("���� �����")]
    public ShapeColor Color;

    public bool IsPartialMatch(FigureData other)
    {
        if (other == null)
            return false;

        return Animal == other.Animal &&
           Shape == other.Shape &&
           Color == other.Color;
    }
}

public enum AnimalType
{
    Cat,
    Dog,
    Rabbit,
    Panda,
    Bear,
    Koala,
    Monkey,
    Mouse,
    Tiger
}

public enum ShapeType
{
    Circle,
    Triangle,
    Square
}

public enum ShapeColor
{
    Red,
    Green,
    Blue
}
