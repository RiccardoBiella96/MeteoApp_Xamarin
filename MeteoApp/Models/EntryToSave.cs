using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeteoApp.Models
{
    public class EntryToSave
    {
        [PrimaryKey]
        public string key { get; set; }

        public string value { get; set; }

    }
}
