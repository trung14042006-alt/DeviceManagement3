/****************/
/* Mã sinh viên: 202419003 */
/* Họ tên: Nguyễn Thành Trung */
/****************/

using System;
using System.Collections.Generic;

namespace DeviceManagement3
{
    public class HighestPriorityStrategy<T> : ISelectionStrategy<T>
    {
        private readonly Func<T, double> _scoreSelector;

        public HighestPriorityStrategy(Func<T, double> scoreSelector)
        {
            _scoreSelector = scoreSelector ?? throw new ArgumentNullException(nameof(scoreSelector));
        }

        public T Select(IReadOnlyList<T> candidates)
        {
            if (candidates == null || candidates.Count == 0)
                throw new NoSuitableItemException("Danh sách ứng viên rỗng, không thể chọn thiết bị.");

            T bestItem = candidates[0];
            double maxScore = _scoreSelector(bestItem);

            for (int i = 1; i < candidates.Count; i++)
            {
                double score = _scoreSelector(candidates[i]);
                if (score > maxScore)
                {
                    maxScore = score;
                    bestItem = candidates[i];
                }
            }

            return bestItem;
        }
    }

    public class FirstAvailableStrategy<T> : ISelectionStrategy<T>
    {
        public T Select(IReadOnlyList<T> candidates)
        {
            if (candidates == null || candidates.Count == 0)
                throw new NoSuitableItemException("Danh sách ứng viên rỗng, không thể chọn thiết bị.");

            return candidates[0];
        }
    }
}