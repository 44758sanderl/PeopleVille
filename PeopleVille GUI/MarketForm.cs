using PeopleVille_GUI.Components;
using PeopleVilleEngine;
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
    public class MarketForm : Form
    {
        private readonly Market _location;
        private readonly FlowLayoutPanel _marketVillagersPanel;
        private void ShowVillagersForm(BaseVillager initialVillager)
        {
            Form form = new Form()
            {
                Height = 500,
                Width = 500
            };

            FlowLayoutPanel _flowLayoutPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                Width = 900,
                BackColor = Color.White

            };
            form.Controls.Add(_flowLayoutPanel);

            foreach (BaseVillager villager in _location.Villagers())
            {
                if (villager == initialVillager)
                {
                    continue;
                }
                _flowLayoutPanel.Controls.Add(new VillagerComponent(villager, 
                    ()=> {
                        new TradeForm(initialVillager, villager).Show();
                    }
                ));
            }

            if (_flowLayoutPanel.Controls.Count == 0)
            {
                MessageBox.Show("No villagers avilable to trade with!");
                return;
            }
            form.Show();
        }

        private void LoadVillagers()
        {
            foreach (BaseVillager villager in _location.Villagers())
            {
                _marketVillagersPanel.Controls.Add(new VillagerComponent(villager,
                    () => {
                        VillagerActionMenu window = new VillagerActionMenu(villager);
                        
                        Button tradeButton = new Button()
                        {
                            Text = "Trade Button",
                            Height = 50,
                            Dock = DockStyle.Top,
                            TextAlign = ContentAlignment.MiddleCenter,
                            BackColor = Color.DarkGray,
                            Margin = new Padding(0, 0, 0, 15)
                        };

                        tradeButton.Click += (e, sender) =>
                        {
                            ShowVillagersForm(villager);
                        };

                        window.mainPanel.Controls.Add(tradeButton);
                        window.mainPanel.Controls.SetChildIndex(tradeButton, 0);
                        window.Show();
                    }

                ));
            }
        }
        public MarketForm(Market location)
        {
            _location = location;
            Text = _location.Name;
            Width = 1200;
            Height = 1200;
            
            _marketVillagersPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                Width = 900,
                BackColor = Color.White

            };

            Controls.Add(_marketVillagersPanel);
            LoadVillagers();
        }
    }
}
