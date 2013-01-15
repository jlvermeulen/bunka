using System.Collections.Generic;
using Microsoft.Xna.Framework;

// class for buildings that produce resources
public class BuildingProduction : Building
{
    // overload for single producer
    public BuildingProduction(BuildingType type, ResourceProducer producers, CPoint position)
        : base(type, position)
    {
        this.ResourceProducers = new ResourceProducer[] { producers };
        this.InitialiseProducers();
    }

    // overload for multiple producers
    public BuildingProduction(BuildingType type, ResourceProducer[] producers, CPoint position)
        : base(type, position)
    {
        this.ResourceProducers = producers;
        this.InitialiseProducers();
    }

    public void Update(GameTime t)
    {
        foreach (ResourceProducer p in this.ResourceProducers)
            p.Update(t);
    }

    //////////////////
    //   METHODS    //
    //////////////////

    private void InitialiseProducers()
    {
        for (int i = 0; i < ResourceProducers.Length; i++)
        {
            this.ResourceProducers[i].Output.Location = this;

            // add to free resources, create new entry for resource if necessary
            LinkedList<Resource> list;
            if (BunkaGame.ResourceManager.FreeResources.TryGetValue(this.ResourceProducers[i].OutputType, out list))
                list.AddFirst(this.ResourceProducers[i].Output);
            else
            {
                list = new LinkedList<Resource>();
                list.AddFirst(this.ResourceProducers[i].Output);
                BunkaGame.ResourceManager.FreeResources.Add(this.ResourceProducers[i].OutputType, list);
            }
        }
    }

    //////////////////
    //  PROPERTIES  //
    //////////////////

    public ResourceProducer[] ResourceProducers { get; set; }
}