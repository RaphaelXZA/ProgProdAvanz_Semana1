using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgProdAvanz_Semana1
{
    internal class Game : IGame
    {
        private Location _currentLocation;
        private List<Key> _inventory;
        private bool _isGameOver;
        private bool _victory;
        private bool _inRiddleMode = false;

        public Game()
        {
            _inventory = new List<Key>();
            _isGameOver = false;
            _victory = false;
            _currentLocation = InitializeGame();
        }

        private Location InitializeGame()
        {
            //Crear llaves
            Key[] keys = new Key[4];
            keys[0] = new Key("Llave Corazón", "Una llave de color rojo brillante con forma de corazón.");
            keys[1] = new Key("Llave Pica", "Una llave de color azul intenso con forma de pica.");
            keys[2] = new Key("Llave Trébol", "Una llave de color verde esmeralda con forma de trebol de cuatro hojas.");
            keys[3] = new Key("Llave Diamante", "Una llave dorada con forma de diamante, más grande que las demás. Parece ser muy importante.");

            //Crear ubicaciones
            Location centerArea = new Location("Área Central", "Estás en un área central con cuatro pasillos que se extienden en los diferentes puntos cardinales.");
            Location northCorridor = new Location("Pasillo Norte", "Un largo pasillo que se extiende hacia el norte. Hay una puerta al final.");
            Location eastCorridor = new Location("Pasillo Este", "Un largo pasillo que se extiende hacia el este. Hay una puerta al final.");
            Location southCorridor = new Location("Pasillo Sur", "Un largo pasillo que se extiende hacia el sur. Hay una puerta al final.");
            Location westCorridor = new Location("Pasillo Oeste", "Un largo pasillo que se extiende hacia el oeste. Hay una puerta al final.");

            //Crear habitaciones detrás de las puertas
            Location northRoom = new Location("Habitación Norte", "Una habitación fría con paredes de piedra. Hay un acertijo grabado en la pared.");
            Location eastRoom = new Location("Habitación Este", "Una habitación iluminada por antorchas. Hay un acertijo en un pedestal.");
            Location southRoom = new Location("Habitación Sur", "Una habitación húmeda con goteras. Hay un acertijo tallado en el suelo.");
            Location westRoom = new Location("Habitación Oeste", "Una habitación cubierta de enredaderas. Hay un acertijo colgando del techo.");

            //Crear salidas
            centerArea.AddExit("norte", northCorridor);
            centerArea.AddExit("este", eastCorridor);
            centerArea.AddExit("sur", southCorridor);
            centerArea.AddExit("oeste", westCorridor);

            northCorridor.AddExit("sur", centerArea);
            eastCorridor.AddExit("oeste", centerArea);
            southCorridor.AddExit("norte", centerArea);
            westCorridor.AddExit("este", centerArea);

            northRoom.AddExit("sur", northCorridor);
            eastRoom.AddExit("oeste", eastCorridor);
            southRoom.AddExit("norte", southCorridor);
            westRoom.AddExit("este", westCorridor);

            //Crear puertas
            Door northDoor = new Door("Puerta Norte", "Una puerta de madera con un símbolo de corazon rojo.", "Llave Corazón", northRoom);
            Door eastDoor = new Door("Puerta Este", "Una puerta de hierro con un símbolo de pica azul.", "Llave Pica", eastRoom);
            Door southDoor = new Door("Puerta Sur", "Una puerta de bronce con un símbolo de trébol verde.", "Llave Trébol", southRoom);
            Door westDoor = new Door("Puerta Oeste", "Una puerta de plata con un símbolo de diamente dorado.", "Llave Diamante", westRoom);

            //Crear acertijos
            Riddle northRiddle = new Riddle(
                "Acertijo del Norte",
                "¿Qué mes tiene 28 días?",
                new string[] { "Marzo", "Mayo", "Todos", "Febrero" },
                2, //Indice de la respuesta (0, 1, 2 o 3)
                keys[1] //Recompensa: Llave Pica
            );

            Riddle eastRiddle = new Riddle(
                "Acertijo del Este",
                "Si me nombras desaparezco, ¿qué soy?",
                new string[] { "El miedo", "El silencio", "La edad", "El humo" },
                1, //Indice de la respuesta (0, 1, 2 o 3)
                keys[2] // Recompensa: Llave Trébol
            );

            Riddle southRiddle = new Riddle(
                "Acertijo del Sur",
                "No tengo trono ni reina, ni nadie que me comprenda, pero sigo siendo el ________",
                new string[] { "Rey", "Mejor", "Papá de los helados", "Señor de la noche" },
                0, //Indice de la respuesta (0, 1, 2 o 3)
                keys[3] // Recompensa: Llave Diamante
            );

            Riddle westRiddle = new Riddle(
                "Acertijo del Oeste",
                "Si me tienes, quieres compartirme. Si me compartes, ya no me tienes. ¿Qué soy?",
                new string[] { "La risa", "Un secreto", "La felicidad", "La amistad" },
                1, //Indice de la respuesta (0, 1, 2 o 3)
                null //Recompensa: Fin del juego
            );

            //Colocar entidades en ubicaciones
            centerArea.AddEntity(keys[0]); //Llave Corazon al principio

            northCorridor.AddEntity(northDoor);
            eastCorridor.AddEntity(eastDoor);
            southCorridor.AddEntity(southDoor);
            westCorridor.AddEntity(westDoor);

            northRoom.AddEntity(northRiddle);
            eastRoom.AddEntity(eastRiddle);
            southRoom.AddEntity(southRiddle);
            westRoom.AddEntity(westRiddle);

            //Establecer ubicación inicial
            _currentLocation = centerArea;
            return centerArea;
        }

        public void Execute()
        {
            Console.WriteLine("===============================================");
            Console.WriteLine("   AVENTURA DE TEXTO - EL LABERINTO DE LLAVES  ");
            Console.WriteLine("===============================================");
            Console.WriteLine("\nEres un explorador atrapado en un misterioso laberinto.");
            Console.WriteLine("Debes encontrar las llaves para abrir las puertas y resolver los acertijos para escapar.");
            Console.WriteLine("\nPresiona cualquier tecla para comenzar...");
            Console.ReadKey();

            while (!_isGameOver)
            {
                Console.Clear();
                Console.WriteLine($"=== {_currentLocation.Name} ===");
                Console.WriteLine(_currentLocation.GetFullDescription());

                if (_victory)
                {
                    Console.WriteLine("\n¡FELICIDADES! Has resuelto todos los acertijos y has logrado escapar del laberinto.");
                    Console.WriteLine("\n¿Quieres jugar de nuevo?");
                    Console.WriteLine("1. Sí");
                    Console.WriteLine("2. No (Salir)");

                    string choice = (Console.ReadLine() ?? "").Trim();
                    if (choice == "1")
                    {
                        _inventory.Clear();
                        _isGameOver = false;
                        _victory = false;
                        InitializeGame();
                        continue;
                    }
                    else
                    {
                        _isGameOver = true;
                        break;
                    }
                }

                DisplayOptions();
                ProcessInput();
            }

            Console.WriteLine("\nGracias por jugar.");
        }

        private void DisplayOptions()
        {
            // Si estamos en modo acertijo, mostrar solo las opciones de respuesta
            if (_inRiddleMode)
            {
                foreach (var entity in _currentLocation.Entities)
                {
                    if (entity is Riddle riddle)
                    {
                        // Volver a mostrar el acertijo para que el jugador pueda leerlo
                        Console.WriteLine($"\n=== {riddle.Name} ===");
                        Console.WriteLine(riddle.Interact());

                        // Mostrar las opciones de respuesta
                        Console.WriteLine("\nSelecciona tu respuesta:");
                        for (int i = 0; i < riddle.Options.Length; i++)
                        {
                            Console.WriteLine($"{i + 1}. {riddle.Options[i]}");
                        }
                        break;
                    }
                }
                return; // Terminar el método para no mostrar las opciones normales
            }

            // Si no estamos en modo acertijo, mostrar las opciones normales
            Console.WriteLine("\n¿Qué quieres hacer?");
            Console.WriteLine("1. Ver inventario");
            Console.WriteLine("2. Interactuar");

            // Mostrar opciones de movimiento
            int optionIndex = 3;
            foreach (var exit in _currentLocation.Exits)
            {
                Console.WriteLine($"{optionIndex}. Moverte a {exit.Key}");
                optionIndex++;
            }

            // Verificar si hay una llave para recoger
            foreach (var entity in _currentLocation.Entities)
            {
                if (entity is Key key && !key.IsCollected)
                {
                    Console.WriteLine($"{optionIndex}. Recoger {key.Name}");
                    break;
                }
            }
        }

        private void ProcessInput()
        {
            string input = Console.ReadLine() ?? "";

            if (!int.TryParse(input, out int choice))
            {
                Console.WriteLine("Entrada no válida. Presiona cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }

            if (_inRiddleMode)
            {
                foreach (var entity in _currentLocation.Entities)
                {
                    if (entity is Riddle riddle)
                    {
                        if (choice >= 1 && choice <= riddle.Options.Length)
                        {
                            int answerIndex = choice - 1;
                            bool isCorrect = riddle.CheckAnswer(answerIndex);
                            _inRiddleMode = false;

                            if (isCorrect)
                            {
                                Console.WriteLine("\n¡Respuesta correcta!");

                                //Último acertijo
                                if (riddle.Reward == null)
                                {
                                    Console.WriteLine("\nEl suelo tiembla y las paredes comienzan a moverse...");
                                    Console.WriteLine("\nPresiona cualquier tecla para continuar...");
                                    Console.ReadKey();
                                    Console.WriteLine("Una salida se abre ante ti, mostrando la luz del día...");
                                    Console.WriteLine("\nPresiona cualquier tecla para continuar...");
                                    Console.ReadKey();
                                    _victory = true;
                                }
                                else
                                {
                                    Console.WriteLine($"Como recompensa, obtienes la {riddle.Reward.Name}.");
                                    riddle.Reward.Collect();
                                    _inventory.Add(riddle.Reward);
                                    _currentLocation.RemoveEntity(riddle); //Eliminar acertijo resuelto
                                    Console.WriteLine("\nPresiona cualquier tecla para continuar...");
                                    Console.ReadKey();
                                }
                            }
                            else
                            {
                                Console.WriteLine("\n¡Respuesta incorrecta!");
                                Console.WriteLine("\nPresiona cualquier tecla para continuar...");
                                Console.ReadKey();
                                Console.WriteLine("El suelo se abre bajo tus pies y caes en un abismo sin fin...");
                                Console.WriteLine("\nPresiona cualquier tecla para continuar...");
                                Console.ReadKey();
                                Console.WriteLine("\nFIN DEL JUEGO");
                                Console.WriteLine("\nPresiona cualquier tecla para reiniciar...");
                                Console.ReadKey();

                                //Reiniciar el juego
                                _inventory.Clear();
                                _inRiddleMode = false;
                                InitializeGame();
                            }
                            return;
                        }
                        else
                        {
                            Console.WriteLine("Opción no válida. Presiona cualquier tecla para continuar...");
                            Console.ReadKey();
                            return;
                        }
                    }
                }
            }
            else
            {

                switch (choice)
                {
                    case 1: 
                        ShowInventory();
                        break;
                    case 2: 
                        InteractWithEntity();
                        break;
                    default:
                        HandleMovementOrAction(choice);
                        break;
                }
            }
        }

        private void ShowInventory()
        {
            Console.WriteLine("\n=== INVENTARIO ===");
            if (_inventory.Count == 0)
            {
                Console.WriteLine("Tu inventario esta vacio.");
            }
            else
            {
                for (int i = 0; i < _inventory.Count; i++)
                {
                    Console.WriteLine($"- {_inventory[i].Name}: {_inventory[i].Description}");
                }
            }
            Console.WriteLine("\nPresiona cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private GameEntity? GetInteractingEntity()
        {
            foreach (var entity in _currentLocation.Entities)
            {
                if (entity is Door || (entity is Riddle riddle && !riddle.IsSolved))
                {
                    return entity;
                }
            }
            return null;
        }

        private void InteractWithEntity()
        {
            GameEntity? entity = GetInteractingEntity();

            if (entity == null)
            {
                Console.WriteLine("No hay nada con lo que interactuar aquí.");
                Console.WriteLine("\nPresiona cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }

            if (entity is Door door)
            {
                HandleDoorInteraction(door);
            }
            else if (entity is Riddle riddle)
            {
                HandleRiddleInteraction(riddle);
            }
        }

        private void HandleDoorInteraction(Door door)
        {
            Console.WriteLine("\n" + door.Interact());

            if (door.IsLocked)
            {
                bool hasKey = false;
                foreach (var key in _inventory)
                {
                    if (key.Name == door.RequiredKeyName)
                    {
                        hasKey = true;
                        break;
                    }
                }

                if (hasKey)
                {
                    if (door.Unlock(door.RequiredKeyName))
                    {
                        Console.WriteLine($"¡Has usado la {door.RequiredKeyName} para abrir la puerta!");
                        Console.WriteLine("\nPresiona cualquier tecla para continuar...");
                        Console.ReadKey();
                        _currentLocation = door.LocationBehind;
                        return;
                    }
                }
                else
                {
                    Console.WriteLine($"No tienes la llave necesaria ({door.RequiredKeyName}) para abrir esta puerta.");
                }
            }
            else
            {
                Console.WriteLine("Entras por la puerta abierta...");
                Thread.Sleep(1000);
                _currentLocation = door.LocationBehind;
                return;
            }

            Console.WriteLine("\nPresiona cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private void HandleRiddleInteraction(Riddle riddle)
        {
            Console.WriteLine("\n" + riddle.Interact());
            _inRiddleMode = true; 
            Console.WriteLine("\nPresiona cualquier tecla para ver las opciones de respuesta...");
            Console.ReadKey();
        }

        private void HandleMovementOrAction(int choice)
        {

            int exitIndex = 0;
            foreach (var exit in _currentLocation.Exits)
            {
                if (choice == exitIndex + 3) 
                {
                    _currentLocation = exit.Value;
                    _inRiddleMode = false;
                    return;
                }
                exitIndex++;
            }

            if (choice == exitIndex + 3)
            {
                foreach (var entity in _currentLocation.Entities)
                {
                    if (entity is Key key && !key.IsCollected)
                    {
                        key.Collect();
                        _inventory.Add(key);
                        _currentLocation.RemoveEntity(key);
                        Console.WriteLine($"\nHas recogido la {key.Name}.");
                        Console.WriteLine("\nPresiona cualquier tecla para continuar...");
                        Console.ReadKey();
                        return;
                    }
                }
            }

            if (_inRiddleMode)
            {
                foreach (var entity in _currentLocation.Entities)
                {
                    if (entity is Riddle riddle)
                    {
                        GameEntity? interactingEntity = GetInteractingEntity();
                        if (interactingEntity == riddle)
                        {
                            int answerIndex = choice - 1;
                            if (answerIndex >= 0 && answerIndex < riddle.Options.Length)
                            {
                                bool isCorrect = riddle.CheckAnswer(answerIndex);
                                _inRiddleMode = false;

                                if (isCorrect)
                                {
                                    Console.WriteLine("\n¡Respuesta correcta!");

                                    //Acertijo final
                                    if (riddle.Reward == null)
                                    {
                                        Console.WriteLine("\nEl suelo tiembla y las paredes comienzan a moverse...");
                                        Console.WriteLine("\nPresiona cualquier tecla para continuar...");
                                        Console.ReadKey();
                                        Console.WriteLine("Una salida se abre ante ti, mostrando la luz del día...");
                                        Console.WriteLine("\nPresiona cualquier tecla para continuar...");
                                        Console.ReadKey();
                                        _victory = true;
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Como recompensa, obtienes la {riddle.Reward.Name}.");
                                        riddle.Reward.Collect();
                                        _inventory.Add(riddle.Reward);
                                        Console.WriteLine("\nPresiona cualquier tecla para continuar...");
                                        Console.ReadKey();
                                    }

                                    return;
                                }
                                else
                                {
                                    Console.WriteLine("\n¡Respuesta incorrecta!");
                                    Console.WriteLine("\nPresiona cualquier tecla para continuar...");
                                    Console.ReadKey();
                                    Console.WriteLine("El suelo se abre bajo tus pies y caes en un abismo sin fin...");
                                    Console.WriteLine("\nPresiona cualquier tecla para continuar...");
                                    Console.ReadKey();
                                    Console.WriteLine("\nFIN DEL JUEGO");
                                    Console.WriteLine("\nPresiona cualquier tecla para reiniciar...");
                                    Console.ReadKey();

                                    // Reiniciar el juego
                                    _inventory.Clear();
                                    _inRiddleMode = false;
                                    InitializeGame();
                                    return;
                                }
                            }
                        }
                    }
                }
            }

            Console.WriteLine("Opción no válida. Presiona cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }
}
