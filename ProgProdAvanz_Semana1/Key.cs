using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProgProdAvanz_Semana1
{
    internal class Key : GameEntity
    {
        public bool IsCollected { get; private set; }
        public Key(string name, string description) : base(name, description)
        {
            IsCollected = false;
        }

        public void Collect()
        {
            IsCollected = true;
        }

        public override string Interact()
        {
            if (!IsCollected)
            {
                return $"Puedes recoger la {Name}.";
            }
            return "Ya has recogido esta llave.";
        }
    }
}
