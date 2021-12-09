using System;
using System.Collections.Generic;
using System.IO;

namespace Aoc.Runner
{
    public abstract class Day
    {
        public bool IsTest { get; set; } = false;
        public virtual List<Test> Tests { get; set; } = new();
        public virtual uint Number()
        {
            try
            {
                return uint.Parse(NumberString());
            }
            catch
            {
                return 999;
            }
        }
        public virtual string NumberString()
        {
            var className = GetType().Name;
            var numberString = className[(className.IndexOf("Day") + 3)..].TrimStart('_');
            return numberString;
        }
        public virtual string GetInput()
        {
            var path = Path.Combine("Inputs", $"Day_{NumberString()}.txt");
            return File.ReadAllText(path);
        }

        public virtual string GetInput(string suffix)
        {
            var path = Path.Combine("Inputs", $"Day_{NumberString()}_{suffix}.txt");
            return File.ReadAllText(path);
        }

        public virtual string SolveA() => SolveA(GetInput());
        public virtual string SolveA(string Input) => throw new NotImplementedException();
        public virtual string SolveB() => SolveB(GetInput());
        public virtual string SolveB(string Input) => throw new NotImplementedException();

    }
    
    public static class LogHelpers
    {
        private static bool IsTest = false;
        public static void SetTest() => IsTest = true;

        public static void Log(string t)
        {
            if (IsTest)
            {
                Console.WriteLine(t);
            }
        }
    }


    public class Test
    {
        public string Output { get; set; } = "NONE";
        public string Name { get; }
        public string Input { get; }
        public string ExpectedOutput { get; }
        public Func<string, string> Solve { get; }

        public Test(string name, string input, string expectedOutput, Func<string, string> solve)
        {
            Name = name;
            Input = input;
            ExpectedOutput = expectedOutput;
            Solve = solve;
        }

        public bool Run()
        {
            Output = Solve.Invoke(Input);
            return Output == ExpectedOutput;
        }
    }
}