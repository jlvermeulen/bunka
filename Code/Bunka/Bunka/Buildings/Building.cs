using Microsoft.Xna.Framework;

// parent class for all buildings
public abstract class Building
{
    BuildingType type;
    Vector2 position;

    public Building(BuildingType type, Vector2 position)
    {
        BunkaGame.MapManager[position] = this;
        this.type = type;
        this.position = position;
    }

    //////////////////
    //  PROPERTIES  //
    //////////////////

    public BuildingType BuildingType
    {
        get { return type; }
    }

    public Vector2 Position
    {
        get { return position; }
    }
}