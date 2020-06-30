using System;
using System.Collections.Generic;
using System.Text;
using TheMovieDB.Core.Entities.Base;

namespace TheMovieDB.Core.Entities
{
    public class ResponseTransactional<T> where T:BaseEntity
    {
        public int page { get; set; }
        public int total_results { get; set; }
        public int total_pages { get; set; }
        public T[] results { get; set; }
    }

    public class ResponseGenre<T> where T : BaseEntity
    {
        public T[] genres { get; set; }
    }
}
