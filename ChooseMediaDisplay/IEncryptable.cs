using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChooseMediaDisplay
{
    /// <summary>
    /// The class implementing the interface will be able to use methods Encrypt or Decrypt.
    /// The class using the Encrypt method will return a string.
    /// The class using the Decrypt method will return a string.
    /// 
    /// </summary>
    interface IEncryptable
    {
        string Encrypt();
        string Decrypt();
    }
}
