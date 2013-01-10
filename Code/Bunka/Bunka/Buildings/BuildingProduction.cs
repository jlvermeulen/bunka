using System.Collections.Generic;
using Microsoft.Xna.Framework;

// class for buildings that produce resources
public class BuildingProduction : Building
{
    ResourceProducer[] producers;

    // overload for single producer
    public BuildingProduction(BuildingType type, ResourceProducer producers, Vector2 position)
        : base(type, position)
    {
        this.producers = new ResourceProducer[] { producers };
        InitialiseProducers();
    }

    // overload for multiple producers
    public BuildingProduction(BuildingType type, ResourceProducer[] producers, Vector2 position)
        : base(type, position)
    {
        this.producers = producers;
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
            if (BunkaGame.ResourceManager.FreeResources.TryGetValue(producers[i].OutputType, out list))
                list.AddFirst(producers[i].Output);
            else
            {
                list = new LinkedList<Resource>();
                list.AddFirst(producers[i].Output);
                BunkaGame.ResourceManager.FreeResources.Add(producers[i].OutputType, list);
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