// parent class for all buildings
public abstract class Building
{
    public Building(BuildingType type, CPoint position)
    {
        BunkaGame.MapManager[position] = this;
        this.BuildingType = type;
        this.Position = position;
    }

    //////////////////
    //  PROPERTIES  //
    //////////////////

    public BuildingType BuildingType { get; private set; }

    public CPoint Position { get; private set; }
}