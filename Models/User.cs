using SQLite;
using System;

namespace FreshFarmApp.Models
{
    public class User
    {
        [PrimaryKey]
        public Guid UserID { get; set; } = Guid.NewGuid();

        [Unique, NotNull]
        public string Email { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public byte[] EncryptedAddress { get; set; } = Array.Empty<byte>();
        public byte[] EncryptedPhone { get; set; } = Array.Empty<byte>();

        [NotNull]
        public byte[] PasswordHash { get; set; } = Array.Empty<byte>();

        [NotNull]
        public byte[] Salt { get; set; } = Array.Empty<byte>();

        public int FailedLoginAttempts { get; set; } = 0;
        public bool IsLocked { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
