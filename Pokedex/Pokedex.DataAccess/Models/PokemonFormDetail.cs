using System;
using System.ComponentModel.DataAnnotations;

namespace Pokedex.DataAccess.Models
{
    public class PokemonFormDetail  
    {
        public int Id { get; set; }
        [Required]
        public int FormId { get; set; }
        public Form Form { get; set; }
        [Required]
        public string OriginalPokemonId { get; set; }
        public Pokemon OriginalPokemon { get; set; }
        [Required]
        public string AltFormPokemonId { get; set; }
        public Pokemon AltFormPokemon { get; set; }
    }
}