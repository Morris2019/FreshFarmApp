using SQLite;
using System;

namespace FreshFarmApp.Models
{
    public class FarmProduct
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Indexed]
        public string Category { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public string Unit { get; set; } = "kg";

        public double QuantityAvailable { get; set; }

        public bool IsOrganic { get; set; }

        public string? ImagePath { get; set; } = string.Empty;

        public string FarmerName { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public DateTime? HarvestDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public double Rating { get; set; }

        public bool IsAvailable { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
