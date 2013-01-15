using System.Collections.Generic;
using Microsoft.Xna.Framework;

// parent class for buildings that convert resources
public class BuildingConversion : Building
{
    Dictionary<ResourceType, uint> requestedResources;

    public BuildingConversion(BuildingType type, ResourceConverter converter, CPoint position)
        : base(type, position)
    {
        this.ResourceConverter = converter;
        this.requestedResources = new Dictionary<ResourceType, uint>();
        this.InitialiseConverters();
    }

    public void Update(GameTime t)
    {
        for (int i = 0; i < this.ResourceConverter.Input.Length; i++)
        {
            if (this.ResourceConverter.Input[i].Amount < this.ResourceConverter.InputSize[i] && !this.requestedResources.ContainsKey(this.ResourceConverter.Input[i].ResourceType))
            {
                BunkaGame.ResourceManager.RequestResource(this.ResourceConverter.InputTypes[i], this.ResourceConverter.InputSize[i] - this.ResourceConverter.Input[i].Amount, this);
                this.requestedResources.Add(this.ResourceConverter.Input[i].ResourceType, this.ResourceConverter.InputSize[i] - this.ResourceConverter.Input[i].Amount);
            }
        }
        this.ResourceConverter.Update(t);
    }

    //////////////////
    //   METHODS    //
    //////////////////

    public void DeliverResource(ResourceType type, uint amount)
    {
        // update requested resources
        uint curr = this.requestedResources[type];
        if (amount >= curr)
            this.requestedResources.Remove(type);
        else
            this.requestedResources[type] = curr - amount;

        // deliver resource
        foreach (Resource r in this.ResourceConverter.Input)
            if (r.ResourceType == type)
            {
                r.Amount += amount;
                break;
            }
    }

    private void InitialiseConverters()
    {
        for (int i = 0; i < this.ResourceConverter.Input.Length; i++)
            this.ResourceConverter.Input[i].Location = this;

        for (int i = 0; i < this.ResourceConverter.Output.Length; i++)
        {
            this.ResourceConverter.Output[i].Location = this;

            // add to free resources, create new entry for resource if necessary
            LinkedList<Resource> list;
            if (BunkaGame.ResourceManager.FreeResources.TryGetValue(this.ResourceConverter.OutputTypes[i], out list))
                list.AddFirst(this.ResourceConverter.Output[i]);
            else
            {
                list = new LinkedList<Resource>();
                list.AddFirst(this.ResourceConverter.Output[i]);
                BunkaGame.ResourceManager.FreeResources.Add(this.ResourceConverter.OutputTypes[i], list);
            }
        }
    }

    //////////////////
    //  PROPERTIES  //
    //////////////////

    public ResourceConverter ResourceConverter { get; set; }
}