using System;
using System.Collections.Generic;
using System.IO;

using System.Windows;
using System.Xml.Serialization;

namespace TourProgram.Models
{
    public class Serialization
    {
        public Serialization() { }
        public Serialization(List<Tour> tours, string fileName)
        {
            TourList = tours;
            FileName = fileName;
        }
        public List<Tour> TourList { get; set; }
        public string FileName { get; set; }
        public void Serialize()
        {
            FileStream FileStream = new FileStream(FileName, FileMode.Create);
            XmlSerializer XMLFormatter = new XmlSerializer(typeof(List<Tour>), new Type[] { typeof(CarryingOutForm), typeof(Organizer), typeof(Excursion) });
            try
            {
                XMLFormatter.Serialize(FileStream, TourList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                FileStream.Close();
            }
        }
        public List<Tour> Deserialize()
        {
            FileStream FileStream = new FileStream(FileName, FileMode.Open);
            XmlSerializer XMLFormatter = new XmlSerializer(typeof(List<Tour>), new Type[] { typeof(CarryingOutForm), typeof(Organizer), typeof(Excursion) });
            List<Tour> Tours = new List<Tour>();
            try
            {
                Tours = XMLFormatter.Deserialize(FileStream) as List<Tour>;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                FileStream.Close();
            }
            return Tours;

        }
    }
}
