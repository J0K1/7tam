using System;
using UnityEngine;

[Serializable]
public class FigureData
{
    [Tooltip("“ип животного, отображаемого на фигуре")]
    public AnimalType Animal;

    [Tooltip("‘орма фигуры")]
    public ShapeType Shape;

    [Tooltip("÷вет формы")]
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
