using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChooseMediaDisplay
{
    /// <summary>
    /// The class implementing ISearchable will use a key to search by and return a boolean of the results.
    /// </summary>
    interface ISearchable
    {
        bool Search(string key);
    }
}
