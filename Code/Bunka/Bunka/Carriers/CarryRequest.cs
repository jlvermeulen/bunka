public struct CarryRequest
{
    ResourceType type;
    uint amount;
    Building destination;

    public CarryRequest(ResourceType type, uint amount, Building destination)
    {
        this.type = type;
        this.amount = amount;
        this.destination = destination;
    }

    //////////////////
    //  PROPERTIES  //
    //////////////////

    public ResourceType ResourceType
    {
        get { return type; }
    }

    public uint Amount
    {
        get { return amount; }
    }

    public Building Destination
    {
        get { return destination; }
    }
}