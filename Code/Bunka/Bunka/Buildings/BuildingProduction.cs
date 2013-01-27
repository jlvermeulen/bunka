using System.Collections.Generic;
using Microsoft.Xna.Framework;

// class for buildings that produce resources
public class BuildingProduction : Building
{
    // overload for single producer
    public BuildingProduction(BuildingType type, CPoint position, ResourceType rType, uint amount, float speed)
        : base(type, position)
    {
        this.ResourceProducers = new ResourceProducer[] { BunkaGame.ResourceManager.CreateResourceProducer(rType, amount, speed, this) };
        this.InitialiseProducers();
    }

    // overload for multiple producers
    public BuildingProduction(BuildingType type, CPoint position, ResourceType[] rTypes, uint[] amounts, float speed)
        : base(type, position)
    {
        this.ResourceProducers = new ResourceProducer[rTypes.Length];
        for (int i = 0; i < rTypes.Length; i++)
            this.ResourceProducers[i] = BunkaGame.ResourceManager.CreateResourceProducer(rTypes[i], amounts[i], speed, this);

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

    public override void CollectResource(ResourceType type, uint amount)
    {
        foreach (ResourceProducer producer in this.ResourceProducers)
            if (producer.OutputType == type)
            {
                producer.Output.Amount -= amount;
                break;
            }
    }

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