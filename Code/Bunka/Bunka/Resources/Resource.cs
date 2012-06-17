// parent class for all resources
abstract class Resource
{
    ResourceManager resourceManager;
    ResourceType type;
    uint amount;
    Building location;

    public Resource(ResourceManager resourceManager, ResourceType type, uint amount)
    {
        // add amount to resource count or create resource count if it is the first resource of its kind
        uint a;
        if (resourceManager.ResourceCounts.TryGetValue(type, out a))
            resourceManager.ResourceCounts[type] = a + amount;
        else
            resourceManager.ResourceCounts.Add(type, amount);

        this.resourceManager = resourceManager;
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
            // update amount and resource count
            uint diff = value - amount;
            amount = value;
            uint total;
            resourceManager.ResourceCounts.TryGetValue(type, out total);
            resourceManager.ResourceCounts[type] = total + diff;
        }
    }

    // building that owns the resource, null if being moved
    public Building Location
    {
        get { return location; }
        set { location = value; }
    }
}