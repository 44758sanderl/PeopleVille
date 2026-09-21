using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PeopleVille_GUI
{
    public partial class VillagerActionMenu : Form
    {
        public VillagerActionMenu(BaseVillager villager)
        {
            Size = new Size(300, 400);
            this.StartPosition = FormStartPosition.CenterParent;

            Panel mainPanel = new Panel()
            {
                Padding = new Padding(10),
                Dock = DockStyle.Fill
            };

            Label hungerLabel = new Label()
            {
                Text = $"Sult: {villager.Hunger} / {villager.MaxHunger}", 
                Height = 50,
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Gray,
                Margin = new Padding(0, 0, 0, 10)
            };

            Label ageLabel = new Label()
            {
                Text = $"Alder: {villager.Age}",
                Height = 50,
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Gray,
                Margin = new Padding(0, 0, 0, 10)
            };

            Label nameLabel = new Label()
            {
                Text = villager.FirstName + " " + villager.LastName,
                Height = 50,
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.DarkGray,
                Margin = new Padding(0, 0, 0, 15)
            };

            Button inventarButton = new Button()
            {
                Text = "Open Inventory",
                Height = 50,
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.DarkGray,
                Margin = new Padding(0, 0, 0, 15)
            };

            inventarButton.Click += (sender, e) =>
            {
                VillagerInventory inventory = new VillagerInventory(villager);
                inventory.Show();
            };

            mainPanel.Controls.Add(inventarButton);
            mainPanel.Controls.Add(hungerLabel);
            mainPanel.Controls.Add(ageLabel);
            mainPanel.Controls.Add(nameLabel);
            

            this.Controls.Add(mainPanel);
        }
    }
}
