using ObjectConcepts;

class Hero : Vehicle
{   

   public new void horn()
    {
        Console.WriteLine("Horn is deactivated");
    }

    public override void startMechanism()
    {
        Console.WriteLine("kick-start enabled and running");

    }
    public override void brakeMechanism()
    {
        Console.WriteLine("brake system is attached to handle");
     
    }
    public void displayName()
    {
          Console.WriteLine("Hero");
    }
}


class Program
{
    static void Main(string[] args)
    {
      Hero hero = new Hero();
        hero.startMechanism();
        hero.brakeMechanism();
        hero.horn();
        hero.displayName();
    }
}


