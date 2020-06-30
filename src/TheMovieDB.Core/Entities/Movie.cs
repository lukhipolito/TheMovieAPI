using System;
using System.Collections.Generic;
using System.Text;
using TheMovieDB.Core.Entities.Base;

namespace TheMovieDB.Core.Entities
{
    public class Movie : BaseEntity
    {
        public decimal popularity { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public int[] genre_ids { get; set; }
        public string title { get; set; }
        public decimal vote_average { get; set; }
        public string overview { get; set; }
        public string release_date { get; set; }
    }
}
