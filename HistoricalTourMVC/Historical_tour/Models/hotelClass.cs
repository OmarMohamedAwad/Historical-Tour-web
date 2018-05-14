using Historical_tour.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HistoricTour.Models
{
    public class hotelClass
    {
        public int Id;
        public String Name;
        public double? Rate;
        public double? Exeprience;
        public int? Stars;
        public String VideoKey;
        public String Descrption;
        public String Location;
        public float latidue;
        public float longtude;
        public List<HotelImage> Images;
        public List<HotelReview> Reviews;


        public hotelClass()
        {
        }



        public static hotelClass ConvertToObject(Hotel hotelDB)
        {
            hotelClass hotel = new hotelClass
            {
                Id = hotelDB.ID,
                Name = hotelDB.Name,
                Rate = hotelDB.Rate,
                VideoKey = hotelDB.VKey,
                Descrption = hotelDB.Descrption,
                Location = hotelDB.Location,
                longtude = (float)hotelDB.longitude,
                latidue = (float)hotelDB.latitude,
                Exeprience = hotelDB.Exeprience,
                Stars = hotelDB.Starts
            };




            return hotel;

        }



        public static List<hotelClass> ConvertToList(IEnumerable<Hotel>hotelDB)
        {
            List<hotelClass> hotels = new List<hotelClass>();

            foreach (Hotel h in hotelDB)
            {
                hotels.Add(new hotelClass
                {
                    Id = h.ID,
                    Name = h.Name,
                    Rate = h.Rate,
                    VideoKey = h.VKey,
                    Descrption = h.Descrption,
                    Location = h.Location,
                    longtude = (float)h.longitude,
                    latidue = (float)h.latitude,
                    Exeprience = h.Exeprience,
                    Stars = h.Starts
                });
            }

            return hotels;

        }



    }
}