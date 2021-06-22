using DappAPI.Extensions.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace DappAPI.Models
{
    public class Capital
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public double Value { get; set; }
        [Required]
        public AssetType Asset { get; set; }
        [Required]
        public CapitalType Type { get; set; }
        [Required]
        public CapitalStatus Status { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }

        public Guid CreatorId { get; set; }
        public DappUser Creator { get; set; }

        public Capital(long id, string title, string description, double value, AssetType asset, CapitalType type)
        {
            Id = id;
            Title = title;
            Description = description;
            Value = value;
            Asset = asset;
            Type = type;
            CreationDate = DateTime.Today;
        }
    }
}
