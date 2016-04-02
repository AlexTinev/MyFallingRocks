using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
class FallingRocks
{
    const char fonSymbol = ' ';
    const int maxRandom = 10000;
    const char dwarf = '^';
    static void Main()
    {
        int probability = 50;
        char[] rocks = { '@', '#', '$', '&', '0', '♥', 'Q', };
        int height = Console.WindowHeight;
        int width = Console.WindowWidth;
        char[,] screen = new char[height, width];
        ConsoleKeyInfo keys = new ConsoleKeyInfo();
        int dwarfPosition = width / 2;
        // making the initial game field )
        for (int i = 0; i < screen.GetLength(0); i++)
        {
            for (int j = 0; j < screen.GetLength(1); j++)
            {
                screen[i, j] = fonSymbol;
            }
        }
        screen[height - 1, dwarfPosition] = dwarf;
        int time = 300;
        int coins = 0;
        int lifes = 3;
        int counterDifficulty = 0;
        char colider;
        while (lifes > 0)
        {
            while (Console.KeyAvailable == true)
            {
                keys = Console.ReadKey();
                if (keys.Key == ConsoleKey.LeftArrow && dwarfPosition > 0)
                {
                    dwarfPosition--;
                }
                else if (keys.Key == ConsoleKey.RightArrow && dwarfPosition < screen.GetLength(1) - 1)
                {
                    dwarfPosition++;
                }
            }
            GetNewScreen(probability, width, ref rocks, ref screen, dwarfPosition);
            counterDifficulty++;
            if (counterDifficulty % 100 == 0)
            {
                probability += 50;
            }
            printGameField(time, ref screen);
            colider = Coalision(ref screen, dwarfPosition);
            switch (colider)
            {
                case '$':
                    coins++;
                    break;
                case '♥':
                    lifes++;
                    break;
                case fonSymbol:
                    break;
                default:
                    lifes--;
                    break;
            }
            Console.Write(coins);
            Console.Write(new string(fonSymbol, screen.GetLength(1) - 1));
            Console.WriteLine(lifes);
            Thread.Sleep(time);
        }
        Console.Clear();
        Console.WriteLine("Game over!\n\rcoins: {0}", coins);
    }
    static char Coalision(ref char[,] screen, int dwarfPosition)
    {
        if (screen[screen.GetLength(0) - 2, dwarfPosition] != fonSymbol)
        {
            return screen[screen.GetLength(0) - 2, dwarfPosition];
        }
        return screen[screen.GetLength(0) - 2, dwarfPosition];
    }
    static string MoveDwarf(ref char[,] screen, int dwarfPosition)
    {
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < screen.GetLength(1); i++)
        {
            if (i != dwarfPosition)
            {
                builder.Append(fonSymbol);
            }
            else
            {
                builder.Append(dwarf);
            }
        }

        return builder.ToString();
    }
    static void GetNewScreen(int probability, int rowLenght, ref char[] rocks, ref char[,] screen, int dwarfPosition)
    {
        // adding the string that holds the dwarf
        string dwarfMovement = MoveDwarf(ref screen, dwarfPosition);
        for (int j = 0; j < screen.GetLength(1); j++)
        {
            screen[screen.GetLength(0) - 1, j] = dwarfMovement[j];
        }
        // moving the symbols on matrix down.
        for (int i = screen.GetLength(0) - 2; i > 0; i--)
        {
            for (int j = 0; j < screen.GetLength(1); j++)
            {
                screen[i, j] = screen[i - 1, j];
            }
        }
        // adding the new set of rocks i.e. taking the string from GetRocks and distributing it on the first line 
        string newRocks = GetRocks(probability, screen.GetLength(1), ref rocks);
        for (int j = 0; j < screen.GetLength(1); j++)
        {
            screen[0, j] = newRocks[j];
        }
    }
    static void printGameField(int time, ref char[,] screen)
    {
        string[] newLine = new string[screen.GetLength(0)];
        StringBuilder buffer = new StringBuilder();
        for (int i = 0; i < screen.GetLength(0); i++)
        {
            for (int j = 0; j < screen.GetLength(1); j++)
            {
                buffer.Append(screen[i, j]);
            }
            newLine[i] = buffer.ToString();
            buffer.Clear();
        }
        Console.Clear();
        for (int i = 0; i < newLine.Length; i++)
        {
            Console.WriteLine(newLine[i]);
        }
    }
    static string GetRocks(int probability, int rowLenght, ref char[] rocks)
    {
        StringBuilder firstLine = new StringBuilder();
        Random randomGenerator = new Random();
        int isChosenRock;
        for (int i = 0; i < rowLenght; i++)
        {
            isChosenRock = randomGenerator.Next(maxRandom);
            if (isChosenRock <= probability)
            {
                firstLine.Append(rocks[randomGenerator.Next(rocks.Length)]);
            }
            else
            {
                firstLine.Append(fonSymbol);
            }
        }
        return firstLine.ToString();
    }

}

