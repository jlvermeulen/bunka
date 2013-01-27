using Microsoft.Xna.Framework;

// class for producing resources
public class ResourceProducer
{
    public ResourceProducer(ResourceType type, uint amount, float speed, Building location)
    {
        this.OutputType = type;
        this.Amount = amount;
        this.Speed = this.TimeLeft = speed;
        this.Output = BunkaGame.ResourceManager.CreateResource(type, 0, location);
        this.Location = location;
    }

    public void Update(GameTime t)
    {
        if (this.TimeLeft > 0)
            this.TimeLeft -= (float)t.ElapsedGameTime.TotalSeconds;

        if (this.TimeLeft <= 0 && this.Output != null)
        {
            this.Output.Amount += this.Amount;
            this.TimeLeft = this.Speed;
        }
    }

    //////////////////
    //  PROPERTIES  //
    //////////////////

    public Building Location { get; private set; }

    public Resource Output { get; private set; }

    public ResourceType OutputType { get; private set; }

    public float Speed { get; set; }

    public float TimeLeft { get; set; }

    public uint Amount { get; set; }
}