/****************/
/* Mã sinh viên: 202419003 */
/* Họ tên: Nguyễn Thành Trung */
/****************/

using System;
using System.Collections.Generic;

namespace DeviceManagement3
{
    public class AllocationService<T> where T : TeachingDevice
    {
        private readonly IRepository<T> _repository;
        private ISelectionStrategy<T> _strategy;

        public AllocationService(IRepository<T> repository, ISelectionStrategy<T> strategy)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
        }

        public void ChangeStrategy(ISelectionStrategy<T> newStrategy)
        {
            _strategy = newStrategy ?? throw new ArgumentNullException(nameof(newStrategy));
        }

        public T Allocate(Predicate<T> additionalCondition)
        {
            if (additionalCondition == null)
                additionalCondition = _ => true;

            var candidates = _repository.FindAll(d => d.IsReadyForUse() && additionalCondition(d));

            if (candidates.Count == 0)
            {
                throw new NoSuitableItemException("Không tìm thấy thiết bị nào thỏa mãn điều kiện sẵn sàng.");
            }

            T selectedDevice = _strategy.Select(candidates);
            selectedDevice.Borrow();

            return selectedDevice;
        }
    }
}