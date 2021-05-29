using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RateLimiter
{
    public class TransientList<T> : IEnumerable<T>
    {
        private IList<TransientItem<T>> _items;
        public TransientList()
        {
            _items = new List<TransientItem<T>>();
        }

        public T this[int index]
        {
            get => _items[index].Item;
            set => _items[index].Item = value;
        }

        public int Count
            => _items.Count;
        public void Add(T item, TimeSpan time)
        {
            TransientItem<T> tItem = new TransientItem<T>()
            {
                Item = item
            };
            tItem.Task = Task.Delay(time)
                .ContinueWith(_ =>
                {
                    Console.WriteLine("Foo");
                    if (_items.Contains(tItem))
                    {
                        _items.Remove(tItem);
                    }
                });
            tItem.Task.Start();
            _items.Add(tItem);
        }

        public IEnumerator<T> GetEnumerator()
            => _items.Select(_ => _.Item).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator()
            => _items.Select(_ => _.Item).GetEnumerator();
    }
}
