using Pokedex.DataAccess.Models;
using System.Collections.Generic;

namespace Pokedex.Models
{
    /// <summary>
    /// The class that is used to represent the comment's view model.
    /// </summary>
    public class CommentViewModel : Comment
    {
        /// <summary>
        /// Gets or sets a list of pages to comment on.
        /// </summary>
        public List<Comment> AllComments { get; set; }

        /// <summary>
        /// Gets or sets a list of pages to comment on.
        /// </summary>
        public List<CommentPage> AllPages { get; set; }

        /// <summary>
        /// Gets or sets a list of types of comment possible.
        /// </summary>
        public List<CommentCategory> AllCategories { get; set; }
    }
}
