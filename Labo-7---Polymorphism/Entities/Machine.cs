using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labo_7___Polymorphism.Entities
{
    abstract class Machine
    {
        public string Name { get; set; }
        public int LifeSpan { get; set; }
        public float Price { get; set; }
        public bool OutOfUse
        {
            get { return OutOfUse; }
            set
            {
                if (LifeSpan <= 0)
                {
                    OutOfUse = true;
                }
                OutOfUse = false;
            }
        }
        protected abstract int LifeSpanCostPerMinute { get; }

        public Machine(string name)
        {
            Name = name;
        }

        public abstract void Use(int numberOfMinutes);

        public string LifeSpanInfo()
        {
            return $"OUT OF USE OF <lifespan: {LifeSpan} h>";
        }

        public override string ToString()
        {
            return LifeSpanInfo();
        }
    }
}
