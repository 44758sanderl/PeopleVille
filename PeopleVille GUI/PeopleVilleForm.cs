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

        public PeopleVilleForm(Village village)
        {
            _village = village;

            // Configure the window
            Text = "PeopleVille";
            Width = 1200;
            Height = 1200;

            // Create the container that will hold our locations
            _locationPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true
            };

            // Put the FlowLayoutPanel inside the Form
            Controls.Add(_locationPanel);

            // Create one box for every location
            CreateLocationControls();
        }

        private void CreateLocationControls()
        {
            foreach (var location in _village.Locations)
            {
                var locationBox = new Panel
                {
                    Width = 200,
                    Height = 300,
                    BorderStyle = BorderStyle.FixedSingle
                };

                var locationName = new Label
                {
                    Text = location.Name,
                    Location = new Point(0, 0),
                    Height = 100,
                    Width = 200,
                    TextAlign = ContentAlignment.MiddleCenter
                };

                var image = new PictureBox
                {
                    Width = 200,
                    Height = 200,
                    Location = new Point(0, 100),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Image = Image.FromFile("Assets/House.jpg"),
                    Enabled = false
                };



                locationBox.Click += (sender, e) =>
                {
                    switch (location)
                    {
                        case SimpleHouse house:
                            OpenSimpleHouse(house);
                            break;
                        default:
                            MessageBox.Show("Unknown location");
                            break;
                    }   

                    
                };

                locationBox.Controls.Add(locationName);
                locationBox.Controls.Add(image);
                

                _locationPanel.Controls.Add(locationBox);
            }
        }
    }
}
