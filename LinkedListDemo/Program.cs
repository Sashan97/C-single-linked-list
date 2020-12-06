using System;

namespace LinkedListDemo
{
    class Program
    {
        private static string filepath = @"C:\Users\sasho\Documents\TextTestLocation\file.txt";
        private static LinkedList<Task> tasks = new LinkedList<Task>();

        static void Main()
        {
            Menu();
        }

        private static void Menu()
        {
            bool menuLoop = true;

            while (menuLoop)
            {
                Console.Clear();
                Console.WriteLine("1 - Enter task manager");
                Console.WriteLine("2 - Change file path");
                Console.WriteLine("3 - Exit");

                char key = Console.ReadKey(true).KeyChar;
                if (key == '1') Manager();
                else if (key == '2') ChangeFilePath();
                else if (key == '3') menuLoop = false;
                else continue;
            }
        }
        private static void ChangeFilePath()
        {
            Console.Clear();
            Console.WriteLine("Your current file path:\n" + filepath + "\n");
            Console.WriteLine("Enter new file path:");
            filepath = Console.ReadLine();
        }
        private static void PrintList(LinkedList<Task> list)
        { 
            for (int i = 0; i < list.Length(); i++)
            {
                Console.Write(string.Format("{0,3:D}", i+1));
                Console.Write(string.Format("{0,12:D}", list.ElementAt(i).Title));
                Console.Write(string.Format("{0,35:D}", list.ElementAt(i).Description));
                Console.Write(string.Format("{0,20:D}", list.ElementAt(i).Subject));
                Console.WriteLine(string.Format("{0,19:D}", list.ElementAt(i).GetDate()));
            }
        }
        private static void PrintHeader()
        {
            Console.Write(string.Format("{0,3:D}", "ID"));
            Console.Write(string.Format("{0,12:D}", "TITLE"));
            Console.Write(string.Format("{0,35:D}", "DESCRIPTION"));
            Console.Write(string.Format("{0,20:D}", "SUBJECT"));
            Console.WriteLine(string.Format("{0,19:D}", "DEADLINE"));
        }
        private static void Manager()
        {
            bool managerLoop = true;

            while (managerLoop)
            {
                Console.Clear();
                Console.WriteLine("A - Add | L - Load | S - Save | C - Change | F - Find | R - Remove | X - Clear | Q - Quit");
                Console.WriteLine("_________________________________________________________________________________________\n");
                PrintHeader();
                PrintList(tasks);
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.A) AddTask();
                else if (key.Key == ConsoleKey.L) LoadFile();
                else if (key.Key == ConsoleKey.S) SaveFile();
                else if (key.Key == ConsoleKey.C) ChangeTask();
                else if (key.Key == ConsoleKey.F) FindTask();
                else if (key.Key == ConsoleKey.R) RemoveTask();
                else if (key.Key == ConsoleKey.X) ClearTasks();
                else if (key.Key == ConsoleKey.Q) managerLoop = false;
                else continue;
            }

        }
        private static void AddTask()
        {
            Console.Clear();
            Console.Write("Enter the task title: ");
            string title = Console.ReadLine();

            Console.Write("Enter the task description: ");
            string description = Console.ReadLine();

            Console.Write("Enter subject: ");
            string subject = Console.ReadLine();

            bool loop = true;
            int year = 0;
            while (loop)
            {
                Console.Write("Enter deadline year [2020-2030]: ");
                if(int.TryParse(Console.ReadLine(), out year)){
                    if (year >= 2020 && year <= 2030) loop = false;
                }
            }

            loop = true;
            int month = 0;
            while (loop)
            {
                Console.Write("Enter deadline month [1-12]: ");
                if(int.TryParse(Console.ReadLine(), out month))
                {
                    if (month >= 1 && month <= 12) loop = false;
                }
            }

            loop = true;
            int day = 0;
            while (loop)
            {
                Console.Write("Enter deadline day [1-31]: ");
                if (int.TryParse(Console.ReadLine(), out day))
                {
                    if (day >= 1 && day <= 31) loop = false;
                }
            }

            Task task = new Task(title, description, subject, year, month, day);

            loop = true;

            while (loop)
            {
                Console.Clear();
                PrintHeader();
                Console.Write(string.Format("{0,3:D}", "-"));
                Console.Write(string.Format("{0,12:D}", task.Title));
                Console.Write(string.Format("{0,35:D}", task.Description));
                Console.Write(string.Format("{0,20:D}", task.Subject));
                Console.WriteLine(string.Format("{0,19:D}", task.GetDate()));

                Console.WriteLine("\n1 - Add to start");
                Console.WriteLine("2 - Add to end");
                Console.WriteLine("3 - Add on position");
                Console.WriteLine("4 - Discard");


                char key = Console.ReadKey().KeyChar;
                if (key == '1')
                {
                    tasks.AddToStart(task);
                    return;
                }
                else if (key == '2') 
                {
                    tasks.AddToEnd(task);
                    return;
                }
                else if (key == '3')
                {
                    if (tasks.Length() >= 2)
                    {
                        int position;
                        bool indexLoop = true;
                        while (indexLoop)
                        {
                            Console.Write("\nEnter position [2-" + tasks.Length() + "]: ");
                            if (int.TryParse(Console.ReadLine(), out position))
                            {
                                if (position >= 2 && position <= tasks.Length())
                                {
                                    tasks.AddOnPosition(position-1, task);
                                    return;
                                }
                            }
                        }
                    }
                }
                else if (key == '4') return;
                else continue;
            }
        }
        private static void LoadFile()
        {
            Console.Clear();
            Console.WriteLine("Your current file path: " + filepath);
            Console.WriteLine("Change the path? [Y/N]");

            bool loop = true;
            while (loop)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Y)
                {
                    ChangeFilePath();
                    loop = false;
                }
                else if (key.Key == ConsoleKey.N) loop = false;
                else continue;
            }

            if (!System.IO.File.Exists(filepath))
            {
                Console.WriteLine("File does not exist. Press any key to return.");
                Console.ReadKey(true);
                return;
            }

            System.IO.StreamReader reader = new System.IO.StreamReader(filepath);

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] parts = line.Split(',');

                int year, month, day;

                if(!int.TryParse(parts[3], out year) || !int.TryParse(parts[4], out month) || !int.TryParse(parts[5], out day))
                {
                    Console.WriteLine("Error reading file. Press any key to return.");
                    Console.ReadKey(true);
                    return;
                }

                Task task = new Task(parts[0], parts[1], parts[2], year, month, day);
                tasks.AddToEnd(task);
            }

            reader.Close();
        }
        private static void SaveFile()
        {
            Console.Clear();
            Console.WriteLine("Your current file path: " + filepath);
            Console.WriteLine("Change the path? [Y/N]");

            bool loop = true;
            while (loop)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Y)
                {
                    ChangeFilePath();
                    loop = false;
                }
                else if (key.Key == ConsoleKey.N) loop = false;
                else continue;
            }

            bool append = false;

            if (System.IO.File.Exists(filepath))
            {
                Console.WriteLine("Append to file? [Y/N]");
                loop = true;
                while (loop)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Y)
                    {
                        append = true;
                        loop = false;
                    }
                    else if (key.Key == ConsoleKey.N) loop = false;
                    else continue;
                }
            }

            System.IO.StreamWriter writer = new System.IO.StreamWriter(filepath, append);

            for(int i = 0; i < tasks.Length(); i++)
            {
                string line = string.Format("{0},{1},{2},{3},{4},{5}",
                    tasks.ElementAt(i).Title,
                    tasks.ElementAt(i).Description, 
                    tasks.ElementAt(i).Subject, 
                    tasks.ElementAt(i).DeadlineYear, 
                    tasks.ElementAt(i).DeadlineMonth, 
                    tasks.ElementAt(i).DeadlineDay);

                writer.WriteLine(line);
            }
            writer.Close();
        }
        private static void ChangeTask()
        {
            int number = 0;
            bool loop = true;
            while (loop)
            {
                Console.WriteLine("\nTask to change: ");
                if (int.TryParse(Console.ReadLine(), out number))
                {
                    if (number > 0 && number <= tasks.Length()) loop = false;
                }
            }

            Console.Clear();
            PrintHeader();
            Console.Write(string.Format("{0,3:D}", number));
            Console.Write(string.Format("{0,12:D}", tasks.ElementAt(number - 1).Title));
            Console.Write(string.Format("{0,35:D}", tasks.ElementAt(number - 1).Description));
            Console.Write(string.Format("{0,20:D}", tasks.ElementAt(number - 1).Subject));
            Console.WriteLine(string.Format("{0,19:D}", tasks.ElementAt(number - 1).GetDate()));

            Console.WriteLine("\n1 - Change title");
            Console.WriteLine("2 - Change decription");
            Console.WriteLine("3 - Change subject");
            Console.WriteLine("4 - Change date");
            Console.WriteLine("5 - Return");

            loop = true;
            while (loop)
            {
                char key = Console.ReadKey(true).KeyChar;
                if (key == '1')
                {
                    Console.WriteLine("Enter new title: ");
                    string newTitle = Console.ReadLine();
                    tasks.ElementAt(number - 1).Title = newTitle;
                    loop = false;
                }
                else if (key == '2')
                {
                    Console.WriteLine("Enter new description: ");
                    string newDescription = Console.ReadLine();
                    tasks.ElementAt(number - 1).Description = newDescription;
                    loop = false;
                }
                else if (key == '3')
                {
                    Console.WriteLine("Enter new subject: ");
                    string newSubject = Console.ReadLine();
                    tasks.ElementAt(number - 1).Subject = newSubject;
                    loop = false;
                }
                else if (key == '4')
                {
                    bool dateLoop = true;
                    int newYear = 0;
                    while (dateLoop)
                    {
                        Console.Write("Enter deadline year [2020-2030]: ");
                        if (int.TryParse(Console.ReadLine(), out newYear))
                        {
                            if (newYear >= 2020 && newYear <= 2030) dateLoop = false;
                        }
                    }

                    dateLoop = true;
                    int newMonth = 0;
                    while (dateLoop)
                    {
                        Console.Write("Enter deadline month [1-12]: ");
                        if (int.TryParse(Console.ReadLine(), out newMonth))
                        {
                            if (newMonth >= 1 && newMonth <= 12) dateLoop = false;
                        }
                    }

                    dateLoop = true;
                    int newDay = 0;
                    while (dateLoop)
                    {
                        Console.Write("Enter deadline day [1-31]: ");
                        if (int.TryParse(Console.ReadLine(), out newDay))
                        {
                            if (newDay >= 1 && newDay <= 31) dateLoop = false;
                        }
                    }

                    tasks.ElementAt(number - 1).DeadlineYear = newYear;
                    tasks.ElementAt(number - 1).DeadlineMonth = newMonth;
                    tasks.ElementAt(number - 1).DeadlineDay = newDay;

                    loop = false;
                }
                else if (key == '5') loop = false;
                else continue;
            }
        }
        private static void FindTask()
        {
            Console.WriteLine("\nFind by:");
            Console.WriteLine("1 - Subject");
            Console.WriteLine("2 - Deadline");
            Console.WriteLine("3 - Cancel");

            bool loop = true;
            while (loop)
            {
                char key = Console.ReadKey(true).KeyChar;
                if (key == '1')
                {
                    FindBySubject();
                    loop = false;
                }
                else if (key == '2')
                {
                    FindByDeadline();
                    loop = false;
                }
                else if (key == '3') return;
                else continue;
            }
        }
        private static void FindBySubject()
        {
            Console.Clear();
            Console.Write("Enter subject: ");
            string query = Console.ReadLine();

            LinkedList<Task> results = new LinkedList<Task>();
            int resultCount = 0;

            for(int i = 0; i<tasks.Length(); i++)
            {
                if(tasks.ElementAt(i).Subject == query)
                {
                    results.AddToEnd(tasks.ElementAt(i));
                    resultCount++;
                }
            }

            if(resultCount == 0)
            {
                Console.WriteLine("No entries found. Press any key to return.");
            }
            else
            {
                Console.WriteLine("\nShowing search results for: " + query + ". Press any key to return.");
                PrintHeader();
                PrintList(results);
            }
            Console.ReadKey(true);
        }
        private static void FindByDeadline()
        {
            Console.Clear();
            bool loop = true;
            int year = 0;
            while (loop)
            {
                Console.Write("Enter deadline year [2020-2030]: ");
                if (int.TryParse(Console.ReadLine(), out year))
                {
                    if (year >= 2020 && year <= 2030) loop = false;
                }
            }

            loop = true;
            int month = 0;
            while (loop)
            {
                Console.Write("Enter deadline month [1-12]: ");
                if (int.TryParse(Console.ReadLine(), out month))
                {
                    if (month >= 1 && month <= 12) loop = false;
                }
            }

            loop = true;
            int day = 0;
            while (loop)
            {
                Console.Write("Enter deadline day [1-31]: ");
                if (int.TryParse(Console.ReadLine(), out day))
                {
                    if (day >= 1 && day <= 31) loop = false;
                }
            }

            LinkedList<Task> results = new LinkedList<Task>();
            int resultCount = 0;

            for (int i = 0; i < tasks.Length(); i++)
            {
                if (tasks.ElementAt(i).DeadlineYear == year &&
                    tasks.ElementAt(i).DeadlineMonth == month &&
                    tasks.ElementAt(i).DeadlineDay == day)
                {
                    results.AddToEnd(tasks.ElementAt(i));
                    resultCount++;
                }
            }

            if (resultCount == 0)
            {
                Console.WriteLine("No entries found. Press any key to return.");
            }
            else
            {
                Console.WriteLine(String.Format("\nShowing search results for {0}-{1}-{2}. Press any key to return.", year, month, day));
                PrintHeader();
                PrintList(results);
            }
            Console.ReadKey(true);
        }
        private static void RemoveTask()
        {
            if (!tasks.IsEmpty())
            {
                bool loop = true;
                while (loop)
                {
                    Console.WriteLine("\nTask to remove: ");
                    if (int.TryParse(Console.ReadLine(), out int number))
                    {
                        if (number > 0 && number <= tasks.Length())
                        {
                            tasks.RemoveAt(number - 1);
                            loop = false;
                        }
                    }
                }
            } 
        }
        private static void ClearTasks()
        {
            tasks.Clear();
        }

    }
}
