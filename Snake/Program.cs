namespace Snake
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            // Erstellen einer Instanz von Sound_Window
            Sound_Window soundWindow = new Sound_Window();

            // Starten der Anwendung mit Main_Window
            Application.Run(new Main_Window());
        }
    }
}