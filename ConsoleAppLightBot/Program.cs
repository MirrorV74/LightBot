char[,] FieldCreation()
{
    char[,] fieldLVL1 = new char[3, 3];
    for (int i = 0; i < fieldLVL1.GetLength(0); i++)
    {
        for (int j = 0; j < fieldLVL1.GetLength(1); j++)
        {
            fieldLVL1[i, j] = 'a';
        }
    }
    fieldLVL1[0, 0] = 'X';
    fieldLVL1[2, 2] = 'O';

    return fieldLVL1;
}

void PrintField(char[,] field)
{
    for (int i = 0; i < field.GetLength(0); i++)
    {
        for (int j = 0; j < field.GetLength(1); j++)
        {
            Console.Write(field[i,j]);
        }
        Console.WriteLine();
    }
}
char[,] field = FieldCreation();

PrintField(field);