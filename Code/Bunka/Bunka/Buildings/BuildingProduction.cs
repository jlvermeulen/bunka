using System.Collections.Generic;
using Microsoft.Xna.Framework;

// class for buildings that produce resources
class BuildingProduction : Building
{
    ResourceManager resourceManager;
    ResourceProducer[] producers;

    // overload for single producer
    public BuildingProduction(BuildingType type, ResourceManager resourceManager, ResourceProducer producers)
        : base(type)
    {
        this.producers = new ResourceProducer[] { producers };
        this.resourceManager = resourceManager;
        InitialiseProducers();
    }

    // overload for multiple producers
    public BuildingProduction(BuildingType type, ResourceManager resourceManager, ResourceProducer[] producers)
        : base(type)
    {
        this.producers = producers;
        this.resourceManager = resourceManager;
        InitialiseProducers();
    }

    public void Update(GameTime t)
    {
        foreach (ResourceProducer p in producers)
            p.Update(t);
    }

    //////////////////
    //   METHODS    //
    //////////////////

    void InitialiseProducers()
    {
        for (int i = 0; i < producers.Length; i++)
        {
            producers[i].Output.Location = this;

            // add to free resources, create new entry for resource if necessary
            LinkedList<Resource> list;
            if (resourceManager.FreeResources.TryGetValue(producers[i].OutputType, out list))
                list.AddFirst(producers[i].Output);
            else
            {
                list = new LinkedList<Resource>();
                list.AddFirst(producers[i].Output);
                resourceManager.FreeResources.Add(producers[i].OutputType, list);
            }
        }
    }

    //////////////////
    //  PROPERTIES  //
    //////////////////

    public ResourceProducer[] ResourceProducers
    {
        get { return producers; }
        set { producers = value; }
    }
}