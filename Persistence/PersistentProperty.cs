using System;
using System.Collections.Generic;
using System.Text;

namespace YPIConnect.Persistence
{
    public class PersistentProperty : System.Attribute
    {
        string m_SqlName;        

        public PersistentProperty(string sqlName)
        {
            this.m_SqlName = sqlName;
        }   
        
        public string SqlName
        {
            get { return this.m_SqlName; }
        }
    }
}
