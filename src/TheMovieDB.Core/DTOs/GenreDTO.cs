using System;
using System.Collections.Generic;
using System.Text;
using TheMovieDB.Core.Entities;

namespace TheMovieDB.Core.DTOs
{
    public class GenreDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public static implicit operator GenreDTO(Genre genre)
        {
            return new GenreDTO
            {
                Id = genre.id,
                Name = genre.name
            };
        }
    }
}
