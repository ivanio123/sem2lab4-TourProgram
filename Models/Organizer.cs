namespace TourProgram.Models
{
    public class Organizer
    {
        public Organizer() { }
        public Organizer(string Name, string Surname)
        {
            this.Name = Name;
            this.Surname = Surname;
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public override string ToString()
        {
            return "Organizer Name: " + Name + " Surname: " + Surname + "\n\r";
        }
    }
}
