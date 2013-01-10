using System.Collections.Generic;
using Microsoft.Xna.Framework;

// parent class for buildings that convert resources
public class BuildingConversion : Building
{
    ResourceConverter converter;
    Dictionary<ResourceType, uint> requestedResources;

    public BuildingConversion(BuildingType type, ResourceConverter converter, Vector2 position)
        : base(type, position)
    {
        this.converter = converter;
        requestedResources = new Dictionary<ResourceType, uint>();
        InitialiseConverters();
    }

    public void Update(GameTime t)
    {
        for (int i = 0; i < converter.Input.Length; i++)
        {
            if (converter.Input[i].Amount < converter.InputSize[i] && !requestedResources.ContainsKey(converter.Input[i].ResourceType))
            {
                BunkaGame.ResourceManager.RequestResource(converter.InputTypes[i], converter.InputSize[i] - converter.Input[i].Amount, this);
                requestedResources.Add(converter.Input[i].ResourceType, converter.InputSize[i] - converter.Input[i].Amount);
            }
        }
        converter.Update(t);
    }

    //////////////////
    //   METHODS    //
    //////////////////

    public void DeliverResource(ResourceType type, uint amount)
    {
        // update requested resources
        uint curr = requestedResources[type];
        if (amount >= curr)
            requestedResources.Remove(type);
        else
            requestedResources[type] = curr - amount;

        // deliver resource
        foreach (Resource r in converter.Input)
            if (r.ResourceType == type)
            {
                r.Amount += amount;
                break;
            }
    }

    void InitialiseConverters()
    {
        for (int i = 0; i < converter.Input.Length; i++)
            converter.Input[i].Location = this;

        for (int i = 0; i < converter.Output.Length; i++)
        {
            converter.Output[i].Location = this;

            // add to free resources, create new entry for resource if necessary
            LinkedList<Resource> list;
            if (BunkaGame.ResourceManager.FreeResources.TryGetValue(converter.OutputTypes[i], out list))
                list.AddFirst(converter.Output[i]);
            else
            {
                list = new LinkedList<Resource>();
                list.AddFirst(converter.Output[i]);
                BunkaGame.ResourceManager.FreeResources.Add(converter.OutputTypes[i], list);
            }
        }
    }

    //////////////////
    //  PROPERTIES  //
    //////////////////

    public ResourceConverter ResourceConverter
    {
        get { return converter; }
        set { converter = value; }
    }
}