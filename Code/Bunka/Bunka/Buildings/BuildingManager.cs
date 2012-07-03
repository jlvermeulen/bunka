using System.Collections.Generic;
using Microsoft.Xna.Framework;

// all building types
enum BuildingType { PRODUCTION, Quarry, Lumberjack, CONVERSION, CokingPlant };

// class for building administration
class BuildingManager
{
    BuildingLoader loader;
    List<BuildingProduction> production;
    List<BuildingConversion> conversion;
    ResourceManager resourceManager;

    public BuildingManager(ResourceManager resourceManager)
    {
        loader = new BuildingLoader();
        production = new List<BuildingProduction>();
        conversion = new List<BuildingConversion>();
        this.resourceManager = resourceManager;
        Test();
        new BuildingLoader();
    }

    public void Test()
    {
        CreateBuilding(BuildingType.Quarry);
        CreateBuilding(BuildingType.Lumberjack);
        CreateBuilding(BuildingType.Lumberjack);
        CreateBuilding(BuildingType.Lumberjack);
        CreateBuilding(BuildingType.CokingPlant);
    }

    public void Update(GameTime t)
    {
        foreach (BuildingProduction p in production)
        {
            p.Update(t);
        }
        foreach (BuildingConversion c in conversion)
        {
            c.Update(t);
        }
    }

    //////////////////
    //   METHODS    //
    //////////////////

    public void CreateBuilding(BuildingType type)
    {
        Building building = loader.CreateBuilding(type, resourceManager);

        if (type > BuildingType.CONVERSION)
            conversion.Add((BuildingConversion)building);
        else
            production.Add((BuildingProduction)building);
    }

    //////////////////
    //  PROPERTIES  //
    //////////////////

    public List<Building> Buildings
    {
        get { return null; }
    }
}