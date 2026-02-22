using System.Collections.ObjectModel;
using FreshFarmApp.Data;
using FreshFarmApp.Models;
using FreshFarmApp.Services;

namespace FreshFarmApp.ViewModels;

public class HomeViewModel
{
    private readonly AppDatabase _db; 
    public HomeViewModel(AppDatabase db)
    {
        _db = db;
        LoadArticles();
    }

    public ObservableCollection<Article> LatestArticles { get; } = new();

    private async void LoadArticles()
    {
        var articles = await _db.GetArticlesAsync();
        LatestArticles.Clear();
        foreach (var article in articles)
            LatestArticles.Add(article);
    }
}