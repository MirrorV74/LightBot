using System.Diagnostics;
using ConsoleAppLightBot;

char[,] ReedLevel()
{
    Console.WriteLine("Input name of file that contains level");
    string levelName = Console.ReadLine();
    
    StreamReader reader = new StreamReader($"{levelName}");

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

bool AnotherOne()
{
    bool inGame = true;
    Console.WriteLine("You won!");
    Console.WriteLine("Wanna play again");
    Console.WriteLine("1. Yes");
    Console.WriteLine("2. No");
    int answer = int.Parse(Console.ReadLine());
    if (answer == 2)
    {
        inGame = false;
    }
    
    return inGame;
}

void PrintField(char[,] field)
{
    for (int i = 0; i < field.GetLength(0); i++)
    {
        for (int j = 0; j < field.GetLength(1); j++)
        {
            switch (field[i,j])
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
                case Tile.Wall:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case Tile.Portal:
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
            if (field[i,j] == Tile.Player)
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
    bool levelCompleted = false;
    for (int i = 0; i < moves.Length && allowedCombination; i++)
    {
        switch (moves[i])
        {
            case 'w':
            {
                if (playerY != 0)
                {
                    playerY--;
                    switch (field[playerY,playerX])
                    {
                        case Tile.Trail:
                        case Tile.Empty:
                        {
                            field[playerY+1, playerX] = Tile.Trail;
                            break;
                        }
                        case Tile.Wall:
                        {
                            playerY++;
                            break;
                        }
                    }
                }
                else
                {
                    allowedCombination = false;
                    Console.WriteLine("Impossible movement");
                }
                break;
            }
            case 'a':
            {
                if (playerX != 0)
                {
                    playerX--;
                    switch (field[playerY,playerX])
                    {
                        case Tile.Trail:
                        case Tile.Empty:
                        {
                            field[playerY, playerX+1] = Tile.Trail;
                            break;
                        }
                        case Tile.Wall:
                        {
                            playerX++;
                            break;
                        }
                    }
                }
                else
                {
                    allowedCombination = false;
                    Console.WriteLine("Impossible movement");
                }
                break;
            }
            case 's':
            {
                if (playerY != field.GetLength(0)-1)
                {
                    playerY++;
                    switch (field[playerY,playerX])
                    {
                        case Tile.Trail:
                        case Tile.Empty:
                        {
                            field[playerY-1, playerX] = Tile.Trail;
                            break;
                        }
                        case Tile.Wall:
                        {
                            playerY--;
                            break;
                        }
                    }
                }
                else
                {
                    allowedCombination = false;
                    Console.WriteLine("Impossible movement");
                }
                break;
            }
            case 'd':
            {
                if (playerX != field.GetLength(1)-1)
                {
                    playerX++;
                    switch (field[playerY,playerX])
                    {
                        case Tile.Trail:
                        case Tile.Empty:
                        {
                            field[playerY, playerX-1] = Tile.Trail;
                            break;
                        }
                        case Tile.Wall:
                        {
                            playerX--;
                            break;
                        }
                    }
                }
                else
                {
                    allowedCombination = false;
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

        if (allowedCombination)
        {
            Thread.Sleep(300);
            field[playerY, playerX] = Tile.Player;
            Console.Clear();
            PrintField(field);
        }
    }

    if (field[playerX,playerY] == Tile.Portal)
    {
        levelCompleted = true;
    }
    return levelCompleted;
}

bool inGame = true;
do
{
    char[,] field = ReedLevel();
    bool levelCompleted = false;
    while (!levelCompleted)
    {
        Console.Clear();
        PrintField(field);
        char[] moves = ReadMoves();
        levelCompleted =  PlayMoves(moves, field);
    }
    inGame = AnotherOne();
} while (inGame);



