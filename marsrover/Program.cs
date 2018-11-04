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

    }
}
