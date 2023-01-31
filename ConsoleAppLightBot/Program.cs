using System.Diagnostics;

char[,] FieldCreation()
{
    StreamReader reader = new StreamReader("level1.txt");

    int cols = int.Parse(reader.ReadLine());
    int rows = int.Parse(reader.ReadLine());

    char[,] fieldLVL1 = new char[cols, rows];
    for (int i = 0; i < fieldLVL1.GetLength(0); i++)
    {
        char[] strfield = reader.ReadLine().ToCharArray();
        for (int j = 0; j < strfield.Length; j++)
        {
            fieldLVL1[i, j] = strfield[j];
        }
    }

    reader.Close();

    return fieldLVL1;
}

void PrintField(char[,] field)
{
    for (int i = 0; i < field.GetLength(0); i++)
    {
        for (int j = 0; j < field.GetLength(1); j++)
        {
            switch (field[i,j])
            {
                case 'a':
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case 'o':
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case 'X':
                    Console.ForegroundColor = ConsoleColor.Red;
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
            if (field[i,j] == 'X')
            {
                playerCoordinates[0] = i;
                playerCoordinates[1] = j;
            }
        }
    }
    return playerCoordinates;
}

void PlayMoves(char[] moves, char[,] field)
{
    int[] playerCoordinates = FindPlayer(field);
    int playerY = playerCoordinates[0];
    int playerX = playerCoordinates[1];
    bool allowedCombination = true;
    for (int i = 0; i < moves.Length && allowedCombination; i++)
    {
        switch (moves[i])
        {
            case 'w':
            {
                if (playerY != 0)
                {
                    field[playerY, playerX] = 'a';
                    playerY--;
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
                    field[playerY, playerX] = 'a';
                    playerX--;
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
                    field[playerY, playerX] = 'a';
                    playerY++;
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
                    field[playerY, playerX] = 'a';
                    playerX++;
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
            field[playerY, playerX] = 'X';
            Console.Clear();
            PrintField(field);
        }
    }
}

char[,] field = FieldCreation();
char[] moves = ReadMoves();
PlayMoves(moves, field);
