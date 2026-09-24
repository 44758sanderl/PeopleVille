using PeopleVilleEngine.Locations;
using System;
using System.Collections.Generic;
using System.Text;

namespace PeopleVille_GUI.Components
{
    public class LocationComponent : Panel
    {
        private readonly ILocation _location;

        public PictureBox Picture { get; }
        public Label Title { get; }
        public LocationsForm LocationForm { get; }


        public LocationComponent(
            LocationsForm locationForm,
            ILocation location,
            Action<ILocation> onClick,
            Action<LocationComponent, ILocation> onInitialize)
        {
            _location = location;
            LocationForm = locationForm;
            Size = new Size(200, 300);
            BorderStyle = BorderStyle.FixedSingle;

            Title = new Label()
            {
                Text = location.Name,
                Location = new Point(0, 0),
                Height = 100,
                Width = 200,
                TextAlign = ContentAlignment.MiddleCenter
            };

            Picture = new PictureBox()
            {
                Width = 200,
                Height = 200,
                Location = new Point(0, 100),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = Image.FromFile("Assets/Placeholder.jpg"),
                Enabled = false
            };

            Controls.Add(Title);
            Controls.Add(Picture);

            onInitialize(this, location);

            Click += (sender, e) =>
            {
                onClick(location);
            };
        }
    }
}
