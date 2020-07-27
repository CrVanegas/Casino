using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class ImplementDB
    {
        private IDataAccess dataAccess;

        public ImplementDB(IDataAccess access)
        {
            this.dataAccess = access;
        }

        public string GetValue(string key) => this.dataAccess.GetValue(key);

        public void SetValue(string key, string value) => this.dataAccess.SetValue(value: value, key: key);
    }
}
