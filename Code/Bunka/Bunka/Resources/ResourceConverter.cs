using Microsoft.Xna.Framework;

// class for converting one resource into another
class ResourceConverter
{
    ResourceManager manager;
    Resource[] input, output;
    float speed, timer;
    byte[] inSize, outSize;

    public ResourceConverter(ResourceManager manager, Resource[] input, Resource[] output, byte[] inSize, byte[] outSize, float speed)
    {
        this.manager = manager;
        this.input = input;
        this.output = output;
        this.inSize = inSize;
        this.outSize = outSize;
        this.speed = this.timer = speed;
    }

    public void Update(GameTime t)
    {
        if (timer > 0)
            timer -= (float)t.ElapsedGameTime.TotalSeconds;

        if (timer <= 0 && CanConvert)
        {
            for (int i = 0; i < input.GetLength(0); i++)
                input[i].Amount -= inSize[i];
            for (int i = 0; i < output.GetLength(0); i++)
                output[i].Amount += outSize[i];
            timer = speed;
        }
    }

    //////////////////
    //  PROPERTIES  //
    //////////////////

    public Resource[] Input
    {
        get { return input; }
    }

    public Resource[] Output
    {
        get { return output; }
    }

    public ResourceType[] InputTypes
    {
        get 
        { 
            int i = input.GetLength(0);
            ResourceType[] types = new ResourceType[i];
            for (int j = 0; j < i; j++)
            {
               types[j] = input[j].ResourceType;
            }
            return types;
        }
    }

    public ResourceType[] OutputTypes
    {
        get 
        { 
            int i = output.GetLength(0);
            ResourceType[] types = new ResourceType[i];
            for (int j = 0; j < i; j++)
            {
               types[j] = output[j].ResourceType;
            }
            return types;
        }
    }

    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    bool CanConvert
    {
        get
        {
            bool canConvert = true;
            for (int i = 0; i < input.GetLength(0); i++)
            {
                if (input[i].Amount < inSize[i])
                    canConvert = false;
            }
            return canConvert;
        }
    }
}