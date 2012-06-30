using System.Collections.Generic;
using Microsoft.Xna.Framework;

// all building types
enum BuildingType { Production, Quarry, Lumberjack, Conversion, CokingPlant };

// class for building administration
class BuildingManager
{
    List<Building_Production> production;
    List<Building_Conversion> conversion;
    ResourceManager resourceManager;

    public BuildingManager(ResourceManager resourceManager)
    {
        production = new List<Building_Production>();
        conversion = new List<Building_Conversion>();
        this.resourceManager = resourceManager;
        Test();
    }

    public void Test()
    {
        CreateProductionBuilding(BuildingType.Quarry);
        CreateProductionBuilding(BuildingType.Lumberjack);
        CreateProductionBuilding(BuildingType.Lumberjack);
        CreateProductionBuilding(BuildingType.Lumberjack);
        CreateConversionBuilding(BuildingType.CokingPlant);
    }

    public void Update(GameTime t)
    {
        foreach (Building_Production p in production)
        {
            p.Update(t);
        }
        foreach (Building_Conversion c in conversion)
        {
            c.Update(t);
        }
    }

    //////////////////
    //   METHODS    //
    //////////////////

    public void CreateProductionBuilding(BuildingType type)
    {
        switch (type)
        {
            case BuildingType.Lumberjack:
                production.Add(new Building_Production_Lumberjack(resourceManager));
                break;
            case BuildingType.Quarry:
                production.Add(new Building_Production_Quarry(resourceManager));
                break;
            default:
                break;
        }
    }

    public void CreateConversionBuilding(BuildingType type)
    {
        switch (type)
        {
            case BuildingType.CokingPlant:
                conversion.Add(new Building_Conversion_CokingPlant(resourceManager));
                break;
            default:
                break;
        }
    }

    //////////////////
    //  PROPERTIES  //
    //////////////////

    public List<Building> Buildings
    {
        get { return null; }
    }
}