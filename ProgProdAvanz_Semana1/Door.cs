using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgProdAvanz_Semana1
{
    internal class Door : GameEntity
    {
        public bool IsLocked { get; private set; }
        public string RequiredKeyName { get; private set; }
        public Location LocationBehind { get; private set; }

        public Door(string name, string description, string requiredKeyName, Location locationBehind) : base(name, description)
        {
            IsLocked = true;
            RequiredKeyName = requiredKeyName;
            LocationBehind = locationBehind;
        }

        public override string Interact()
        {
            if (IsLocked)
            {
                return $"La puerta está cerrada. Necesitas la {RequiredKeyName} para abrirla.";
            }
            return $"La puerta está abierta.";
        }

        public bool Unlock(string keyName)
        {
            if (keyName == RequiredKeyName && IsLocked)
            {
                IsLocked = false;
                return true;
            }
            return false;
        }
    }
}
