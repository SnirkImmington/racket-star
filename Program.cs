using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacketInterpreter
{
    class Program
    {
        private static Dictionary<string, object> values;

        static void Main(string[] args)
        {
            Console.WriteLine("Let's make a racket.");
            Console.WriteLine();

            values = new Dictionary<string, object>();

            while (true)
            {
                var line = ReadInterpreterLine();

                int generation = 0;
                for (int i = 0; i < line.Length; i++)
                {
                    // We've come to an ending
                    if (line[i] == ')')
                    {
                        for (int open = i; open >= 0; open--)
                        {
                            if (line[open] == '(')
                            {
                                //Console.WriteLine("Generation " + generation + ":");
                                generation++;
                                
                                var expression = line.Substring(open, i - open + 1);
                                //Console.WriteLine("Found command " + expression + ".");
                                
                                line = line.Remove(open, i - open + 1);

                                string answer = "Error";
                                try
                                {
                                    answer = handleCommand(ParseArguments(expression));
                                }
                                catch (Exception ex)
                                {
                                    answer = ex.Message;
                                }
                                //Console.WriteLine(answer);

                                line = line.Insert(open, answer);
                                //Console.WriteLine("Line = " + line);
                                //Console.WriteLine();
                                i = 0; open = 0;
                                break;
                            }
                        }
                        //break;
                    }
                }
                
                // The expression has been simplified.
                Console.WriteLine(line);
                Console.WriteLine();
            }
        }

        static String ReadInterpreterLine()
        {
            Console.Write("> ");
            Console.ForegroundColor = ConsoleColor.Blue;
            var turn = Console.ReadLine();
            
            // Handle exit command
            if (turn == "exit") Environment.Exit(0);

            Console.ForegroundColor = ConsoleColor.White;
            return turn;
        }

        // Lazy argument parsing
        static string[] ParseArguments(string input)
        {
            if (input[0] == '(')
                input = input.Trim('(', ')');

            return input.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
        }

        static void addDefinition(string variableName, string input)
        {
            // Possible int, double values.
            int intVal; double doubleVal;

            // Parse ints or doubles
            if (int.TryParse(input, out intVal))
            {
                values.Add(variableName, intVal);
            }
            else if (double.TryParse(input, out doubleVal))
            {
                values.Add(variableName, doubleVal);
            }
               
            // Check for variable
            else if (values.ContainsKey(input))
            {
                values.Add(variableName, values[input]);
            }

            // Weak string check
            else values.Add(variableName, input);
        }

        static string getVariable(string name)
        {
            return values[name].ToString();
        }

        static string handleCommand(string[] arguments)
        {
            if (arguments.Length == 0) return "";
            switch(arguments[0])
            {
                case "+":
                    {
                        var answer = int.Parse(arguments[1]);
                        for (int i = 2; i < arguments.Length; i++)
                        {
                            answer += int.Parse(arguments[i]);
                        }
                        return answer.ToString();
                    }

                case "-":
                    {
                        var answer = int.Parse(arguments[1]);
                        for (int i = 2; i < arguments.Length; i++)
                        {
                            answer -= int.Parse(arguments[i]);
                        }
                        return answer.ToString();
                    }

                case "++":
                    return (int.Parse(arguments[1]) + 1).ToString();

                case "--":
                    return (int.Parse(arguments[1]) - 1).ToString();

                case "/":
                    {
                        var answer = int.Parse(arguments[1]);
                        for (int i = 2; i < arguments.Length; i++)
                        {
                            answer /= int.Parse(arguments[i]);
                        }
                        return answer.ToString();
                    }

                case "*":
                    {
                        var answer = int.Parse(arguments[1]);
                        for (int i = 2; i < arguments.Length; i++)
                        {
                            answer *= int.Parse(arguments[i]);
                        }
                        return answer.ToString();
                    }
                case "^":
                    {
                        if (arguments.Length == 3)
                        {
                            int baseInt = int.Parse(arguments[1]);
                            int exponent = int.Parse(arguments[2]);
                            return Math.Pow(baseInt, exponent).ToString();
                        }
                        return "Incorrect number of arguments! (expected 2)";
                        
                    }
                case "define":
                    {
                        var newArgs = arguments.TakeWhile((args, index) => index > 0).ToArray();
                        return "define";
                        //break;
                    }
                default: return "null";
            }
        }
    }
}
