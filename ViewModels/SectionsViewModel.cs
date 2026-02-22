using System;
using FreshFarmApp.Models;
using FreshFarmApp.Services;
using FreshFarmApp.Views;

namespace FreshFarmApp.ViewModels
{
    public class SectionsViewModel
    {
        public SectionsViewModel(INewsService news)
        {
            this.Sections = news.GetCategories();
        }

        public ICollection<Category> Sections { get; set; }
    }
}
