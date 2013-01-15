using System;

#if WINDOWS || XBOX
static class Program
{
    static void Main(string[] args)
    {
        using (Bunka game = new Bunka())
        {
            game.Run();
        }
    }
}
#endif

