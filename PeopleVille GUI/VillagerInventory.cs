using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PeopleVille_GUI
{
    public partial class VillagerInventory : Form
    {
        public VillagerInventory(BaseVillager villager)
        {
            Size = new Size(600, 350);
            Text = $"{villager.FirstName}'s Inventory";
            StartPosition = FormStartPosition.CenterParent;

            FlowLayoutPanel inventoryGrid = new FlowLayoutPanel()
            {
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(2),            
                BackColor = Color.White
            };

            foreach (var item in villager.Items)
            {
                Panel itemSlot = new Panel()
                {
                    Size = new Size(80, 80),
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = Color.White,
                    Margin = new Padding(1)
                };

                Label itemLabel = new Label()
                {
                    Text = item.Name,
                    Size = new Size(80, 80),
                    Location = new Point(0, 0),
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 8, FontStyle.Regular),
                    Enabled = false
                };
                itemSlot.Controls.Add(itemLabel);
           
                itemSlot.Click += (sender, e) =>
                {
                    MessageBox.Show($"Du klikkede på: {item.Name}"); 
                };

                inventoryGrid.Controls.Add(itemSlot);
            }

            this.Controls.Add(inventoryGrid);
        }
    }
}