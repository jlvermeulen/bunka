using System.Collections.Generic;
using Microsoft.Xna.Framework;

// parent class for buildings that produce resources
abstract class Building_Production : Building
{
    ResourceManager resourceManager;
    ResourceProducer[] production;

    // overload for single producer
    public Building_Production(BuildingType type, ResourceManager resourceManager, ResourceProducer production)
        : base(type)
    {
        this.production = new ResourceProducer[] { production };
        this.resourceManager = resourceManager;
    }

    // overload for multiple producers
    public Building_Production(BuildingType type, ResourceManager resourceManager, ResourceProducer[] production)
        : base(type)
    {
        this.production = production;
        this.resourceManager = resourceManager;
    }

    public void Update(GameTime t)
    {
        foreach (ResourceProducer p in production)
        {
            if (p.Output == null)
            {
                // create new resource
                p.Output = resourceManager.CreateResource(p.OutputType);
                p.Output.Location = this;

                // add to free resources, create new entry for resource if necessary
                LinkedList<Resource> list;
                if (resourceManager.FreeResources.TryGetValue(p.OutputType, out list))
                    list.AddFirst(p.Output);
                else
                {
                    list = new LinkedList<Resource>();
                    list.AddFirst(p.Output);
                    resourceManager.FreeResources.Add(p.OutputType, list);
                }
            }
            p.Update(t);
        }
    }

    //////////////////
    //  PROPERTIES  //
    //////////////////

    public ResourceProducer[] ResourceProducers
    {
        get { return production; }
        set { production = value; }
    }
}