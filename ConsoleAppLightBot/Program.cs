using System.Diagnostics;

char[,] FieldCreation()
{
    StreamReader reader = new StreamReader("1level.txt");

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
    int playerX = playerCoordinates[0];
    int playerY = playerCoordinates[1];
    field[playerX, playerY] = 'a';
    bool allowedCombination = true;
    for (int i = 0; i < moves.Length && allowedCombination; i++)
    {
        switch (moves[i])
        {
            case 'w':
            {
                if (playerY != 0)
                {
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
                if (playerY != field.GetLength(1))
                {
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
                if (playerX != field.GetLength(0))
                {
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
        field[playerX, playerY] = 'X';
        PrintField(field);
    }
}

char[,] field = FieldCreation();
char[] moves = ReadMoves();
PlayMoves(moves, field);
