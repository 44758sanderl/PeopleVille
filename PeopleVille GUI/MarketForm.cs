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
    public partial class MarketForm : Form
    {
        private readonly TableLayoutPanel _marketGrid;
        private readonly Market _location;
        public MarketForm(Market location)
        {
            _location = location;
            Text = _location.Name;
            Width = 1200;
            Height = 1200;

            _marketGrid = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 1,
                ColumnCount = 2,
                BackColor = Color.LightBlue
                
            };
            _marketGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70f));
            _marketGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30f));
            _marketGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));

            FlowLayoutPanel _marketVillagersPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                Width = 900,
                BackColor = Color.White

            };

            Panel _marketActionPanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = Color.Gray,
                Padding = new Padding(5)
            };

            Button tradeButton = new Button()
            {
                Text = "Trade",
                Height = 50,
                Dock = DockStyle.Top,
            };
            tradeButton.Width = 200;

            _marketActionPanel.Controls.Add(tradeButton);

            _marketGrid.Controls.Add(_marketVillagersPanel, 0, 0);
            _marketGrid.Controls.Add(_marketActionPanel, 1, 0);

            Controls.Add(_marketGrid);

            foreach (BaseVillager villager in _location.Villagers())
            {
                this.Controls.Add(new VillagerComponent(villager));
            }
        }
    }
}
