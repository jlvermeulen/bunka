// parent class for all buildings
public abstract class Building
{
    protected Building(BuildingType type, CPoint position)
    {
        BunkaGame.MapManager[position] = this;
        this.BuildingType = type;
        this.Position = position;
    }

    public virtual void CollectResource(ResourceType type, uint amount) { }

    public virtual void DeliverResource(ResourceType type, uint amount) { }

    //////////////////
    //  PROPERTIES  //
    //////////////////

    public BuildingType BuildingType { get; private set; }

    public CPoint Position { get; private set; }
}