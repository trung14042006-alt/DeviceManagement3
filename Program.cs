/****************/
/* Mã sinh viên: 202419003 */
/* Họ tên: Nguyễn Thành Trung */
/****************/

using System;

namespace DeviceManagement3
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== CHƯƠNG TRÌNH QUẢN LÝ CẤP PHÁT THIẾT BỊ GIẢNG DẠY (DEVICEMANAGEMENT3) ===\n");

            IRepository<ProjectorRemote> projectorRepo = new InMemoryRepository<ProjectorRemote>();
            IRepository<AirConditionerRemote> acRepo = new InMemoryRepository<AirConditionerRemote>();
            IRepository<WirelessMicrophone> micRepo = new InMemoryRepository<WirelessMicrophone>();

            var proj1 = new ProjectorRemote("PJ01", "Máy chiếu Sony 1", "Kho A", 15);
            var proj2 = new ProjectorRemote("PJ02", "Máy chiếu Sony 2", "Kho A", 85);
            var proj3 = new ProjectorRemote("PJ03", "Máy chiếu Sony 3", "Kho A", 95);

            var ac1 = new AirConditionerRemote("AC01", "ĐH Panasonic 1", "Kho B", new[] { "A201", "A202" });
            var ac2 = new AirConditionerRemote("AC02", "ĐH Daikin 2", "Kho B", new[] { "A203", "A204" });

            var mic1 = new WirelessMicrophone("MIC01", "Micro Shure 1", "Kho C", 20, 90);
            var mic2 = new WirelessMicrophone("MIC02", "Micro Sony 2", "Kho C", 80, 85);
            var mic3 = new WirelessMicrophone("MIC03", "Micro Bose 3", "Kho C", 90, 95);

            projectorRepo.Add(proj1);
            projectorRepo.Add(proj2);
            projectorRepo.Add(proj3);

            acRepo.Add(ac1);
            acRepo.Add(ac2);

            micRepo.Add(mic1);
            micRepo.Add(mic2);
            micRepo.Add(mic3);
            Console.WriteLine("1. Thêm thành công các thiết bị mẫu vào kho.");

            RunTest("Thêm thiết bị trùng mã 'PJ02'", () => {
                var duplicateProj = new ProjectorRemote("PJ02", "Trùng mã", "Kho A", 50);
                projectorRepo.Add(duplicateProj);
            });

            var found = projectorRepo.FindById("PJ03");
            Console.WriteLine($"3. Tìm theo mã 'PJ03': {(found != null ? found.Name : "Không tìm thấy")}");

            var readyProjectors = projectorRepo.FindAll(d => d.IsReadyForUse());
            Console.WriteLine($"4. Số lượng máy chiếu sẵn sàng trong kho: {readyProjectors.Count} (Mong đợi: 2)");

            var projStrategy = new HighestPriorityStrategy<ProjectorRemote>(d => d.GetDefaultPriorityScore());
            var projService = new AllocationService<ProjectorRemote>(projectorRepo, projStrategy);

            var allocatedProj = projService.Allocate(d => true);
            Console.WriteLine($"5. Đã cấp phát máy chiếu tốt nhất: {allocatedProj.Id} - {allocatedProj.Name} (Pin: {allocatedProj.BatteryLevel}%)");

            var acStrategy = new FirstAvailableStrategy<AirConditionerRemote>();
            var acService = new AllocationService<AirConditionerRemote>(acRepo, acStrategy);

            var allocatedAc = acService.Allocate(d => d.IsCompatibleWith("A203"));
            Console.WriteLine($"6. Đã cấp phát điều hòa cho phòng A203: {allocatedAc.Id} - {allocatedAc.Name}");

            var micStrategy = new FirstAvailableStrategy<WirelessMicrophone>();
            var micService = new AllocationService<WirelessMicrophone>(micRepo, micStrategy);

            var allocatedMic = micService.Allocate(d => true);
            Console.WriteLine($"7. Đã cấp phát micro đầu tiên: {allocatedMic.Id} - {allocatedMic.Name}");

            Console.WriteLine("8. Đổi chiến lược cấp phát máy chiếu sang FirstAvailableStrategy:");
            projService.ChangeStrategy(new FirstAvailableStrategy<ProjectorRemote>());
            var allocatedProj2 = projService.Allocate(d => d.Id == "PJ02");
            Console.WriteLine($"   -> Kết quả đổi chiến lược chọn được: {allocatedProj2.Id} - {allocatedProj2.Name}");

            RunTest("Cấp phát điều hòa cho phòng không tồn tại 'Z999'", () => {
                acService.Allocate(d => d.IsCompatibleWith("Z999"));
            });

            Console.WriteLine($"10. Trạng thái hiện tại của thiết bị được chọn {allocatedProj.Id}: {allocatedProj.Status} (Mong đợi: Borrowed)");
            Console.WriteLine($"11. Repository tổng quát hoạt động tốt cho cả ProjectorRemote ({projectorRepo.GetAll().Count}) và AirConditionerRemote ({acRepo.GetAll().Count}).");

            Console.WriteLine("\n=== HOÀN TẤT KIỂM THỬ TOÀN BỘ HỆ THỐNG ===");
        }

        private static void RunTest(string testName, Action testAction)
        {
            Console.Write($"[TEST] {testName} ... ");
            try
            {
                testAction();
                Console.WriteLine("THẤT BẠI (Không bắt được ngoại lệ dự kiến).");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"THÀNH CÔNG (Bắt được ngoại lệ loại {ex.GetType().Name}): {ex.Message}");
            }
        }
    }
}