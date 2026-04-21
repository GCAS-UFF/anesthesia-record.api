namespace UFF.FichaAnestesica.Domain.Entities
{
    public class SurgeryLocation : Base
    {
        private SurgeryLocation() { }

        public SurgicalCenter SurgicalCenter { get; private set; }
        public string Room { get; private set; }

        public void SetSurgicalCenter(SurgicalCenter center)
        {
            SurgicalCenter = center;
        }

        public static SurgeryLocation Create(string room, SurgicalCenter surgicalCenter)
        {
            return new SurgeryLocation
            {
                Room = room,
                SurgicalCenter = surgicalCenter
            };
        }
    }
}