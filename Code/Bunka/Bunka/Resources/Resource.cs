enum ResourceType { Wood, Stone, None }

abstract class Resource
{
    ResourceType resourceType;
    uint amount;

    public Resource(ResourceManager manager, ResourceType type, uint amount)
    {
        uint a;
        bool exists = manager.ResourceCounts.TryGetValue(type, out a);
        if (exists)
            manager.ResourceCounts[type] = a + amount;
        else
            manager.ResourceCounts.Add(type, amount);

        resourceType = type;
        this.amount = amount;
    }

    //////////////////
    //  PROPERTIES  //
    //////////////////

    public ResourceType ResourceType
    {
        get { return resourceType; }
    }

    public uint Amount
    {
        get { return amount; }
        set { amount = value; }
    }
}