/****************/
/* Mã sinh viên: 202419003 */
/* Họ tên: Nguyễn Thành Trung */
/****************/

using System;
using System.Collections.Generic;

namespace DeviceManagement3
{
    public interface IIdentifiable
    {
        string Id { get; }
    }

    public interface IRepository<T> where T : IIdentifiable
    {
        void Add(T item);
        bool Remove(string id);
        T? FindById(string id);
        IReadOnlyList<T> GetAll();
        IReadOnlyList<T> FindAll(Predicate<T> condition);
    }

    public interface ISelectionStrategy<T>
    {
        T Select(IReadOnlyList<T> candidates);
    }
}