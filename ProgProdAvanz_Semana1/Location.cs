using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgProdAvanz_Semana1
{
    internal class Location
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Dictionary<string, Location> Exits { get; private set; }
        public List<GameEntity> Entities { get; private set; }

        public Location(string name, string description)
        {
            Name = name;
            Description = description;
            Exits = new Dictionary<string, Location>();
            Entities = new List<GameEntity>();
        }

        public void AddExit(string direction, Location location)
        {
            Exits[direction] = location;
        }

        public void AddEntity(GameEntity entity)
        {
            Entities.Add(entity);
        }

        public void RemoveEntity(GameEntity entity)
        {
            Entities.Remove(entity);
        }

        public string GetFullDescription()
        {
            string description = $"{Description}\n";

            //Entidades en la localizacion actual
            if (Entities.Count > 0)
            {
                description += "\nPuedes ver:\n";
                for (int i = 0; i < Entities.Count; i++)
                {
                    description += $"- {Entities[i].Name}\n";
                }
            }

            //Caminos en la localizacion actual
            if (Exits.Count > 0)
            {
                description += "\nCaminos disponibles:\n";
                foreach (var exit in Exits)
                {
                    description += $"- {exit.Key}\n";
                }
            }

            return description;
        }
    }
}
