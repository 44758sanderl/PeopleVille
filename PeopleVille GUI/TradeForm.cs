using PeopleVilleEngine.Items;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PeopleVille_GUI
{
    internal class TradeForm : Form
    {
        private readonly BaseVillager _initialVillager;
        private readonly BaseVillager _requestedVillager;

        private readonly List<BaseItem> _initialTradeItems = new();
        private readonly List<BaseItem> _requestedTradeItems = new();

        private FlowLayoutPanel _initialInventoryPanel;
        private FlowLayoutPanel _initialTradePanel;

        private FlowLayoutPanel _requestedInventoryPanel;
        private FlowLayoutPanel _requestedTradePanel;

        public TradeForm(BaseVillager initialVillager, BaseVillager requestedVillager)
        {
            _initialVillager = initialVillager;
            _requestedVillager = requestedVillager;

            Height = 700;
            Width = 1100;
            Text = "Trade";
            StartPosition = FormStartPosition.CenterParent;

            TableLayoutPanel mainLayout = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                RowCount = 1,
                ColumnCount = 3,
                Padding = new Padding(10)
            };

            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42F));
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16F));
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42F));

            Panel initialPanel = CreateVillagerPanel(
                _initialVillager,
                out _initialInventoryPanel,
                out _initialTradePanel
            );

            Panel requestedPanel = CreateVillagerPanel(
                _requestedVillager,
                out _requestedInventoryPanel,
                out _requestedTradePanel
            );

            Panel actionPanel = new Panel()
            {
                Dock = DockStyle.Fill
            };

            Button tradeButton = new Button()
            {
                Text = "Trade",
                Dock = DockStyle.Bottom,
                Height = 60
            };

            tradeButton.Click += (sender, e) =>
            {
                ExecuteTrade();
            };

            actionPanel.Controls.Add(tradeButton);

            mainLayout.Controls.Add(initialPanel, 0, 0);
            mainLayout.Controls.Add(actionPanel, 1, 0);
            mainLayout.Controls.Add(requestedPanel, 2, 0);

            Controls.Add(mainLayout);

            LoadInventory(
                _initialVillager,
                _initialInventoryPanel,
                _initialTradeItems,
                _initialTradePanel
            );

            LoadInventory(
                _requestedVillager,
                _requestedInventoryPanel,
                _requestedTradeItems,
                _requestedTradePanel
            );

            LoadTradeItems(_initialTradePanel, _initialTradeItems);
            LoadTradeItems(_requestedTradePanel, _requestedTradeItems);
        }

        private Panel CreateVillagerPanel(
            BaseVillager villager,
            out FlowLayoutPanel inventoryPanel,
            out FlowLayoutPanel tradePanel)
        {
            Panel mainPanel = new Panel()
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(5)
            };

            Label nameLabel = new Label()
            {
                Text = $"{villager.FirstName} {villager.LastName}",
                Dock = DockStyle.Top,
                Height = 40,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 14, FontStyle.Bold)
            };

            PictureBox villagerPicture = new PictureBox()
            {
                Dock = DockStyle.Top,
                Height = 160,
                Width = 160,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = Image.FromFile("Assets/Villager.jpg")
            };

            Label inventoryLabel = new Label()
            {
                Text = "Inventory",
                Dock = DockStyle.Top,
                Height = 30,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            inventoryPanel = new FlowLayoutPanel()
            {
                Dock = DockStyle.Top,
                Height = 130,
                AutoScroll = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };

            Label tradeLabel = new Label()
            {
                Text = "Items selected to trade",
                Dock = DockStyle.Top,
                Height = 30,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            tradePanel = new FlowLayoutPanel()
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.LightBlue
            };

            mainPanel.Controls.Add(tradePanel);
            mainPanel.Controls.Add(tradeLabel);
            mainPanel.Controls.Add(inventoryPanel);
            mainPanel.Controls.Add(inventoryLabel);
            mainPanel.Controls.Add(villagerPicture);
            mainPanel.Controls.Add(nameLabel);

            return mainPanel;
        }

        private void LoadInventory(
            BaseVillager villager,
            FlowLayoutPanel panel,
            List<BaseItem> tradeItems,
            FlowLayoutPanel tradePanel)
        {
            panel.Controls.Clear();

            foreach (BaseItem item in villager.Items)
            {
                Panel itemSlot = CreateItemSlot(item);

                // Show selected state
                if (tradeItems.Contains(item))
                    itemSlot.BackColor = Color.LightGreen;
                else
                    itemSlot.BackColor = Color.White;

                // Toggle selection when clicking the slot or its label
                itemSlot.Click += (s, e) =>
                {
                    ToggleSelection(item, villager, panel, tradeItems, tradePanel);
                };

                // The label fills the panel; ensure clicks on it toggle as well
                foreach (Control c in itemSlot.Controls)
                {
                    c.Click += (s, e) =>
                    {
                        ToggleSelection(item, villager, panel, tradeItems, tradePanel);
                    };
                }

                panel.Controls.Add(itemSlot);
            }
        }

        private void ToggleSelection(
            BaseItem item,
            BaseVillager villager,
            FlowLayoutPanel inventoryPanel,
            List<BaseItem> tradeItems,
            FlowLayoutPanel tradePanel)
        {
            if (tradeItems.Contains(item))
            {
                tradeItems.Remove(item);
            }
            else
            {
                tradeItems.Add(item);
            }

            // Refresh both sides: inventory (to update highlight) and trade panel (to show selected items)
            LoadInventory(villager, inventoryPanel, tradeItems, tradePanel);
            LoadTradeItems(tradePanel, tradeItems);
        }

        private void LoadTradeItems(
            FlowLayoutPanel panel,
            List<BaseItem> tradeItems)
        {
            panel.Controls.Clear();

            foreach (BaseItem item in tradeItems)
            {
                Panel itemSlot = CreateItemSlot(item);

                // show selected appearance in trade panel
                itemSlot.BackColor = Color.LightGreen;

                // allow clicking in trade panel to unselect
                itemSlot.Click += (s, e) =>
                {
                    // find which side this trade panel belongs to and remove from appropriate list
                    // the higher-level logic will call LoadInventory after ExecuteTrade or we can remove directly
                    // We'll simply remove the item from whichever list contains it
                    if (_initialTradeItems.Contains(item))
                    {
                        _initialTradeItems.Remove(item);
                        LoadInventory(_initialVillager, _initialInventoryPanel, _initialTradeItems, _initialTradePanel);
                    }
                    else if (_requestedTradeItems.Contains(item))
                    {
                        _requestedTradeItems.Remove(item);
                        LoadInventory(_requestedVillager, _requestedInventoryPanel, _requestedTradeItems, _requestedTradePanel);
                    }

                    LoadTradeItems(_initialTradePanel, _initialTradeItems);
                    LoadTradeItems(_requestedTradePanel, _requestedTradeItems);
                };

                panel.Controls.Add(itemSlot);
            }
        }

        private Panel CreateItemSlot(BaseItem item)
        {
            Panel itemSlot = new Panel()
            {
                Size = new Size(90, 90),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                Margin = new Padding(3),
                Cursor = Cursors.Hand
            };

            Label itemLabel = new Label()
            {
                Text = item.Name,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Enabled = true,
                BackColor = Color.Transparent
            };

            itemSlot.Controls.Add(itemLabel);

            return itemSlot;
        }

        private void InventoryPanel_DragEnter(object sender, DragEventArgs e)
        {
            // Drag & drop removed in this selection-based UI. Keep method to avoid compile errors if referenced elsewhere.
            if (e.Data.GetDataPresent(typeof(BaseItem)))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void ExecuteTrade()
        {
            foreach (BaseItem item in _initialTradeItems)
            {
                _initialVillager.Items.Remove(item);
                _requestedVillager.Items.Add(item);
            }

            foreach (BaseItem item in _requestedTradeItems)
            {
                _requestedVillager.Items.Remove(item);
                _initialVillager.Items.Add(item);
            }

            Close();
        }
    }
}