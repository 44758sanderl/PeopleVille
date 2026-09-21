using PeopleVille_GUI.Components;
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

                VillagerComponent component = new VillagerComponent(villager);

                _simpleHousePanel.Controls.Add(component);
            }
        }
    }
}
