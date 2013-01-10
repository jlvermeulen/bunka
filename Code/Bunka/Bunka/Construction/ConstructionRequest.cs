using Microsoft.Xna.Framework;

public struct ConstructionRequest
{
    BuildingType type;
    Vector2 position;

    public ConstructionRequest(BuildingType type, Vector2 position)
    {
        this.type = type;
        this.position = position;
    }

    public BuildingType BuildingType
    {
        get { return type; }
    }

    public Vector2 Position
    {
        get { return position; }
    }
}