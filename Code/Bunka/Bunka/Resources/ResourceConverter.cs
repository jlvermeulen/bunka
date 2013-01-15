using Microsoft.Xna.Framework;

// class for converting one resource into another
public class ResourceConverter
{
    public ResourceConverter(ResourceType[] inputTypes, ResourceType[] outputTypes, byte[] inSize, byte[] outSize, float speed)
    {
        this.InputTypes = inputTypes;
        this.OutputTypes = outputTypes;
        this.InputSize = inSize;
        this.OutputSize = outSize;
        this.Speed = this.TimeLeft = speed;
        this.Input = new Resource[inputTypes.Length];
        this.Output = new Resource[outputTypes.Length];

        for (int i = 0; i < this.Input.Length; i++)
            this.Input[i] = BunkaGame.ResourceManager.CreateResource(inputTypes[i]);
        for (int i = 0; i < this.Output.Length; i++)
            this.Output[i] = BunkaGame.ResourceManager.CreateResource(outputTypes[i]);
    }

    public void Update(GameTime t)
    {
        if (this.TimeLeft > 0)
            this.TimeLeft -= (float)t.ElapsedGameTime.TotalSeconds;

        if (this.TimeLeft <= 0 && CanConvert)
        {
            for (int i = 0; i < this.Input.Length; i++)
                this.Input[i].Amount -= this.InputSize[i];
            for (int i = 0; i < this.Output.Length; i++)
                this.Output[i].Amount += this.OutputSize[i];
            this.TimeLeft = this.Speed;
        }
    }

    //////////////////
    //  PROPERTIES  //
    //////////////////

    public Resource[] Input { get; private set; }

    public Resource[] Output { get; private set; }

    public ResourceType[] InputTypes { get; private set; }

    public ResourceType[] OutputTypes { get; private set; }

    public byte[] InputSize { get; private set; }

    public byte[] OutputSize { get; private set; }

    public float Speed { get; set; }

    public float TimeLeft { get; set; }

    private bool CanConvert
    {
        get
        {
            for (int i = 0; i < this.Input.Length; i++)
                if (this.Input[i].Amount < this.InputSize[i])
                    return false;
            return true;
        }
    }
}