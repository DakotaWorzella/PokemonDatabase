using System.ComponentModel.DataAnnotations;

namespace Pokedex.DataAccess.Models
{
    public class PokemonEggGroupDetail
    {
        public int Id { get; set; }
        [Required]
        public int PokemonId { get; set; }
        public Pokemon Pokemon { get; set; }
        [Display(Name = "Primary Egg Group"), Required]
        public int? PrimaryEggGroupId { get; set; }
        public EggGroup PrimaryEggGroup { get; set; }
        [Display(Name = "Secondary Egg Group")]
        public int? SecondaryEggGroupId { get; set; }
        public EggGroup SecondaryEggGroup { get; set; }
        public int GenerationId { get; set; }
        public Generation Generation { get; set; }
    }
}