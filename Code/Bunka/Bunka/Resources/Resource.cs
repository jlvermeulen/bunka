// class for all resources
public class Resource
{
    uint amount;

    public Resource(ResourceType type, uint amount)
    {
        // create resource count if it is the first resource of its kind
        if(!BunkaGame.ResourceManager.ResourceCounts.ContainsKey(type))
            BunkaGame.ResourceManager.ResourceCounts.Add(type, amount);

        this.ResourceType = type;
        this.Amount = amount;
    }

    //////////////////
    //  PROPERTIES  //
    //////////////////

    public ResourceType ResourceType { get; private set; }

    public uint Amount
    {
        get { return amount; }
        set
        {
            // update amount and resource count
            uint diff = value - amount;
            amount = value;
            BunkaGame.ResourceManager.ResourceCounts[this.ResourceType] += diff;
        }
    }

    // building that owns the resource, null if being moved
    public Building Location { get; set; }
}