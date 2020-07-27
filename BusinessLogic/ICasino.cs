using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public interface ICasino
    {
        string Create();

        string Open(string id);
    }
}
