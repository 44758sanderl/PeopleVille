using PeopleVille_GUI.Components;
using PeopleVilleEngine;
using PeopleVilleEngine.Locations;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PeopleVille_GUI
{
    public partial class SimpleHouseForm : Form
    {
        private readonly FlowLayoutPanel _simpleHousePanel;
        private readonly SimpleHouse _location;
        private readonly EventManager _eventManager;

        public SimpleHouseForm(SimpleHouse location)
        {
            _location = location;
            _eventManager = EventManager.GetEventManager();

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

            LoadVillagerComponents();

            _eventManager.VillagerMovedAction += () =>
            {
                LoadVillagerComponents();
            };
        }

        private void LoadVillagerComponents()
        {
            _simpleHousePanel.Controls.Clear();

            foreach (BaseVillager villager in _location.Villagers())
            {
                VillagerComponent component = new VillagerComponent(villager, () => {
                    var window = new VillagerActionMenu(villager);
                    window.Show();
                });

                _simpleHousePanel.Controls.Add(component);
            }
        }
    }
}
