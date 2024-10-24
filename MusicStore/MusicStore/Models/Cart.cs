using MusicStore.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicStore.Models
{
    public class Cart
    {
        [Key]
        public int RecordId { get; set; }
        public string? CartId { get; set; }
        [ForeignKey("AppUser")]
        public string? Id { get; set; }
        public int AlbumId { get; set; }
        public int Count { get; set; }
        public System.DateTime DateCreated { get; set; }
        public virtual Album? Album { get; set; }
        public virtual AppUser? AppUser { get; set; }
    }
}
