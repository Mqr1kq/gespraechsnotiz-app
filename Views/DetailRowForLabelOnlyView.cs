using CommunityToolkit.Maui.Markup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gespraechsnotiz_App.Views
{
    public partial class DetailRowForLabelOnlyView : ContentView
    {
        public DetailRowForLabelOnlyView(string label, string? labelValue, bool isLabelBold, bool valueAlignedLeft)
        {
            var rowLayoutGrid = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = GridLength.Auto },  //labelObject takes only as much space as needed
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }  //labelValueObject takes the remaining space
                },
            }.Fill();

            var labelObject = new Label
            {
                Text = label,
                MinimumWidthRequest = UIStyles.LABEL_MINIMUM_WIDTH,
                HorizontalOptions = LayoutOptions.Start
            }.Paddings(right: UIStyles.COMMON_PADDINGS_MEDIUM);

            var labelValueObject = new Label
            {
                Text = labelValue,
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Start,
                LineBreakMode = LineBreakMode.WordWrap
            }.Paddings(right: UIStyles.COMMON_PADDINGS_VERY_SMALL);

            if (valueAlignedLeft == true) labelValueObject.HorizontalOptions = LayoutOptions.Start;

            if (isLabelBold == true)
            {
                labelObject.Bold();
                labelValueObject.Bold();
            }

            if (!string.IsNullOrEmpty(labelValue))
            {
                rowLayoutGrid.Add(labelObject, 0, 0);
                rowLayoutGrid.Add(labelValueObject, 1, 0);
            }

            Content = rowLayoutGrid;
        }
    }
}
