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

        public void Sync(SurgeryLocation incoming)
        {
            Room = incoming.Room;

            if (incoming.SurgicalCenter != null)
            {
                if (SurgicalCenter == null)
                {
                    SurgicalCenter = incoming.SurgicalCenter;
                }
                else
                {
                    SurgicalCenter.Sync(incoming.SurgicalCenter);
                }
            }
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