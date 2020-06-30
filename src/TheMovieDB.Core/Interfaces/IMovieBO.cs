using System;
using System.Collections.Generic;
using System.Text;
using TheMovieDB.Common.DTOs;
using TheMovieDB.Core.Entities;

namespace TheMovieDB.Core.Interfaces
{
    public interface IMovieBO : IDisposable
    {
        IList<MovieDTO> List();
    }
}
