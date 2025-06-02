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
                Console.WriteLine("3. List all ToDos in a project.");
                Console.WriteLine("4. Delete a project.");
                Console.WriteLine("5. Update a project.");
                Console.WriteLine("6. Create a ToDo.");
                Console.WriteLine("7. List all ToDos.");
                Console.WriteLine("8. List all outstanding ToDos.");
                Console.WriteLine("9. Delete a ToDo.");
                Console.WriteLine("10. Update a ToDo.");
                Console.WriteLine("11. Exit.");

                var choice = Console.ReadLine() ?? "11";

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
                            foreach (var p in projects)
                            {
                                // Recalculate CompletePercent
                                var todosInProject2 = toDos.Where(t => t.ProjectId == p.Id).ToList();

                                if (todosInProject2.Count == 0)
                                {
                                    p.CompletePercent = 0;
                                }
                                else
                                {
                                    int completedCount = todosInProject2.Count(t => t.IsCompleted == true);
                                    p.CompletePercent = (completedCount * 100.0) / todosInProject2.Count;
                                }

                                // Display project with completion %
                                Console.WriteLine($"[{p.Id}] {p.Name} - {p.Description} | Complete: {p.CompletePercent:F1}%");
                            }
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
                            Console.WriteLine("Projects:");
                            foreach (var p in projects)
                                Console.WriteLine($"[{p.Id}] {p.Name}");

                            Console.Write("Enter Project ID to delete: ");
                            int delProjId = int.Parse(Console.ReadLine() ?? "0");

                            var projectToDelete = projects.FirstOrDefault(p => p.Id == delProjId);

                            if (projectToDelete != null)
                            {
                                // Unassign all ToDos that belong to this project
                                foreach (var todo in toDos.Where(t => t.ProjectId == delProjId))
                                    todo.ProjectId = 0; // or -1 to signify "Unassigned"

                                projects.Remove(projectToDelete);
                                Console.WriteLine("Project deleted. All ToDos have been unassigned.");
                            }
                            else
                            {
                                Console.WriteLine("Project not found.");
                            }
                            break;
                        case 5:
                             // Update a Project
                            Console.WriteLine("Projects:");
                            foreach (var p in projects)
                                Console.WriteLine($"[{p.Id}] {p.Name}");

                            Console.Write("Enter Project ID to update: ");
                            int projToUpdate = int.Parse(Console.ReadLine() ?? "0");
                            var projRef = projects.FirstOrDefault(p => p.Id == projToUpdate);

                            if (projRef != null)
                            {
                                Console.Write("New Name: ");
                                projRef.Name = Console.ReadLine();
                                Console.Write("New Description: ");
                                projRef.Description = Console.ReadLine();
                                Console.WriteLine("Project updated.");
                            }
                            else
                            {
                                Console.WriteLine("Project not found.");
                            }
                            break;

                        case 6:
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
                        case 7:
                            toDos.ForEach(Console.WriteLine);
                            break;
                        case 8:
                            toDos.Where(t => (t != null) && !(t?.IsCompleted ?? false))
                                .ToList()
                                .ForEach(Console.WriteLine);
                            break;
                        case 9:
                            toDos.ForEach(Console.WriteLine);
                            Console.Write("ToDo to Delete: ");
                            toDoChoice = int.Parse(Console.ReadLine() ?? "0");

                            var reference = toDos.FirstOrDefault(t => t.Id == toDoChoice);
                            if (reference != null)
                             {
                                toDos.Remove(reference);
                             }

                                break;
                        case 10:
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
                                Console.Write("Priority (1-5): ");
                                if (int.TryParse(Console.ReadLine(), out int parsedPriority))
                                    updateReference.Priority = parsedPriority;
                                else
                                    Console.WriteLine("Invalid priority. Keeping existing value.");

                                Console.Write("Mark as complete? (y/n): ");
                                string? input = Console.ReadLine()?.Trim().ToLower();

                                if (input == "y")
                                    updateReference.IsCompleted = true;
                                else if (input == "n")
                                    updateReference.IsCompleted = false;
                                else
                                    Console.WriteLine("Invalid input. Leaving IsCompleted unchanged.");

                            }
                            break;
                        case 11:
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

            } while (choiceInt != 11);

        }
    }
}