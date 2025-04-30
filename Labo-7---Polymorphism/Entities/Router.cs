using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labo_7___Polymorphism.Entities
{
    class Router : Machine
    {
        public double WorkSpaceWidth { get; set; }
        public double WorkSpaceLenght { get; set; }
        public double CostPerMinute { get; set; }
        protected override int LifeSpanCostPerMinute => 50;

        public Router(string name, double width, double length, double costPerMinute) : base(name)
        {
            WorkSpaceWidth = width;
            WorkSpaceLenght = length;
            CostPerMinute = costPerMinute;

            base.LifeSpan = 2500;
        }

        public override void Use(int numberOfMinutes)
        {
            base.LifeSpan -= LifeSpanCostPerMinute * numberOfMinutes;
        }

        public override string ToString()
        {
            return $"ROUTER: '{base.Name}' ({WorkSpaceLenght}x{WorkSpaceWidth}) <lifespan: {base.LifeSpan} h>";
        }
    }
}
