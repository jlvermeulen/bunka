using System.Collections.Generic;
using Microsoft.Xna.Framework;

// parent class for buildings that convert resources
abstract class Building_Conversion : Building
{
    ResourceManager resourceManager;
    ResourceConverter[] converters;

    // overload for single converter
    public Building_Conversion(BuildingType type, ResourceManager resourceManager, ResourceConverter converter)
        : base(type)
    {
        this.resourceManager = resourceManager;
        this.converters = new ResourceConverter[] { converter };
    }

    // overload for multiple converters
    public Building_Conversion(BuildingType type, ResourceManager resourceManager, ResourceConverter[] converters)
        : base(type)
    {
        this.resourceManager = resourceManager;
        this.converters = converters;
    }

    public void Update(GameTime t)
    {
        foreach (ResourceConverter c in converters)
        {
            for (int i = 0; i < c.Output.Length; i++)
                if (c.Output[i] == null)
                {
                    // create new resource
                    c.Output[i] = resourceManager.CreateResource(c.OutputTypes[i]);
                    c.Output[i].Location = this;

                    // add to free resources, create new entry for resource if necessary
                    LinkedList<Resource> list;
                    if (resourceManager.FreeResources.TryGetValue(c.OutputTypes[i], out list))
                        list.AddFirst(c.Output[i]);
                    else
                    {
                        list = new LinkedList<Resource>();
                        list.AddFirst(c.Output[i]);
                        resourceManager.FreeResources.Add(c.OutputTypes[i], list);
                    }
                }
            for (int i = 0; i < c.Input.Length; i++)
            {
                if (c.Input[i] == null)
                {
                    c.Input[i] = resourceManager.CreateResource(c.InputTypes[i]);
                    c.Input[i].Location = this;
                }
                else if (c.Input[i].Amount < c.InputSize[i])
                    resourceManager.RequestResource(c.InputTypes[i], c.InputSize[i] - c.Input[i].Amount, this);
            }
            c.Update(t);
        }
    }

    //////////////////
    //  PROPERTIES  //
    //////////////////

    public ResourceConverter[] ResourceConverters
    {
        get { return converters; }
        set { converters = value; }
    }
}