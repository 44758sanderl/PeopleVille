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

        private readonly Action<ILocation> _cardClick;
        private readonly Action<LocationComponent, ILocation> _cardOnInitilize;
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

        public LocationsForm(Village village, Action<ILocation> cardClickFunction, Action<LocationComponent, ILocation> onCardInitilize)
        {
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

            CreateLocationControls();
        }

        private void CreateLocationControls()
        {
            foreach (ILocation location in _village.Locations)
            {
                Panel panel;
                panel = new LocationComponent(location, _cardClick,  _cardOnInitilize);
                _locationPanel.Controls.Add(panel);
            }
        }
    }
}
