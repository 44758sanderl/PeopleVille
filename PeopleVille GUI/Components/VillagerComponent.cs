using System;
using System.Collections.Generic;
using System.Text;

namespace PeopleVille_GUI.Components
{
    public class VillagerComponent : Panel
    {
        private readonly BaseVillager _villager;
        private readonly Action _onClick;
        public VillagerComponent(BaseVillager villager, Action OnClick) {

            _villager = villager;
            _onClick = OnClick;
            this.Size = new Size(200, 300);
            this.BorderStyle = BorderStyle.FixedSingle;

            Label label = new Label()
            {
                Text = villager.FirstName,
                Location = new Point(0, 0),
                Height = 100,
                Width = 200,
                TextAlign = ContentAlignment.MiddleCenter
            };

            PictureBox pictureBox = new PictureBox()
            {
                Width = 200,
                Height = 200,
                Location = new Point(0, 100),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = Image.FromFile("Assets/Villager.jpg"),
                Enabled = false
            };

            this.Controls.Add(label);
            this.Controls.Add(pictureBox);

            this.Click += (e, sender) => { OnClick(); };

        }

        private void ShowVillagerActionMenu() { 
            var window = new VillagerActionMenu(_villager);
            window.Show();
        }
    }
}
