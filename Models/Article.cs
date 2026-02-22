using SQLite;

namespace FreshFarmApp.Models;

public class Article
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;
    public string ImageURL { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Time { get; set; } = string.Empty;
}
