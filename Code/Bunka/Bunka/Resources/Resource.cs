// parent class for all resources
abstract class Resource
{
    ResourceManager manager;
    ResourceType type;
    uint amount;

    public Resource(ResourceManager manager, ResourceType type, uint amount)
    {
        uint a;
        bool exists = manager.ResourceCounts.TryGetValue(type, out a);
        if (exists)
            manager.ResourceCounts[type] = a + amount;
        else
            manager.ResourceCounts.Add(type, amount);

        this.manager = manager;
        this.type = type;
        this.amount = amount;
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
        set
        {
            uint diff = value - amount;
            amount = value;
            uint total;
            manager.ResourceCounts.TryGetValue(type, out total);
            manager.ResourceCounts[type] = total + diff;
        }
    }
}