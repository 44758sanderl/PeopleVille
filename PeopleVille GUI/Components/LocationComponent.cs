using PeopleVilleEngine.Locations;
using System;
using System.Collections.Generic;
using System.Text;

namespace PeopleVille_GUI.Components
{
    public class LocationComponent : Panel
    {
        private readonly ILocation _location;
        public delegate void OnLocationClick();

        public LocationComponent(ILocation location, String path, OnLocationClick onClick)
        {

            _location = location;

            this.Size = new Size(200, 300);
            this.BorderStyle = BorderStyle.FixedSingle;

            Label label = new Label()
            {
                Text = location.Name,
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
                Image = Image.FromFile(path),
                Enabled = false
            };

            this.Controls.Add(label);
            this.Controls.Add(pictureBox);

            this.Click += (sender, e) =>
            {
                onClick();
            };

        }
    }
}
