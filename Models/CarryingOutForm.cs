namespace TourProgram.Models
{
    public class CarryingOutForm
    {
        public CarryingOutForm() { }
        public CarryingOutForm(CarryingOut carryingOut)
        {
            this.carryingOut = carryingOut;
        }

        public enum CarryingOut { ByBus, Walking, HorsebackRide, TripTransport }
        public CarryingOut carryingOut { get; set; }
        public override string ToString()
        {
            return "CarryingOut: " + carryingOut.ToString() + "\n\r";
        }
    }
}
