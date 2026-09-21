using PeopleVilleEngine;

namespace PeopleVille_GUI
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            var village = new Village();

            Application.Run(new PeopleVilleForm(village));

        }
    }
}