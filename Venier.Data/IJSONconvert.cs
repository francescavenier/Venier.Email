using System;
using System.Collections.Generic;
using System.Text;

namespace Venier.Data
{
    interface IJSONconvert
    {
        void JSONdeserialize(Message model);
    }
}
