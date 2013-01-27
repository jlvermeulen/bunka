using System.Collections.Generic;
using Microsoft.Xna.Framework;

// all building types
public enum BuildingType { Construction, Stockpile, PRODUCTION, Quarry, Lumberjack, CoalMine, IronMine, Fishery, CONVERSION, CokingPlant, Sawmill, IronSmelter };

// class for building administration
public class BuildingManager
{
    List<BuildingProduction> production;
    List<BuildingConversion> conversion;
    List<Building> special;

    public BuildingManager()
    {
        this.BuildingLoader = new BuildingLoader();
        this.production = new List<BuildingProduction>();
        this.conversion = new List<BuildingConversion>();
        this.special = new List<Building>();
    }

    public void BuildStockpile()
    {
        this.special.Add(new Stockpile(new CPoint(2, 2)));
        BunkaGame.MapManager[2, 2] = special[0];
    }

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
        else if (type > BuildingType.PRODUCTION)
            this.production.Add((BuildingProduction)building);
        else
            this.special.Add(building);
    }

    //////////////////
    //  PROPERTIES  //
    //////////////////

    public BuildingLoader BuildingLoader { get; private set; }
}