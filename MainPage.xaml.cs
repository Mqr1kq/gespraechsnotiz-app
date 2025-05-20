using Gespraechsnotiz_App.ViewModels;

namespace Gespraechsnotiz_App
{
    public partial class MainPage : ContentPage
    {
        //public MainPage()
        //{
        //    Title = "Notes";
        //    var viewModel = new MainViewModel();
        //    BindingContext = viewModel;

        //    Content = new VerticalStackLayout
        //    {
        //        Children =
        //        {
        //            new Button
        //            {
        //                Text = "New Note",
        //                Command = viewModel.AddNoteCommand
        //            },
        //            new CollectionView
        //            {
        //                ItemTemplate = new DataTemplate(() =>
        //                {
        //                    var title = new Label { FontAttributes = FontAttributes.Bold };
        //                    title.SetBinding(Label.TextProperty, "Topic");

        //                    var date = new Label { FontSize = 12 };
        //                    date.SetBinding(Label.TextProperty, new Binding("CreatedAt", stringFormat: "{0:dd.MM.yyyy, HH:mm:ss}"));

        //                    return new Frame
        //                    {
        //                        Content = new VerticalStackLayout
        //                        {
        //                            Children = { date, title }
        //                        }
        //                    };
        //                }),
        //                ItemsSource = viewModel.Notes
        //            }
        //        }
        //    };
        }
    }

}
