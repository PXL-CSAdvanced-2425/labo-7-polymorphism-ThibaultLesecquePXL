using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labo_7___Polymorphism.Entities
{
    class LaserCutter : Router
    {
        protected override int LifeSpanCostPerMinute => 1500;

        public double Accuracy { get; set; }

        public LaserCutter(string name, double width, double length, double costPerMinute, double accuracy) : base(name, width, length, costPerMinute)
        {
            Accuracy = accuracy;
            base.LifeSpan = 5000;
        }

        public override void Use(int numberOfMinutes)
        {
            base.Use(numberOfMinutes);
            base.LifeSpan -= 100;
        }

        public override string ToString()
        {
            return $"LASER: '{base.Name}' ({WorkSpaceLenght}x{WorkSpaceWidth}) ['accuracy: {Accuracy}] <lifespan: {base.LifeSpan} h>";
        }
    }
}
