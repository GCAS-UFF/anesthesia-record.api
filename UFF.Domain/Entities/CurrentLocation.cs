namespace UFF.FichaAnestesica.Domain.Entities
{
    public class CurrentLocation : Base
    {
        private CurrentLocation() { }
        public Unit Unit { get; private set; }
        public string Bed { get; private set; }
        public string Floor { get; private set; }
        public string Room { get; private set; }

        public static CurrentLocation Create(string bed, string floor, string room, Unit unit)
        {
            return new CurrentLocation
            {
                Bed = bed,
                Floor = floor,
                Room = room,
                Unit = unit
            };
        }
   
        public void Sync(CurrentLocation incoming)
        {
            if (incoming == null)
                return;

            Bed = incoming.Bed;
            Floor = incoming.Floor;
            Room = incoming.Room;

            SyncUnit(incoming.Unit);
        }
      
        public void SyncUnit(Unit incoming)
        {
            if (incoming == null)
            {
                Unit = null;
                return;
            }

            if (Unit == null)
            {
                Unit = incoming;
                return;
            }

            Unit.Sync(incoming);
        }
    }
}