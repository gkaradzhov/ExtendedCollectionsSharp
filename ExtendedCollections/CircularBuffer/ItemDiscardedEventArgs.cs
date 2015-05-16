using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedCollections
{
    public class ItemDiscardedEventArgs<T> : EventArgs
    {
        public ItemDiscardedEventArgs(T discard, T newItem)
        {
            ItemDiscarded = discard;
            NewItem = newItem;
        }
        public T ItemDiscarded { get; set; }
        public T NewItem { get; set; }
    }
}
