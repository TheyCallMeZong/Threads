using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threads
{
    class Collections<T>
    {
        private List<T> _collections;
        private object locker;

        public Collections()
        {
            _collections = new List<T>();
            locker = new object();
        }

        public void AddItem(T item)
        {
            lock (locker)
            {
                _collections.Add(item);
            }
        }
        public void DeleteItem(int item)
        {
            lock (locker)
            {
                _collections.RemoveAt(item);
            }
        }

        public List<T> GetList()
        {
            lock (locker)
            {
                return _collections;
            }
        }
    }
}
