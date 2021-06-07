using System.Text.RegularExpressions;

namespace TourProgram.Models
{
    public class Excursion
    {
        public Excursion() { }
        public Excursion(Organizer organizer, CarryingOutForm carryingOut, decimal excursionPrice, string excursionPlace)
        {
            Organizer = organizer;
            CarryingOut = carryingOut;
            ExcursionPrice = excursionPrice;
            ExcurcionPlace = excursionPlace;
        }

        public Organizer Organizer { get; set; }
        public CarryingOutForm CarryingOut { get; set; }
        public decimal ExcursionPrice { get; set; }
        public string ExcurcionPlace { get; set; }
        readonly Regex NameReg = new Regex(@"^[a-zA-Z]+\s?$");
        readonly Regex PriceReg = new Regex(@"^[\d]+(((,|\.)[1-9]+))?$");
        public bool CheckName()
        {
            if (NameReg.IsMatch(Organizer.Name))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool CheckSurname()
        {
            if (NameReg.IsMatch(Organizer.Surname))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool CheckPrice()
        {
            if (PriceReg.IsMatch(ExcursionPrice.ToString()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public override string ToString()
        {
            return " Organizer: " + Organizer.ToString() + " Carrying out form: " + CarryingOut.ToString() + " Excursion Price: " + ExcursionPrice.ToString() + " Excursion Place: " + ExcurcionPlace + "\n\r";
        }

    }
}
