using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectConcepts
{
    abstract class Vehicle
    {
        public void horn()
        {
            Console.WriteLine("make sound");
        }

        public abstract void startMechanism();
        public abstract void brakeMechanism();
    }
}
