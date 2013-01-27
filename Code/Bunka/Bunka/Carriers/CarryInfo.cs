public class CarryInfo
{
    public CarryInfo(ResourceType type, uint amount, Building origin, Building destination)
    {
        this.ResourceType = type;
        this.Amount = amount;
        this.Origin = origin;
        this.Destination = destination;
    }

    public Building Origin { get; private set; }

    public Building Destination { get; private set; }

    public ResourceType ResourceType { get; private set; }

    public uint Amount { get; private set; }
}