using System.Collections.Generic;
using Microsoft.Xna.Framework;

// all building types
public enum BuildingType { Construction, PRODUCTION, Quarry, Lumberjack, CoalMine, IronMine, Fishery, CONVERSION, CokingPlant, Sawmill, IronSmelter };

// class for building administration
public class BuildingManager
{
    List<BuildingProduction> production;
    List<BuildingConversion> conversion;

    public BuildingManager()
    {
        this.BuildingLoader = new BuildingLoader();
        this.production = new List<BuildingProduction>();
        this.conversion = new List<BuildingConversion>();
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
        foreach (BuildingProduction p in this.production)
        {
            p.Update(t);
        }
        foreach (BuildingConversion c in this.conversion)
        {
            c.Update(t);
        }
    }

    //////////////////
    //   METHODS    //
    //////////////////

    public void CreateBuilding(BuildingType type, CPoint position)
    {
        Building building = this.BuildingLoader.CreateBuilding(type, position);

        if (type > BuildingType.CONVERSION)
            this.conversion.Add((BuildingConversion)building);
        else
            this.production.Add((BuildingProduction)building);
    }

    //////////////////
    //  PROPERTIES  //
    //////////////////

    public BuildingLoader BuildingLoader { get; private set; }
}