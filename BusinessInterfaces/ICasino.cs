using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessInterfaces
{
    public interface ICasino
    {
        string Create();

        string Open(string id);
    }
}
