/****************/
/* Mã sinh viên: 202419003 */
/* Họ tên: Nguyễn Thành Trung */
/****************/

using System;

namespace DeviceManagement3
{
    public class DuplicateIdException : Exception
    {
        public string DuplicateId { get; }

        public DuplicateIdException(string duplicateId)
            : base($"Mã thiết bị '{duplicateId}' đã tồn tại trong kho.")
        {
            DuplicateId = duplicateId;
        }
    }

    public class NoSuitableItemException : Exception
    {
        public NoSuitableItemException()
            : base("Không tìm thấy thiết bị nào phù hợp với điều kiện hoặc danh sách ứng viên rỗng.")
        {
        }

        public NoSuitableItemException(string message) : base(message) { }
    }

    public class DeviceNotReadyException : Exception
    {
        public string DeviceId { get; }
        public DeviceStatus CurrentStatus { get; }

        public DeviceNotReadyException(string deviceId, DeviceStatus currentStatus)
            : base($"Thiết bị '{deviceId}' không sẵn sàng để sử dụng (Trạng thái hiện tại: {currentStatus}).")
        {
            DeviceId = deviceId;
            CurrentStatus = currentStatus;
        }
    }
}