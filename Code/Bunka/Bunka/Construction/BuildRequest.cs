struct BuildRequest
{
    ConstructionBuilding location;

    public BuildRequest(ConstructionBuilding location)
    {
        this.location = location;
    }

    //////////////////
    //  PROPERTIES  //
    //////////////////

    public ConstructionBuilding Location
    {
        get { return location; }
    }
}