using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Maui.Markup;
using CommunityToolkit.Mvvm.ComponentModel;
using Gespraechsnotiz_App.Models;
using MauiIcons.Core;
using MauiIcons.Material;

namespace Gespraechsnotiz_App.ViewModels
{
    [INotifyPropertyChanged]
    public partial class MainViewModel
    {
        public string Title { get { return "Notizen"; } }
        public string CreateNewNoteLabel { get { return "Neue Notiz"; } }
        public string ImportanceLabel { get { return "Dringlichkeit"; } }
        public string DeleteMessageDescription { get { return "Möchten Sie die Notiz wirklich löschen?"; } }
        public string YesLabel { get { return "Ja"; } }
        public string NoLabel { get { return "Nein"; } }
        public string DeleteLabel { get { return "Löschen"; } }

        public Color PriorityToColor(Importance importance)
        {
            Color priorityColor = importance switch
            {
                Importance.Niedrig => Colors.Green,
                Importance.Mittel => Colors.Orange,
                Importance.Hoch => Colors.Red,
                _ => Colors.Grey,
            };

            return priorityColor;
        }

        public Frame CreateRoundedButtonWithClickEffect(
            MaterialIcons icon,
            int size,
            Color effectColor,
            int marginRight,
            int marginLeft,
            Action action)
        {
            var button = new MauiIcon
            {
                WidthRequest = size,
                HeightRequest = size
            }.Icon(icon).IconSize(size).Padding(0).Center();

            var frame = new Frame
            {
                Content = new Grid
                {
                    Children = { button },
                    WidthRequest = UIStyles.BUTTON_FRAME_SIZE,
                    HeightRequest = UIStyles.BUTTON_FRAME_SIZE
                }.Center(),
                CornerRadius = UIStyles.BUTTON_FRAME_CORNER_RADIUS,
                HasShadow = false,
                WidthRequest = UIStyles.BUTTON_FRAME_SIZE,
                HeightRequest = UIStyles.BUTTON_FRAME_SIZE,
                BorderColor = Colors.Transparent,
            }.Margins(right: marginRight, left: marginLeft).Padding(UIStyles.BUTTON_FRAME_PADDINGS).BackgroundColor(Colors.Transparent);

            frame.GestureRecognizers.Add(new TapGestureRecognizer().Assign(out TapGestureRecognizer frameTapGestureRecognizer));
            frameTapGestureRecognizer.Tapped += async (sender, e) =>
            {
                await frame.BackgroundColorTo(effectColor, easing: Easing.CubicIn);
                await Task.Delay(200);
                frame.BackgroundColor = Colors.Transparent;
                action?.Invoke();
            };

            return frame;
        }
    }
}
