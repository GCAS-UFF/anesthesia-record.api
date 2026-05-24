namespace UFF.FichaAnestesica.Domain.Entities
{
    public class CurrentLocation : Base
    {
        private CurrentLocation()
        {
        }
        
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

        public static CurrentLocation Update(CurrentLocation currentLocation)
        {
            return new CurrentLocation
            {
                Bed = currentLocation.Bed,
                Floor = currentLocation.Floor,
                Room = currentLocation.Room,
                Unit = currentLocation.Unit
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

        public void SetUnit(Unit unit)
            => this.Unit = unit;

        public void SyncUnit(Unit incoming)
        {
            if (incoming == null)
            {
                Unit = null;
                return;
            }

            if (Unit == null || Unit.Code != incoming.Code)
            {
                Unit = incoming;
            }
            else
            {
                Unit.Sync(incoming);
            }
        }
    }
}