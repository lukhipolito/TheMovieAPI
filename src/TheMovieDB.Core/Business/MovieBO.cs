using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using TheMovieDB.Common.DTOs;
using TheMovieDB.Core.DTOs;
using TheMovieDB.Core.Entities;
using TheMovieDB.Core.Interfaces;
using TheMovieDB.Core.Utilities;

namespace TheMovieDB.Core.Business
{
    public class MovieBO : IMovieBO
    {
        private readonly IConfiguration _configuration;
        private readonly string URL_API;
        private readonly string ACCESS_KEY;

        public void Dispose()
        {
            
        }

        public MovieBO(IConfiguration config)
        {
            this._configuration = config;
            URL_API = _configuration.GetSection("TMDB_API").Value;
            ACCESS_KEY = _configuration.GetSection("TMDB_Access_Key").Value;
        }

        public IList<MovieDTO> List()
        {
            List<MovieDTO> lst = new List<MovieDTO>();
            int pageNum = 1;
            string url = GetDiscoverURL(pageNum, DateTime.Now.ToString("yyyy-MM-dd"));
            string response = GetTMDB(url);
            var result = JsonConvert.DeserializeObject<ResponseTransactional<Movie>>(response);

            if (result.total_results > 0)
            {
                pageNum = result.total_pages;

                Movie[] movieList = (Movie[])result.results;
                FillMovieList(lst, movieList);

                for (int i = 2; i <= pageNum; i++)
                {
                    url = GetDiscoverURL(i, DateTime.Now.ToString("yyyy-MM-dd"));
                    response = GetTMDB(url);
                    result = JsonConvert.DeserializeObject<ResponseTransactional<Movie>>(response);

                    if (result.total_results <= 0)
                        break;

                    movieList = (Movie[])result.results;
                    FillMovieList(lst, movieList);
                }

                lst.OrderBy(x => x.Data_Lancamento);
                return lst;
            }
            else
                throw new ArgumentException("Falha ao tentar contatar servidor The Movie");
        }

        internal IList<GenreDTO> GetMovieGenres(int[] ids)
        {
            var list = new List<GenreDTO>();
            if (!ids.Any())
                return list;

            string url = $"{URL_API}genre/movie/list?api_key={ACCESS_KEY}&language=pt-BR";
            string response = GetTMDB(url);
            var result = JsonConvert.DeserializeObject<ResponseGenre<Genre>>(response);
            foreach (var genre in result.genres)
            {
                if (ids.Contains(genre.id))
                    list.Add(genre);
            }
            return list;
        }


        public string GetTMDB(string url)
        {
            var response = RequestUtility.Get(url);
            return response;
        }

        #region private methods
        private void FillMovieList(List<MovieDTO> list, Movie[] movies)
        {
            foreach (var movie in movies)
            {
                if (Convert.ToDateTime(movie.release_date) <= DateTime.Now)
                    continue;

                MovieDTO _movie = (Movie)movie;
                _movie._genres = (List<GenreDTO>)this.GetMovieGenres(movie.genre_ids);
                list.Add(_movie);
            }
        }

        private string GetDiscoverURL(int pageNum, string date)
        {
            return $"{URL_API}discover/movie?api_key={ACCESS_KEY}&language=pt-BR&release_date.gte={date}&page={pageNum.ToString()}&sort_by=release_date.asc&include_adult=false&include_video=false";
        }
        #endregion
    }
}
