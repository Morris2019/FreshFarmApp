using FreshFarmApp.Services;
using FreshFarmApp.ViewModels;

namespace FreshFarmApp.Views;

public partial class SectionsPage : ContentPage
{
    public SectionsPage(INewsService news)
    {
        InitializeComponent();
        this.BindingContext = new SectionsViewModel(news);
    }
       
    
}
