using Microsoft.VisualBasic.Logging;
using PeopleVille_GUI.Components;
using PeopleVilleEngine;
using PeopleVilleEngine.Locations;

namespace PeopleVille_GUI
{
    public class PeopleVilleForm : Form
    {
        private readonly Village _village;
        private readonly FlowLayoutPanel _locationPanel;

        private void OpenSimpleHouse(SimpleHouse location)
        {
            var window = new SimpleHouseForm(location);
            window.Show();
        }

        private void OpenMarket(Market location)
        {
            var window = new MarketForm(location);
            window.Show();
        }

        public PeopleVilleForm(Village village)
        {
            _village = village;

            Text = "PeopleVille";
            Width = 1200;
            Height = 1200;

            _locationPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true
            };

            Controls.Add(_locationPanel);

            CreateLocationControls();
        }

        private void CreateLocationControls()
        {
            foreach (var location in _village.Locations)
            {
                //var locationBox = new Panel
                //{
                //    Width = 200,
                //    Height = 300,
                //    BorderStyle = BorderStyle.FixedSingle
                //};

                //var locationName = new Label
                //{
                //    Text = location.Name,
                //    Location = new Point(0, 0),
                //    Height = 100,
                //    Width = 200,
                //    TextAlign = ContentAlignment.MiddleCenter
                //};

                //var image = new PictureBox
                //{
                //    Width = 200,
                //    Height = 200,
                //    Location = new Point(0, 100),
                //    SizeMode = PictureBoxSizeMode.StretchImage,
                //    Image = Image.FromFile("Assets/House.jpg"),s
                //    Enabled = false
                //};

                //locationBox.Click += (sender, e) =>
                Panel panel; 
                switch (location)
                {
                    case SimpleHouse house:
                        panel = new LocationComponent(location, "Assets/House.jpg", () => OpenSimpleHouse((SimpleHouse)location));
                        _locationPanel.Controls.Add(panel);
                        break;
                    case Market market:
                        panel = new LocationComponent(location, "Assets/Market.jpg", () => OpenMarket((Market)location));
                        _locationPanel.Controls.Add(panel);
                        break;
                    default:
                        MessageBox.Show("Unknown location");
                        break;
                };  
      

                //locationBox.Controls.Add(locationName);
                //locationBox.Controls.Add(image);
                
                
            }
        }
    }
}
