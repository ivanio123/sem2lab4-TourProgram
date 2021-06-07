using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TourProgram.Models
{
    public class Tour
    {
        public Tour() { }
        public Tour(DateTime Start)
        {
            this.Start = Start;
        }
        public DateTime Start { get; set; }
        public decimal Price { get; set; }
        readonly Regex DateReg = new Regex(@"(0[1-9]|[12][0-9]|3[01])[ \.-](0[1-9]|1[012])[ \.-](19|20|)\d\d");
      
        public List<Excursion> Excursions = new List<Excursion>();
        public void AddExcursion(Excursion excursion)
        {
            Excursions.Add(excursion);
        }
        public void DeleteExcursion(Excursion excursion)
        {
            Excursions.Remove(excursion);
        }
        public void CalculatePrice()
        {
            decimal price = 0;
            foreach (var Excursion in Excursions)
            {
                price += Excursion.ExcursionPrice;
            }
            Price = price;
        }
        public bool CheckStart()
        {
            if (DateReg.IsMatch(Start.ToString()))
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
            return " Tour Starting at: " + Start.ToString() + " Tour Price: " + Price.ToString() + "\n\r";
        }
        public string GenerateFullInformation()
        {
            string fullInfo = "";
            fullInfo += this.ToString();
            if (Excursions != null)
            {
                foreach (var excursions in Excursions)
                {
                    fullInfo += excursions.ToString() + "\n\r";
                }
            }
            return fullInfo;
        }
    }
}
