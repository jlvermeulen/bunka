// class for all resources
public class Resource
{
    ResourceType type;
    uint amount;
    Building location;

    public Resource(ResourceType type, uint amount)
    {
        // add amount to resource count or create resource count if it is the first resource of its kind
        uint a;
        if (BunkaGame.ResourceManager.ResourceCounts.TryGetValue(type, out a))
            BunkaGame.ResourceManager.ResourceCounts[type] = a + amount;
        else
            BunkaGame.ResourceManager.ResourceCounts.Add(type, amount);

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
            BunkaGame.ResourceManager.ResourceCounts[type] += diff;
        }
    }

    // building that owns the resource, null if being moved
    public Building Location
    {
        get { return location; }
        set { location = value; }
    }
}