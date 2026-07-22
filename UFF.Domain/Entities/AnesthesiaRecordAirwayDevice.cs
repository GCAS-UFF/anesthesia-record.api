using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Entities
{
    public class AnesthesiaRecordAirwayDevice
    {
        private AnesthesiaRecordAirwayDevice() { }

        public int Id { get; private set; }
        public int AnesthesiaRecordId { get; private set; }
        public AnesthesiaRecord AnesthesiaRecord { get; private set; } = null!;
        public AirwayDeviceTypeEnum DeviceType { get; private set; }

        public static AnesthesiaRecordAirwayDevice Create(AirwayDeviceTypeEnum deviceType)
        {
            return new AnesthesiaRecordAirwayDevice
            {
                DeviceType = deviceType
            };
        }
    }
}