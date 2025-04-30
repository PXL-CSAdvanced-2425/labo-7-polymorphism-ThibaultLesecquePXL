using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labo_7___Polymorphism.Entities
{
    class General : Machine
    {
        protected override int LifeSpanCostPerMinute => 1;

        public General(string name) : base(name) 
        {
            base.LifeSpan = 1000;
        }

        public override void Use(int numberOfMinutes)
        {
            base.LifeSpan -= LifeSpanCostPerMinute * numberOfMinutes;
        }

        public override string ToString()
        {
            return $"{base.Name} <lifespan: {base.LifeSpan} h>";
        }
    }
}
