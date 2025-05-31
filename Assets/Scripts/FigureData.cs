using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

[Serializable]
public class FigureData
{
    public AnimalType animal;
    public ShapeType shape;
    public ShapeColor color;

    public bool IsPertialMatch(FigureData other) => 
        this.animal == other.animal && this.color == other.color;
}

public enum AnimalType
{
    Cat,
    Dog,
    Rabbit,
    Panda
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
