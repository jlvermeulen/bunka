using Microsoft.Xna.Framework;

// class for producing resources
class ResourceProducer
{
    ResourceManager manager;
    Resource output;
    uint amount;
    float speed, timer;

    public ResourceProducer(ResourceManager manager, Resource output, uint amount, float speed)
    {
        this.manager = manager;
        this.output = output;
        this.amount = amount;
        this.speed = this.timer = speed;
    }

    public void Update(GameTime t)
    {
        if (timer > 0)
            timer -= (float)t.ElapsedGameTime.TotalSeconds;

        if (timer <= 0)
        {
            output.Amount += amount;
        }
    }

    //////////////////
    //  PROPERTIES  //
    //////////////////

    public Resource Output
    {
        get { return output; }
    }

    public ResourceType OutputType
    {
        get { return output.ResourceType; }
    }

    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }
}