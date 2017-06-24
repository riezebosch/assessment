using System.Collections.Generic;
using api.db.model;

namespace api.db
{
    public class WordList
    {
        public List<Entry> Entries { get; set; }
        public string Title { get; set; }
        public int Id { get; set; }
    }
}