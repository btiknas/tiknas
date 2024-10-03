namespace Tiknas.EventBus;

public class MyDerivedEventData : MySimpleEventData
{
    public MyDerivedEventData(int value)
        : base(value)
    {
    }
}
