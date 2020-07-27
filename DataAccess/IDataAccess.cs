using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public interface IDataAccess
    {
        string GetValue(string key);

        void SetValue(string key, string value);
    }
}
