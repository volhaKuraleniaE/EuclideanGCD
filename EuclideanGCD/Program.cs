﻿using System;
using System.Diagnostics;

namespace EuclideanGCD
{
    static class EuclideanGCD
    {
        public static int CalculateGCD(int numberA, int numberB)
        {
            while (numberA != 0 && numberB != 0)
            {
                if (numberA > numberB)
                {
                    numberA %= numberB;
                }
                else
                {
                    numberB %= numberA;
                }
            }
            return numberA + numberB;
        }
        public static int CalculateGCD(int numA, int numB, int numC)
        {
            int first = CalculateGCD(numA, numB);
            int second = CalculateGCD(numB, numC);
            if (first != second)
            {
                return CalculateGCD(first, second);
            }
            return first;
        }
        public static int CalculateGCD(int numA, int numB, int numC, int numD)
        {
            int first = CalculateGCD(numA, numB, numC);
            int second = CalculateGCD(numB, numC, numD);
            if (first != second)
            {
                return CalculateGCD(first, second);
            }
            return first;
        }
        public static int CalculateGCD(int numA, int numB, int numC, int numD, int numE)
        {
            int first = CalculateGCD(numA, numB, numC);
            int second = CalculateGCD(numC, numD, numE);
            if (first != second)
            {
                return CalculateGCD(first, second);
            }
            return first;
        }
        public static int FindMethod(int[] myList)
        {
            int length = myList.Length;
            switch (length)
            {
                case 3:
                    return CalculateGCD(myList[0], myList[1], myList[2]);
                case 4:
                    return CalculateGCD(myList[0], myList[1], myList[2], myList[3]);
                case 5:
                    return CalculateGCD(myList[0], myList[1], myList[2], myList[3], myList[4]);
                default:
                    break;
            }
            return CalculateGCD(myList[0], myList[1]);
        }
    }
    static class SteinGCD
    {
        public static int CalculateGCD(int numA, int numB, out TimeSpan time)
        {
            Stopwatch timeWatch = new Stopwatch();
            timeWatch.Start();
            if (numA == 0)
            {
                timeWatch.Stop();
                time = timeWatch.Elapsed;
                return numB;
            }
            if (numB == 0)
            {
                timeWatch.Stop();
                time = timeWatch.Elapsed;
                return numA;
            }
            int result;
            for (result = 0; ((numA | numB) & 1) == 0; ++result)
            {
                numA >>= 1;
                numB >>= 1;
            }
            while ((numA & 1) == 0)
                numA >>= 1;
            do
            {
                while ((numB & 1) == 0)
                    numB >>= 1;
                if (numA > numB)
                {

                    int temp = numA;
                    numA = numB;
                    numB = temp;
                }
                numB -= numA;
            } while (numB != 0);
            timeWatch.Stop();
            time = timeWatch.Elapsed;
            return numA << result;
        }
    }
    class Program
    {
        private static int[] InputNumbers()
        {
            Console.WriteLine("Enter numbers: ");
            string line = Console.ReadLine();
            CheckException(line);
            string[] strArray = line.Split(',');
            return Array.ConvertAll(strArray, int.Parse);
        }
        private static void CheckException(string line)
        {
            foreach (var item in line)
            {
                if (char.IsLetter(item) || char.IsWhiteSpace(item))
                {
                    throw new FormatException(message: "Invalid symbol or space");
                }
                if (char.IsPunctuation(item) && item != ',')
                {
                    throw new FormatException(message: $"Invalid symbol: {item}");
                }
            }
        }
        private static void FindEuclideanGCD()
        {
            try
            {
                int[] numbersList = InputNumbers();
                int result = EuclideanGCD.FindMethod(numbersList);
                Console.WriteLine($"Euclidean's GCD is: {result}");
                Console.WriteLine();
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        private static void FindSteinGCD()
        {
            try
            {
                int[] steinList = InputNumbers();
                int steinResult = SteinGCD.CalculateGCD(steinList[0], steinList[1],out TimeSpan time);
                Console.WriteLine($"Stein's GCD is: {steinResult}, time: {time}");
                Console.WriteLine();
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        static void Main()
        {
            FindEuclideanGCD();
            FindSteinGCD();
        }
    }
}

