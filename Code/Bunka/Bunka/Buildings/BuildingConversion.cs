using System.Collections.Generic;
using Microsoft.Xna.Framework;

// parent class for buildings that convert resources
class BuildingConversion : Building
{
    ResourceManager resourceManager;
    ResourceConverter converter;
    List<ResourceType> requestedResources;

    public BuildingConversion(BuildingType type, ResourceManager resourceManager, ResourceConverter converter)
        : base(type)
    {
        this.resourceManager = resourceManager;
        this.converter = converter;
        requestedResources = new List<ResourceType>();
    }

    public void Update(GameTime t)
    {
        for (int i = 0; i < converter.Input.Length; i++)
        {
            if (converter.Input[i].Amount < converter.InputSize[i] && !requestedResources.Contains(converter.Input[i].ResourceType))
            {
                resourceManager.RequestResource(converter.InputTypes[i], converter.InputSize[i] - converter.Input[i].Amount, this);
                requestedResources.Add(converter.Input[i].ResourceType);
            }
        }
        converter.Update(t);
    }

    //////////////////
    //   METHODS    //
    //////////////////

    public void DeliverResource(ResourceType type)
    {
        requestedResources.Remove(type);
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
            if (resourceManager.FreeResources.TryGetValue(converter.OutputTypes[i], out list))
                list.AddFirst(converter.Output[i]);
            else
            {
                list = new LinkedList<Resource>();
                list.AddFirst(converter.Output[i]);
                resourceManager.FreeResources.Add(converter.OutputTypes[i], list);
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