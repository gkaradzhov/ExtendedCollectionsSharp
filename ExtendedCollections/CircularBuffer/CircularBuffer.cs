using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtendedCollections.Interfaces;

namespace ExtendedCollections
{
    public class CircularBuffer<T> : IBuffer<T>
    {
        #region Private fields
        private Queue<T> _buffer;
        private int _capacity;
        #endregion

        #region Constructors
        public CircularBuffer(int capacity)
        {
            Initialize(capacity);
        }
        public CircularBuffer(IEnumerable<T> currentCollection)
        {
            Initialize(currentCollection);
        }
        #endregion

        #region Public properties
        /// <summary>
        /// Changes the capacity of an existent buffer and copies the old bufers values 
        /// </summary>
        public int Capacity
        {
            get { return _capacity; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("value", "must be positive");

                if (value == _buffer.Count)
                    return;

                Initialize(_buffer, value);
            }
        }
        public bool IsEmpty
        {
            get { return _buffer.Count == 0; }
        }
        public bool IsFull
        {
            get { return _buffer.Count == Capacity; }
        }
        #endregion

        #region Public methods
        public void Write(T item)
        {
            if (IsFull)
            {
                var itemDiscarded = _buffer.Dequeue();
                OnItemDiscarded(itemDiscarded, item);
            }
            _buffer.Enqueue(item);
        }

        private void OnItemDiscarded(T itemDiscarded, T newItem)
        {
            if (ItemDiscarded == null) return;
            var args = new ItemDiscardedEventArgs<T>(itemDiscarded, newItem);
            ItemDiscarded(this, args);
        }
        public T Read()
        {
            if (!IsEmpty) return _buffer.Dequeue();
            throw new InvalidOperationException("Buffer is empty");
        }
        public void Clear()
        {
            _buffer.Clear();
        }

        /// <summary>
        /// Peek element at specific postition. Note, if you use the position parameter the complexity will no longer be O(1)
        /// </summary>
        /// <param name="position">(Optional)Position of element to peek</param>
        /// <returns>Element of buffer</returns>
        public T Peek(int position = 0)
        {
            if (!IsEmpty) return position != 0 ? _buffer.ElementAt(position) : _buffer.Peek();
            throw new InvalidOperationException("Buffer is empty");
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _buffer.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public event EventHandler<ItemDiscardedEventArgs<T>> ItemDiscarded; 
        #endregion

        #region Private methods
        private void Initialize(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException("capacity", "must be positive");

            _buffer = new Queue<T>(capacity);
            _capacity = capacity;
        }
        private void Initialize(IEnumerable<T> currentCollection)
        {
            var collection = currentCollection as IList<T> ?? currentCollection.ToList();
            int capacity = collection.Count();
            if (capacity < 0)
                throw new ArgumentOutOfRangeException("capacity", "must be positive");

            _buffer = new Queue<T>(collection);
            _capacity = capacity;
        }

        private void Initialize(IEnumerable<T> currentCollection, int capacity)
        {
            var collection = currentCollection as IList<T> ?? currentCollection.ToList();
            if (capacity < 0)
                throw new ArgumentOutOfRangeException("capacity", "must be positive");
            if (collection.Count > capacity)
            {
                throw new ArgumentOutOfRangeException("capacity", "must be larger than Collection size");
            }
            _buffer = new Queue<T>(collection);
            _capacity = capacity;
        }
        #endregion

    }
}
