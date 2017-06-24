using System;
using System.Collections.Generic;
using api.db.model;

namespace api.Controllers.tests
{
    internal class EntryComparer : IEqualityComparer<Entry>
    {
        public bool Equals(Entry x, Entry y)
        {
            return x.Id == y.Id &&
                x.Word == y.Word &&
                x.Translation == y.Translation;
        }

        public int GetHashCode(Entry obj)
        {
           return obj.Id.GetHashCode() ^
            obj.Translation.GetHashCode() ^
            obj.Word.GetHashCode();
        }
    }
}