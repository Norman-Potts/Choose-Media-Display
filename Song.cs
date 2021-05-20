using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    /// <summary>
    /// Stores information for a song, derived from media.
    /// </summary>
    class Song : Media
    {
        public string Album { get; protected set;  }
        public string Artist { get; protected set; }

        public Song(string title, int year, string album, string artist ) : base( title, year )
        {
            Album = album;
            Artist = artist;
        }
    }
}
