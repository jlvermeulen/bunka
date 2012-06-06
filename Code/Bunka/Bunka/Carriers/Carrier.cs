using Microsoft.Xna.Framework;

// class for resource carriers
class Carrier
{
    Resource resource;
    Building destination;

    public Carrier()
    {

    }

    public void Update(GameTime t)
    {

    }

    //////////////////
    //  PROPERTIES  //
    //////////////////

    public Resource Carrying
    {
        get { return resource; }
        set { resource = value; }
    }

    public Building Destination
    {
        get { return destination; }
        set { destination = value; }
    }
}