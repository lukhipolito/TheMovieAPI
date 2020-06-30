using System;
using System.Collections.Generic;
using System.Text;
using TheMovieDB.Core.Entities.Base;

namespace TheMovieDB.Core.Entities
{
    public class Genre : BaseEntity
    {
        public string name { get; set; }
    }
}
