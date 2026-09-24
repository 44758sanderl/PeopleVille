using PeopleVille_GUI.Components;
using PeopleVilleEngine;
using PeopleVilleEngine.Locations;

namespace PeopleVille_GUI
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            var village = new Village();

            Application.Run(new LocationsForm(village, 
            (location)=> { 
                switch (location)
                {
                    case SimpleHouse:
                        SimpleHouseForm newSimpleHouseForm = new SimpleHouseForm((SimpleHouse)location);
                        newSimpleHouseForm.Show();
                        break;
                    case Market:
                        MarketForm newMarketForm = new MarketForm((Market)location);
                        newMarketForm.Show();
                        break;
                }
            },

            (locationComponent, location)=>
            {
                switch (location)
                {
                    case SimpleHouse:
                        locationComponent.Picture.Image = Image.FromFile("Assets/House.jpg");
                        break;
                    case Market:
                        locationComponent.Picture.Image = Image.FromFile("Assets/Market.jpg");
                        break;
                }
            }
            ));
        }
    }
}