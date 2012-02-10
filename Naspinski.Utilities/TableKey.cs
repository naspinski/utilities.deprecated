using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Naspinski.Utilities
{
    /// <summary>
    /// Interface for getting the table Key result
    /// </summary>    
    public interface IPrimaryKey
    {

    }

    /// <summary>
    /// Table Key
    /// </summary>    
    public class TableKey : IPrimaryKey
    {

        /// <summary>
        /// Contructor for the tableKey
        /// </summary>        
        /// <param name="t">Property info of the TableKey</param>        
        /// <returns>TableKey</returns>
        public TableKey(PropertyInfo t)
        {
            this.PropertyInfo = t;
        }

        /// <summary>
        /// Contructor for the tableKey
        /// </summary>                
        /// <returns>TableKey</returns>
        public TableKey() { }

        public PropertyInfo PropertyInfo { get; set; }
    }

    /// <summary>
    /// Class for multiple Keys
    /// </summary>            
    public class TableMultipleKey : IPrimaryKey
    {
        private List<TableKey> _Keys;

        /// <summary>
        /// Key list
        /// </summary>                
        public List<TableKey> Keys
        {
            get
            {
                if (this._Keys == null) throw new Exception("Keys not initializad");
                return _Keys;
            }
        }

        /// <summary>
        /// Procedure to add a Key to the list of keys
        /// </summary>        
        /// <param name="t">Table Key</param>                
        public void AddKey(TableKey t)
        {
            if (this._Keys == null) this._Keys = new List<TableKey>();
            this._Keys.Add(t);
        }

        /// <summary>
        /// Procedure to add a multiple Keys from a List of PropertyInfo
        /// </summary>        
        /// <param name="t">List of PropertyInfo</param>                
        public void AddKeys(List<PropertyInfo> t)
        {
            foreach (PropertyInfo a in t) AddKey(new TableKey(a));
        }

    }
}
