public struct CarryRequest
{
    public CarryRequest(ResourceType type, uint amount, Building destination)
        : this()
    {
        this.ResourceType = type;
        this.Amount = amount;
        this.Destination = destination;
    }

    //////////////////
    //  PROPERTIES  //
    //////////////////

    public ResourceType ResourceType { get; private set; }

    public uint Amount { get; private set; }

    public Building Destination { get; private set; }
}