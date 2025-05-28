using Asana.Library.Models;
using System;

namespace Asana
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var toDos = new List<ToDo>();
            int choiceInt;
            do
            {
                Console.WriteLine("Choose a menu option:");
                Console.WriteLine("1. Create a Todo");
                Console.WriteLine("2. list all Todos");
                Console.WriteLine("3. list all outstandingTodos");
                Console.WriteLine("4. Exit");

                var choice = Console.ReadLine() ?? "4";

                if (int.TryParse(choice, out choiceInt))
                {

                    switch (choiceInt)
                    {
                        case 1:
                            Console.WriteLine("Name:");
                            var name = Console.ReadLine();
                            Console.WriteLine("Description:");
                            var description = Console.ReadLine();

                            toDos.Add(new ToDo { Name = name, Description = description, IsCompleted = false });
                            break;
                        case 2:
                            toDos.ForEach(Console.WriteLine);
                            break;
                        case 3:
                            toDos.Where(t => (t != null) && !(t?.IsCompleted ?? false))
                                .ToList()
                                .ForEach(Console.WriteLine);
                            break;
                        case 4:
                            break;
                        default:
                            Console.WriteLine("ERROR:Unknown menu selection");
                            break;
                    }
                } else
                {
                    Console.WriteLine($"ERROR: {choice} is invalid menu selection");
                }
            } while (choiceInt != 4);

        }
    }
}