using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using TheMovieDB.Core.DTOs;
using TheMovieDB.Core.Entities;

namespace TheMovieDB.Common.DTOs
{
    public class MovieDTO
    {
        public string Titulo { get; set; }

        public string[] Generos { get => _genres?.Select(x => x.Name).ToArray(); }

        public DateTime Data_Lancamento { get; set; }

        public string Sumario { get; set; }

        public decimal Avaliacao_Media { get; set; }

        [JsonIgnore]
        internal List<GenreDTO> _genres { get; set; }


        public static implicit operator MovieDTO(Movie movie)
        {
            return new MovieDTO()
            {
                Titulo = movie.title,
                Data_Lancamento = Convert.ToDateTime(movie.release_date),
                Sumario = movie.overview,
                Avaliacao_Media = movie.vote_average
            };
        }
    }
}
