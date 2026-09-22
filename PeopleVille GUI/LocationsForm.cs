using Microsoft.VisualBasic.Logging;
using PeopleVille_GUI.Components;
using PeopleVilleEngine;
using PeopleVilleEngine.Locations;

namespace PeopleVille_GUI
{
    public class LocationsForm : Form
    {
        private readonly Village _village;
        private readonly FlowLayoutPanel _locationPanel;
        public event Action NewVillager;
        private readonly EventManager _eventManager;
        private readonly Action<ILocation> _cardClick;
        private readonly Action<LocationComponent, ILocation> _cardOnInitilize;


        public LocationsForm(Village village, Action<ILocation> cardClickFunction, Action<LocationComponent, ILocation> onCardInitilize)
        {
            _eventManager = EventManager.GetEventManager();
            _village = village;
            _cardClick = cardClickFunction;
            _cardOnInitilize = onCardInitilize;
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

            _eventManager.VillagerMovedAction += () =>
            {
                LoadLocations();
            };


            CreateLocationControls();
        }

        private void LoadLocations()
        {
            _locationPanel.Controls.Clear();
            foreach (ILocation location in _village.Locations)
            {
                Panel panel;
                panel = new LocationComponent(this ,location, _cardClick,  _cardOnInitilize);
                _locationPanel.Controls.Add(panel);
            }
        }

        private void CreateLocationControls()
        {
            LoadLocations();
        }
    }
}
