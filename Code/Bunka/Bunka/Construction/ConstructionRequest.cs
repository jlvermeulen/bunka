using Microsoft.Xna.Framework;

public struct ConstructionRequest
{
    public ConstructionRequest(BuildingType type, CPoint position)
        : this()
    {
        this.BuildingType = type;
        this.Position = position;
    }

    public BuildingType BuildingType { get; private set; }

    public CPoint Position { get; private set; }
}