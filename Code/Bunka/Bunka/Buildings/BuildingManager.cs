using System.Collections.Generic;
using Microsoft.Xna.Framework;

// all building types
public enum BuildingType { Construction, PRODUCTION, Quarry, Lumberjack, CoalMine, IronMine, Fishery, CONVERSION, CokingPlant, Sawmill, IronSmelter };

// class for building administration
public class BuildingManager
{
    BuildingLoader loader;
    List<BuildingProduction> production;
    List<BuildingConversion> conversion;

    public BuildingManager()
    {
        loader = new BuildingLoader();
        production = new List<BuildingProduction>();
        conversion = new List<BuildingConversion>();
        // Test();
    }
    /*
    public void Test()
    {
        CreateBuilding(BuildingType.Quarry);
        CreateBuilding(BuildingType.Lumberjack);
        CreateBuilding(BuildingType.Lumberjack);
        CreateBuilding(BuildingType.Lumberjack);
        CreateBuilding(BuildingType.CokingPlant);
        CreateBuilding(BuildingType.CoalMine);
        CreateBuilding(BuildingType.Fishery);
        CreateBuilding(BuildingType.IronMine);
        CreateBuilding(BuildingType.Sawmill);
        CreateBuilding(BuildingType.IronSmelter);
    }
    */
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

    public void CreateBuilding(BuildingType type, Vector2 position)
    {
        Building building = loader.CreateBuilding(type, position);

        if (type > BuildingType.CONVERSION)
            conversion.Add((BuildingConversion)building);
        else
            production.Add((BuildingProduction)building);
    }

    //////////////////
    //  PROPERTIES  //
    //////////////////

    public BuildingLoader BuildingLoader
    {
        get { return loader; }
    }
}