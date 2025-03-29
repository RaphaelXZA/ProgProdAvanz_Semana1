using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgProdAvanz_Semana1
{
    internal abstract class GameEntity
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }

        protected GameEntity(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public abstract string Interact();

    }
}
