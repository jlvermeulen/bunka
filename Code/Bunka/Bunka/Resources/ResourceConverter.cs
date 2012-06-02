using Microsoft.Xna.Framework;

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
            {
                input[i].Amount -= inSize[i];
                uint value;
                manager.ResourceCounts.TryGetValue(input[i].ResourceType, out value);
                manager.ResourceCounts[input[i].ResourceType] = value - inSize[i];
            }
            for (int i = 0; i < output.GetLength(0); i++)
            {
                output[i].Amount += outSize[i];
                uint value;
                manager.ResourceCounts.TryGetValue(output[i].ResourceType, out value);
                manager.ResourceCounts[output[i].ResourceType] = value - inSize[i];
            }
            timer = speed;
        }
    }

    //////////////////
    //  PROPERTIES  //
    //////////////////

    // PUBLIC

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

    // PRIVATE

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