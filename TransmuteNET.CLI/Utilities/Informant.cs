namespace TransmuteNET.CLI.Utilities
{
    internal static class Informant
    {
        public static void Info(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{DateTime.Now:dd.MM.yyyy HH:mm:ss}\t{message}");
            Console.ResetColor();
        }

        public static void Trace(string message)
        {
            Console.WriteLine($"{DateTime.Now:dd.MM.yyyy HH:mm:ss}\t{message}");
        }

        public static void Success(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{DateTime.Now:dd.MM.yyyy HH:mm:ss}\t{message}");
            Console.ResetColor();
        }

        public static void Warning(Exception ex)
        {
            Warning(ex.Message);

            if (ex.InnerException is not null)
            {
                Info("Inner Exception:\n\n");
                Warning(ex.InnerException.Message);
            }
        }

        public static void Warning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{DateTime.Now:dd.MM.yyyy HH:mm:ss}\t{message}");
            Console.ResetColor();
        }

        public static void Error(Exception ex)
        {
            Error(ex.Message);

            if (ex.InnerException is not null)
            {
                Info("Inner Exception:\n\n");
                Error(ex.InnerException.Message);
            }
        }

        public static void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{DateTime.Now:dd.MM.yyyy HH:mm:ss}\t{message}");
            Console.ResetColor();
        }
    }
}