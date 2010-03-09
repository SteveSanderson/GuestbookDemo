using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;

namespace Guestbook.Spec.Steps.Infrastructure
{
    /// <summary>
    /// Just a way of sending a TechTalk.SpecFlow.Table's contents across a remoting boundary
    /// </summary>
    [Serializable]
    public class SerializableTable
    {
        private readonly List<string> _header;
        private readonly List<Dictionary<string, string>> _rows;

        public SerializableTable(Table table)
        {
            _header = table.Header.ToList();
            _rows = (from row in table.Rows
                    select _header.ToDictionary(key => key, key => row[key])).ToList();
        }

        public IList<string> Header { get { return _header.AsReadOnly(); } }
        public IList<Dictionary<string, string>> Rows { get { return _rows.Select(x => new Dictionary<string, string>(x)).ToList(); } }
    }
}