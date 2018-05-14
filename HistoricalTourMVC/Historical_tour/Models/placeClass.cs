using Historical_tour.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HistoricTour.Models
{
    public class placeClass
    {
        public int Id;
        public String Name;
        public double ?Rate;
        public string VideoKey;
        public List<PlaceImage> Images;
        public String Descrption;
        public String Location;
        public float longitude;
        public float latitude;
        public List<PlaceDay> WorkingDays;
        public List<PlaceReview> Reviews;



        public static placeClass ConvertToObject(Place placesDB)
        {
            placeClass place = new placeClass
            {
                Id = placesDB.ID,
                Name = placesDB.Name,
                Rate = placesDB.Rate,
                VideoKey = placesDB.VKey,
                Descrption = placesDB.Description,
                Location = placesDB.Location,
                longitude = (float)placesDB.longitude,
                latitude = (float)placesDB.latitude
            };

            return place;
        }





        public static List<placeClass> ConvertToList(IEnumerable<Place> placesDB)
        {
            List<placeClass> places = new  List<placeClass>();

            foreach(Place p in placesDB)
            {
                places.Add(new placeClass
                {
                    Id = p.ID,
                    Name = p.Name,
                    Rate = p.Rate,
                    VideoKey = p.VKey,
                    Descrption = p.Description,
                    Location = p.Location,
                    longitude = (float)p.longitude,
                    latitude = (float)p.latitude
                });
            }

            return places;

        }

    }
}