/****************/
/* Mã sinh viên: 202419003 */
/* Họ tên: Nguyễn Thành Trung */
/****************/

using System;

namespace DeviceManagement3
{
    public abstract class TeachingDevice : IIdentifiable
    {
        public string Id { get; }
        public string Name { get; }
        public string StorageLocation { get; }
        public DeviceStatus Status { get; protected set; }

        protected TeachingDevice(string id, string name, string storageLocation)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Mã thiết bị không được để trống.", nameof(id));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Tên thiết bị không được để trống.", nameof(name));
            if (string.IsNullOrWhiteSpace(storageLocation))
                throw new ArgumentException("Vị trí lưu trữ không được để trống.", nameof(storageLocation));

            Id = id;
            Name = name;
            StorageLocation = storageLocation;
            Status = DeviceStatus.Available;
        }

        public abstract bool IsReadyForUse();
        public abstract double GetDefaultPriorityScore();

        public void Borrow()
        {
            if (!IsReadyForUse())
            {
                throw new DeviceNotReadyException(Id, Status);
            }
            Status = DeviceStatus.Borrowed;
        }

        public void Return()
        {
            if (Status != DeviceStatus.Borrowed)
            {
                throw new InvalidOperationException($"Chỉ có thể trả thiết bị đang ở trạng thái Borrowed. Trạng thái hiện tại: {Status}");
            }
            Status = DeviceStatus.Available;
        }
    }
}