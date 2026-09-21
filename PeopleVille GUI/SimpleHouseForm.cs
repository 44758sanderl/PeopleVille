using PeopleVilleEngine.Locations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PeopleVille_GUI
{
    public partial class SimpleHouseForm : Form
    {
        private readonly FlowLayoutPanel _simpleHousePanel;
        private readonly SimpleHouse _location;
        public SimpleHouseForm(SimpleHouse location)
        {
            _location = location;
            Text = _location.Name;
            Width = 1200;
            Height = 1200;

            _simpleHousePanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true
            };

            Controls.Add(_simpleHousePanel);

            foreach (BaseVillager villager in _location.Villagers())
            {
                var simpleHouseBox = new Panel
                {
                    Width = 200,
                    Height = 300,
                    BorderStyle = BorderStyle.FixedSingle
                };

                var simpleHouseName = new Label
                {
                    Text = villager.FirstName,
                    Location = new Point(0, 0),
                    Height = 100,
                    Width = 200,
                    TextAlign = ContentAlignment.MiddleCenter
                };

                var simpleHouseImage = new PictureBox
                {
                    Width = 200,
                    Height = 200,
                    Location = new Point(0, 100),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Image = Image.FromFile("Assets/Villager.jpg"),
                    Enabled = false
                };

                simpleHouseBox.Controls.Add(simpleHouseName);
                simpleHouseBox.Controls.Add(simpleHouseImage);

                _simpleHousePanel.Controls.Add(simpleHouseBox);
            }
        }
    }
}
