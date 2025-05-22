using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Gespraechsnotiz_App.Views
{
    public partial class Divider : ContentView
    {
        public Divider()
        {
            var divider = new BoxView
            {
                HeightRequest = UIStyles.DIVIDER_HEIGHT,
                Color = UIStyles.BackgroundDarkGrey,
                VerticalOptions = LayoutOptions.Fill,
            };

            Content = divider;
        }
    }
}
