/****************/
/* Mã sinh viên: 202419003 */
/* Họ tên: Nguyễn Thành Trung */
/****************/

using System;
using System.Collections.Generic;

namespace DeviceManagement3
{
    public class InMemoryRepository<T> : IRepository<T> where T : IIdentifiable
    {
        private readonly List<T> _items = new List<T>();

        public void Add(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "Không chấp nhận phần tử null.");

            if (FindById(item.Id) != null)
            {
                throw new DuplicateIdException(item.Id);
            }

            _items.Add(item);
        }

        public bool Remove(string id)
        {
            var item = FindById(id);
            if (item == null) return false;
            return _items.Remove(item);
        }

        public T? FindById(string id)
        {
            foreach (var item in _items)
            {
                if (item.Id.Equals(id, StringComparison.OrdinalIgnoreCase))
                    return item;
            }
            return default;
        }

        public IReadOnlyList<T> GetAll()
        {
            return _items.AsReadOnly();
        }

        public IReadOnlyList<T> FindAll(Predicate<T> condition)
        {
            if (condition == null) throw new ArgumentNullException(nameof(condition));

            var result = new List<T>();
            foreach (var item in _items)
            {
                if (condition(item))
                {
                    result.Add(item);
                }
            }
            return result.AsReadOnly();
        }
    }
}