using System.Collections.Generic;

public class Stockpile : Building
{
    Dictionary<ResourceType, Resource> resources;

    public Stockpile(CPoint position)
        : base(BuildingType.Stockpile, position)
    {
        this.resources = new Dictionary<ResourceType, Resource>();
        this.DeliverResource(ResourceType.Stone, 10);
        this.DeliverResource(ResourceType.Wood, 10);
    }

    //////////////////
    //   METHODS    //
    //////////////////

    public override void CollectResource(ResourceType type, uint amount)
    {
        this.resources[type].Amount -= amount;
    }

    public override void DeliverResource(ResourceType type, uint amount)
    {
        Resource resource;
        if (this.resources.TryGetValue(type, out resource))
            resource.Amount += amount;
        else
        {
            resource = BunkaGame.ResourceManager.CreateResource(type, amount, this);
            this.resources.Add(type, resource);

            LinkedList<Resource> list;
            if (BunkaGame.ResourceManager.FreeResources.TryGetValue(type, out list))
                list.AddFirst(resource);
            else
            {
                list = new LinkedList<Resource>();
                list.AddFirst(resource);
                BunkaGame.ResourceManager.FreeResources.Add(type, list);
            }
        }
    }
}