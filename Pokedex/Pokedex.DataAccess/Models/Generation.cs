using System;
using System.ComponentModel.DataAnnotations;

namespace Pokedex.DataAccess.Models
{
    public class Generation   
    {
        [StringLength(4)]
        public string Id { get; set; }
        [StringLength(6)]
        [Required]
        public string Region { get; set; }
        [StringLength(50)]
        [Required]
        public string Games { get; set; }
        [StringLength(5)]
        [Required]
        public string Abbreviation { get; set; }
        [Display(Name = "Release Date")]
        [Required]
        public DateTime ReleaseDate { get; set; }
        [Required]
        public bool IsArchived { get; set; }
    }
}