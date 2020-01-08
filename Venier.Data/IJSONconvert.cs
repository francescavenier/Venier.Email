using System;
using System.Collections.Generic;
using System.Text;

namespace Venier.Data
{
    interface IJSONconvert
    {
        Message JSONdeserialize(string input);
    }
}
