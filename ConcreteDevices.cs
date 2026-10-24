/****************/
/* Mã sinh viên: 202419003 */
/* Họ tên: Nguyễn Thành Trung */
/****************/

using System.Collections.Generic;

namespace DeviceManagement3
{
    public class ProjectorRemote : TeachingDevice
    {
        public int BatteryLevel { get; } // Từ 0 đến 100

        public ProjectorRemote(string id, string name, string storageLocation, int batteryLevel)
            : base(id, name, storageLocation)
        {
            BatteryLevel = batteryLevel;
        }

        public override bool IsReadyForUse()
        {
            return Status == DeviceStatus.Available && BatteryLevel >= 20;
        }

        public override double GetDefaultPriorityScore()
        {
            return BatteryLevel;
        }
    }

    public class AirConditionerRemote : TeachingDevice
    {
        public IReadOnlySet<string> CompatibleRooms { get; }

        public AirConditionerRemote(string id, string name, string storageLocation, IEnumerable<string> compatibleRooms)
            : base(id, name, storageLocation)
        {
            CompatibleRooms = new HashSet<string>(compatibleRooms);
        }

        public bool IsCompatibleWith(string roomId)
        {
            return CompatibleRooms.Contains(roomId);
        }

        public override bool IsReadyForUse()
        {
            return Status == DeviceStatus.Available;
        }

        public override double GetDefaultPriorityScore()
        {
            return IsReadyForUse() ? 100.0 : 0.0;
        }
    }

    public class WirelessMicrophone : TeachingDevice
    {
        public int BatteryLevel { get; }       // Từ 0 đến 100
        public int SignalQuality { get; }      // Từ 0 đến 100

        public WirelessMicrophone(string id, string name, string storageLocation, int batteryLevel, int signalQuality)
            : base(id, name, storageLocation)
        {
            BatteryLevel = batteryLevel;
            SignalQuality = signalQuality;
        }

        public override bool IsReadyForUse()
        {
            return Status == DeviceStatus.Available && BatteryLevel >= 30 && SignalQuality >= 40;
        }

        public override double GetDefaultPriorityScore()
        {
            return (BatteryLevel + SignalQuality) / 2.0;
        }
    }
}