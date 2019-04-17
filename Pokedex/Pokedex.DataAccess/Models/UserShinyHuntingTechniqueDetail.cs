using System;
using System.ComponentModel.DataAnnotations;

namespace Pokedex.DataAccess.Models
{
    public class UserShinyHuntingTechniqueDetail
    {
        public int Id { get; set; }
        [Required]
        public int? ShinyHuntingTechniqueId { get; set; }
        public ShinyHuntingTechnique ShinyHuntingTechnique { get; set; }
        [Required]
        public string PokemonId { get; set; }
        public Pokemon Pokemon { get; set; }
        [Required]
        public int? UserId { get; set; }
        public User User { get; set; }
        [Required]
        public string GenerationId { get; set; }
        public Generation Generation { get; set; }
        [Required]
        public int ShinyAttemptCount { get; set; }
        [Required]
        public bool HasShinyCharm { get; set; }
        [Required]
        public bool IsPokemonCaught { get; set; }
        [Required]
        public bool IsArchived { get; set; }
    }
}