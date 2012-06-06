using System.Collections.Generic;

// all building types
enum BuildingType { Quarry, Lumberjack, CokingPlant };

// class for building administration
class BuildingManager
{
    List<Building> buildings;
    ResourceManager resourceManager;

    public BuildingManager(ResourceManager resourceManager)
    {
        buildings = new List<Building>();
        this.resourceManager = resourceManager;
        //Test();
    }

    public void Test()
    {
        CreateBuilding(BuildingType.Quarry);
        CreateBuilding(BuildingType.Lumberjack);
        CreateBuilding(BuildingType.CokingPlant);
    }

    //////////////////
    //   METHODS    //
    //////////////////

    public void CreateBuilding(BuildingType type)
    {
        switch (type)
        {
            case BuildingType.Lumberjack:
                buildings.Add(new Building_Production_Lumberjack(resourceManager));
                break;
            case BuildingType.Quarry:
                buildings.Add(new Building_Production_Quarry(resourceManager));
                break;
            case BuildingType.CokingPlant:
                buildings.Add(new Building_Conversion_CokingPlant(resourceManager));
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
        get { return buildings; }
    }
}