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
            var itemCount = 0;
            var toDoChoice = 0;
            var projects = new List<Project>();
            int toDoId = 0;
            int projectId = 0;

            do
            {
                Console.WriteLine("Choose a menu option:");
                Console.WriteLine("1. Create a project.");
                Console.WriteLine("2. List all projects.");
                Console.WriteLine("3. List all ToDos in a Project");
                Console.WriteLine("4. Create a ToDo");
                Console.WriteLine("5. List all ToDos");
                Console.WriteLine("6. List all outstanding ToDos");
                Console.WriteLine("7. Delete a ToDo");
                Console.WriteLine("8. Update a ToDo");
                Console.WriteLine("9. Exit");

                var choice = Console.ReadLine() ?? "9";

                if (int.TryParse(choice, out choiceInt))
                {
                    switch (choiceInt)
                    {
                        case 1:
                            // Create Project
                            Console.Write("Project Name: ");
                            string projName = Console.ReadLine();
                            Console.Write("Project Description: ");
                            string projDesc = Console.ReadLine();
                            projects.Add(new Project
                            {
                                Id = ++projectId,
                                Name = projName,
                                Description = projDesc
                            });
                            break;
                        case 2:
                            // List all Projects
                            foreach (var p in projects)
                                Console.WriteLine($"{p.Id}: {p.Name} - {p.Description}");
                            break;
                        case 3:
                            // List ToDos in a given Project
                            Console.Write("Enter Project ID: ");
                            int projectToShow = int.Parse(Console.ReadLine() ?? "0");

                            var todosInProject = toDos.Where(t => t.ProjectId == projectToShow);
                            foreach (var t in todosInProject)
                                Console.WriteLine($"{t.Id}: {t.Name} - {t.Description} (Priority {t.Priority})");
                            break;
                        case 4:
                            // Create ToDo
                            Console.Write("ToDo Name: ");
                            string name = Console.ReadLine();
                            Console.Write("Description: ");
                            string desc = Console.ReadLine();
                            Console.Write("Priority (1-5): ");
                            int priority = int.Parse(Console.ReadLine() ?? "3");

                            Console.WriteLine("Available Projects:");
                            foreach (var p in projects)
                                Console.WriteLine($"{p.Id}: {p.Name}");

                            Console.Write("Enter Project ID for this ToDo: ");
                            int projId = int.Parse(Console.ReadLine() ?? "0");

                            toDos.Add(new ToDo
                            {
                                Id = ++toDoId,
                                Name = name,
                                Description = desc,
                                Priority = priority,
                                IsCompleted = false,
                                ProjectId = projId
                            });
                            break;
                        case 5:
                            toDos.ForEach(Console.WriteLine);
                            break;
                        case 6:
                            toDos.Where(t => (t != null) && !(t?.IsCompleted ?? false))
                                .ToList()
                                .ForEach(Console.WriteLine);
                            break;
                        case 7:
                            toDos.ForEach(Console.WriteLine);
                            Console.Write("ToDo to Delete: ");
                            toDoChoice = int.Parse(Console.ReadLine() ?? "0");

                            var reference = toDos.FirstOrDefault(t => t.Id == toDoChoice);
                            if (reference != null)
                             {
                                toDos.Remove(reference);
                             }

                                break;
                        case 8:
                            toDos.ForEach(Console.WriteLine);
                            Console.Write("ToDo to Update: ");
                            toDoChoice = int.Parse(Console.ReadLine() ?? "0");
                            var updateReference = toDos.FirstOrDefault(t => t.Id == toDoChoice);
                            
                            if(updateReference != null)
                            {
                                Console.Write("Name:");
                                updateReference.Name = Console.ReadLine();
                                Console.Write("Description:");
                                updateReference.Description = Console.ReadLine();
                            }
                            break;
                        case 9:
                            break;
                        default:
                            Console.WriteLine("ERROR: Unknown menu selection");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine($"ERROR: {choice} is not a valid menu selection");
                }

            } while (choiceInt != 9);

        }
    }
}