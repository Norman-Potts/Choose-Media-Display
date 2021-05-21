using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChooseMediaDisplay
{
    /// <summary>
    /// Stores information for a movie, derived from media.
    /// </summary>
    class Movie : Media, IEncryptable
    {
        public string Director { get; protected set; }
        public string Summary { get; protected set; }

        public Movie(string title, int year, string director, string summary) : base(title, year)
        {
            Director = director;
            Summary = summary;
        }

        #region IEncryptable


        /// <summary>
        /// Uses Rot13 algorithm to encrypt string
        /// </summary>
        /// <returns> The encrypted string</returns>
        public string Encrypt()
        {
            return Decrypt();
        }


        /// <summary>
        ///  uses Rot13 algorithm to encrypt string
        /// </summary>
        /// <returns>The encrypted string</returns>
        public string Decrypt()
        {
            char[] charArr = Summary.ToCharArray();
            for (int i = 0; i < charArr.Length; i++)
            {
                int number = (int)charArr[i];
                if (number >= 'a' && number <= 'z')
                {
                    if (number > 'm')
                    {
                        number -= 13;
                    }
                    else
                    {
                        number += 13;
                    }
                }
                else if (number >= 'A' && number <= 'Z')
                {
                    if (number > 'M')
                    {
                        number -= 13;
                    }
                    else
                    {
                        number += 13;
                    }
                }
                charArr[i] = (char)number;

            }
            return new string(charArr);

            #endregion /// End IEncryptable
        }
    }
}
