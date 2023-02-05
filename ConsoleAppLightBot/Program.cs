
using ConsoleAppLightBot;

char[,] ReadLevel()
{
    StreamReader reader = new StreamReader("level1.txt");

    int cols = int.Parse(reader.ReadLine());
    int rows = int.Parse(reader.ReadLine());

    char[,] field = new char[cols, rows];
    for (int i = 0; i < field.GetLength(0); i++)
    {
        char[] strfield = reader.ReadLine().ToCharArray();
        for (int j = 0; j < strfield.Length; j++)
        {
            field[i, j] = strfield[j];
        }
    }

    reader.Close();

    return field;
}

void PrintField(char[,] field)
{
    for (int i = 0; i < field.GetLength(0); i++)
    {
        for (int j = 0; j < field.GetLength(1); j++)
        {
            switch (field[i, j])
            {
                case Tile.Trail:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case Tile.Empty:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case Tile.Player:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case Tile.Pit:
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
                case Tile.Portal:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case Tile.Wall:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
            }

            Console.Write(field[i, j]);
        }

        Console.WriteLine();
    }
}

char[] ReadMoves()
{
    Console.WriteLine("Choose type of imput:");
    Console.WriteLine("1. From file");
    Console.WriteLine("2. By hands");
    int type = int.Parse(Console.ReadLine());
    char[] moves;

    switch (type)
    {
        case 1:
        {
            Console.WriteLine("Write file name");
            string file = Console.ReadLine();

            StreamReader reader = new StreamReader($"{file}");
            moves = reader.ReadLine().ToCharArray();
            reader.Close();

            break;
        }
        case 2:
        {
            Console.WriteLine("Write moves");
            moves = Console.ReadLine().ToCharArray();
            break;
        }
        default:
        {
            moves = new char[1];
            break;
        }
    }

    return moves;
}

int[] FindPlayer(char[,] field)
{
    int[] playerCoordinates = new int[2];
    for (int i = 0; i < field.GetLength(0); i++)
    {
        for (int j = 0; j < field.GetLength(1); j++)
        {
            if (field[i, j] == Tile.Player)
            {
                playerCoordinates[0] = i;
                playerCoordinates[1] = j;
            }
        }
    }

    return playerCoordinates;
}

bool PlayMoves(char[] moves, char[,] field)
{
    int[] playerCoordinates = FindPlayer(field);
    int playerY = playerCoordinates[0];
    int playerX = playerCoordinates[1];
    bool allowedCombination = true;
    bool levelIsCompleted = false;
    string exception = "0";
    
    for (int i = 0; i < moves.Length && allowedCombination; i++)
    {
        switch (moves[i])
        {
            case 'w':
            {
                if (playerY != 0)
                {
                    switch (field[playerY, playerX])
                    {
                        case Tile.Empty:
                        case Tile.Trail:
                        {
                            field[playerY, playerX] = Tile.Empty;
                            playerY--;
                            break;
                        }
                        case Tile.Wall:
                        {
                            break;
                        }
                        case Tile.Pit:
                        {
                            exception= "Fell into pit";
                            Console.WriteLine("You fell into the pit.");
                            break;
                        }
                    }
                }
                else
                {
                    exception = "Impossible movement";
                    Console.WriteLine("Impossible movement");
                }
                break;
            }
            case 'a':
            {
                if (playerY != 0)
                {
                    switch (field[playerY, playerX])
                    {
                        case Tile.Empty:
                        case Tile.Trail:
                        {
                            field[playerY, playerX] = Tile.Empty;
                            playerX--;
                            break;
                        }
                        case Tile.Wall:
                        {
                            break;
                        }
                        case Tile.Pit:
                        {
                            exception= "Fell into pit";
                            Console.WriteLine("You fell into the pit.");
                            break;
                        }
                    }
                }
                else
                {
                    exception = "Impossible movement";
                    Console.WriteLine("Impossible movement");
                }
                break;
            }
            case 's':
            {
                if (playerY != field.GetLength(0) - 1)
                {
                    switch (field[playerY, playerX])
                    {
                        case Tile.Trail:
                        case Tile.Empty:
                        {
                            field[playerY, playerX] = Tile.Empty;
                            playerY++;
                            break;
                        }
                        case Tile.Wall:
                        {
                            break;
                        }
                        case Tile.Pit:
                        {
                            exception= "Fell into pit";
                            Console.WriteLine("You fell into the pit.");
                            break;
                        }
                    }
                }
                else
                {
                    exception = "Impossible movement";
                    Console.WriteLine("Impossible movement");
                }
                break;
            }
            case 'd':
            {
                if (playerY != field.GetLength(1) - 1)
                {
                    switch (field[playerY, playerX])
                    {
                        case Tile.Empty:
                        case Tile.Trail:
                        {
                            field[playerY, playerX] = Tile.Empty;
                            playerX++;
                            break;
                        }
                        case Tile.Wall:
                        {
                            break;
                        }
                        case Tile.Pit:
                        {
                            exception= "Fell into pit";
                            Console.WriteLine("You fell into the pit.");
                            break;
                        }
                    }
                }
                else
                {
                    exception = "Impossible movement";
                    Console.WriteLine("Impossible movement");
                }
                break;
            }
            default:
            {
                allowedCombination = false;
                Console.WriteLine("Incorrect input");
                break;
            }
                
        }
        switch (exception)
        {
            case "0":
            {
                Thread.Sleep(300);
                field[playerY, playerX] = Tile.Player;
                Console.Clear();
                PrintField(field);
                break;
            }
            case "Fell into pit":
            {
                break;
            }
            case "Impossible movement":
            {
                allowedCombination = false;
                break;
            }
        }
    }

    if (field[playerY, playerX] == Tile.Portal)
    {
        levelIsCompleted = true;
    }

    return levelIsCompleted;
}

char[,] field = ReadLevel();
bool levelIsCompleted = false;
while (!levelIsCompleted)
{
    Console.Clear();
    PrintField(field);
    char[] moves = ReadMoves();
    levelIsCompleted = PlayMoves(moves, field);
}