using System;

namespace Tiknas.Serialization.Objects;

[Serializable]
public class Car
{
    public string Name { get; set; }

    private Car()
    {

    }

    public Car(string name)
    {
        Name = name;
    }
}
