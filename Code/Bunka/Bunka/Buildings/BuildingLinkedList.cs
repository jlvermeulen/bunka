class BuildingLinkedList
{
    Building building;
    BuildingLinkedList previous, next;

    public BuildingLinkedList(Building building, BuildingLinkedList previous, BuildingLinkedList next)
    {
        this.building = building;
        this.previous = previous;
        this.next = next;
    }

    //////////////////
    //  PROPERTIES  //
    //////////////////

    public Building Building
    {
        get { return building; }
    }

    public BuildingLinkedList Previous
    {
        get { return previous; }
        set { previous = value; }
    }

    public BuildingLinkedList Next
    {
        get { return next; }
        set { next = value; }
    }
}