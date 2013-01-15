public struct BuildRequest
{
    public BuildRequest(ConstructionBuilding location)
        : this()
    {
        this.Location = location;
    }

    //////////////////
    //  PROPERTIES  //
    //////////////////

    public ConstructionBuilding Location { get; private set; }
}