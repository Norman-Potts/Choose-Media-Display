using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    /// <summary>
    /// Stores information for a book, derived from media.
    /// Has two special properties, Author and summary.
    /// Has two inherited properties from media, title and year.
    /// The smmary can be encrypted and decrypted.
    /// 
    /// </summary>
    class Book : Media, IEncryptable
    {
        public string Author { get; protected set;  }
        public string Summary { get; protected set; }

        public Book(string title, int year, string author, string summary) : base(title, year)
        {
            Author = author;
            Summary = summary;
        }
        #region IEncryptable


        /// <summary>
        /// Uses Rot13 algorithm to encrypt string.
        /// </summary>
        /// <returns></returns>
        public string Encrypt()
        {
            return Decrypt();
        }


        /// <summary>
        /// Uses Rot 13 algorithm to decrypt string.
        /// </summary>
        /// <returns></returns>
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
