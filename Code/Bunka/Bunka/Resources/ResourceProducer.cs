using Microsoft.Xna.Framework;

// class for producing resources
public class ResourceProducer
{
    Resource output;
    ResourceType type;
    uint amount;
    float speed, timer;

    public ResourceProducer(ResourceType type, uint amount, float speed)
    {
        this.type = type;
        this.amount = amount;
        this.speed = this.timer = speed;
        this.output = BunkaGame.ResourceManager.CreateResource(type);
    }

    public void Update(GameTime t)
    {
        if (timer > 0)
            timer -= (float)t.ElapsedGameTime.TotalSeconds;

        if (timer <= 0 && output != null)
        {
            output.Amount += amount;
            timer = speed;
        }
    }

    //////////////////
    //  PROPERTIES  //
    //////////////////

    public Resource Output
    {
        get { return output; }
        set { output = value; }
    }

    public ResourceType OutputType
    {
        get { return type; }
    }

    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }
}