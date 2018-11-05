using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace marsrover
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                Console.WriteLine("Enter upper-right coordinates by spaces :");
                int[] urcoordinate = Array.ConvertAll(Console.ReadLine().Split(' '), x => Convert.ToInt32(x));

                Console.WriteLine("Enter the positions separated by spaces :");
                string[] rover1 = Console.ReadLine().Split(' ');
                Console.WriteLine("Enter instructions :");
                string instructions1 = Console.ReadLine();

                Console.WriteLine("Enter the positions separated by spaces :");
                string[] rover2 = Console.ReadLine().Split(' ');
                Console.WriteLine("Enter instructions :");
                string instructions2 = Console.ReadLine();

                string one = Calculate(rover1[2], Convert.ToInt32(rover1[0]), Convert.ToInt32(rover1[1]),
                    instructions1);
                string two = Calculate(rover2[2], Convert.ToInt32(rover2[0]), Convert.ToInt32(rover2[1]),
                    instructions2);
                //Chainofresponsiblity solution
                //string one = CalculateWithPattern(rover1[2], Convert.ToInt32(rover1[0]), Convert.ToInt32(rover1[1]),
                //    instructions1);
                //string two = CalculateWithPattern(rover2[2], Convert.ToInt32(rover2[0]), Convert.ToInt32(rover2[1]),
                //    instructions2);

                Console.WriteLine("Expected Output:");
                Console.WriteLine(one);
                Console.WriteLine(two);
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ex: " + ex.Message);

            }
        }
        static string Calculate(string lastposition, int x, int y, string instructions)
        {

            foreach (char instruction in instructions)
            {
                if (instruction == 'L')
                {
                    if (lastposition == "N")
                        lastposition = "W";
                    else if (lastposition == "E")
                        lastposition = "N";
                    else if (lastposition == "W")
                        lastposition = "S";
                    else if (lastposition == "S")
                        lastposition = "E";
                }
                else if (instruction == 'R')
                {
                    if (lastposition == "N")
                        lastposition = "E";
                    else if (lastposition == "E")
                        lastposition = "S";
                    else if (lastposition == "W")
                        lastposition = "N";
                    else if (lastposition == "S")
                        lastposition = "W";
                }
                else if (instruction == 'M')
                {
                    if (lastposition == "N")
                        y++;
                    else if (lastposition == "S")
                        y--;
                    else if (lastposition == "E")
                        x++;
                    else if (lastposition == "W")
                        x--;
                }

            }
            return x.ToString() + y + lastposition;

        }
        static string CalculateWithPattern(string lastposition, int x, int y, string instructions) //chainofresponsibility
        {
            Handler h1 = new LeftHandler();
            Handler h2 = new RightHandler();
            Handler h3 = new MHandler3();
            h1.SetNext(h2);
            h2.SetNext(h3);
            foreach (char instruction in instructions)
            {
                var tupple = h1.HandleRequest(instruction, lastposition, x, y);
                lastposition = tupple.Item1;
                x = tupple.Item2;
                y = tupple.Item3;

            }

            return x.ToString() + y + lastposition;

        }

        abstract class Handler
        {
            protected Handler next;
            public void SetNext(Handler next)
            {
                this.next = next;
            }
            public abstract Tuple<string, int, int> HandleRequest(char instruction, string position, int x, int y);
        }
        class LeftHandler : Handler
        {
            public override Tuple<string, int, int> HandleRequest(char instruction, string lastposition, int x, int y)
            {
                if (instruction == 'L')
                {
                    if (lastposition == "N")
                        lastposition = "W";
                    else if (lastposition == "E")
                        lastposition = "N";
                    else if (lastposition == "W")
                        lastposition = "S";
                    else if (lastposition == "S")
                        lastposition = "E";

                }
                else if (next != null)
                {
                    return next.HandleRequest(instruction, lastposition, x, y);

                }
                return new Tuple<string, int, int>(lastposition, x, y);

            }
        }

        class RightHandler : Handler
        {
            public override Tuple<string, int, int> HandleRequest(char instruction, string lastposition, int x, int y)
            {
                if (instruction == 'R')
                {
                    if (lastposition == "N")
                        lastposition = "E";
                    else if (lastposition == "E")
                        lastposition = "S";
                    else if (lastposition == "W")
                        lastposition = "N";
                    else if (lastposition == "S")
                        lastposition = "W";

                }
                else if (next != null)
                {
                    return next.HandleRequest(instruction, lastposition, x, y);
                }
                return new Tuple<string, int, int>(lastposition, x, y);
            }
        }
        class MHandler3 : Handler
        {
            public override Tuple<string, int, int> HandleRequest(char instruction, string lastposition, int x, int y)
            {
                if (instruction == 'M')
                {
                    if (lastposition == "N")
                        y++;
                    else if (lastposition == "S")
                        y--;
                    else if (lastposition == "E")
                        x++;
                    else if (lastposition == "W")
                        x--;

                }

                return new Tuple<string, int, int>(lastposition, x, y);

            }
        }
    }
}
