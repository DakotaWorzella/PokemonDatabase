using Pokedex.DataAccess.Models;
using System.Collections.Generic;

namespace Pokedex.Models
{
    /// <summary>
    /// The class that is used to represent the view model for the page to change an alternate forms' form type.
    /// </summary>
    public class AlternateFormsFormViewModel
    {
        /// <summary>
        /// Gets or sets the pokemon form detail.
        /// </summary>
        public PokemonFormDetail PokemonFormDetail { get; set; }

        /// <summary>
        /// Gets or sets a list of all forms.
        /// </summary>
        public List<Form> AllForms { get; set; }
    }
}
