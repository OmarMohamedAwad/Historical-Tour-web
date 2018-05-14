using Historical_tour.Models;
using HistoricTour.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Historical_tour.Controllers
{
    public class HotelController : Controller
    {



        public static HistoricTourDataContext DB = new HistoricTourDataContext();
        List<List<hotelClass>> hotelsarr = new List<List<hotelClass>>();

        public static List<hotelClass> hotels = hotelClass.ConvertToList(DB.Hotels);

        

        [HttpGet]
        public ActionResult GetHotel(int id)
        {
            List<hotelClass> hotels = hotelClass.ConvertToList(DB.Hotels);
            hotelClass hotel = hotelClass.ConvertToObject(DB.Hotels.Where(h => h.ID == id).SingleOrDefault());
            hotel.Reviews = DB.HotelReviews.Where(r => r.HotelID == id).ToList();
            hotel.Images = DB.HotelImages.Where(r => r.HotelID == id).ToList();

            return View(hotel);

        }


        [HttpGet]
        public ActionResult GetHotels(int type)
        {
            List<hotelClass> hotels = hotelClass.ConvertToList(DB.Hotels);

            for (int i = 0; i < hotels.Count; i++)
            {
                int hotelId = hotels.ElementAt(i).Id;

                hotels.ElementAt(i).Reviews = DB.HotelReviews.Where(r => r.HotelID == hotelId).ToList();
                hotels.ElementAt(i).Images = DB.HotelImages.Where(r => r.HotelID == hotelId).ToList();
            }

            int counter = 0;

            for (int i = 0; i < Math.Ceiling((decimal)hotels.Count / 4); i++)
            {
                List<hotelClass> arr = new List<hotelClass>();

                for (int j = 0; j < 4; j++)
                {
                    try
                    {
                        arr.Add(hotels.ElementAt(counter));
                        counter++;
                    }
                    catch
                    {
                        break;
                    }

                }

                hotelsarr.Add(arr);
            }



            if (type == 1)
            {
                return View(hotelsarr);
            }
            else
            {
                return Json(hotelsarr, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet]
        public ActionResult booking()
        {
            return View();
        }

        //sort
        public ActionResult Sorting(int id)
        {

            List<hotelClass> hotels = hotelClass.ConvertToList(DB.Hotels);

            for (int i = 0; i < hotels.Count; i++)
            {
                int hotelId = hotels.ElementAt(i).Id;

                hotels.ElementAt(i).Reviews = DB.HotelReviews.Where(r => r.HotelID == hotelId).ToList();
                hotels.ElementAt(i).Images = DB.HotelImages.Where(r => r.HotelID == hotelId).ToList();
            }

            //for ordering hotels 

            List<hotelClass> OrderdHotels;

            if (id == 1)
            {
                OrderdHotels= hotels.OrderBy(e => e.Exeprience).ToList();
            }
            else if (id == 2)
            {
               OrderdHotels= hotels.OrderByDescending(e => e.Exeprience).ToList();
            }
            else if (id == 3)
            {
               OrderdHotels= hotels.OrderBy(r => r.Rate).ToList();
            }
            else
            {
               OrderdHotels= hotels.OrderByDescending(r => r.Rate).ToList();

            }


            int counter = 0;

            for (int i = 0; i < Math.Ceiling((decimal)OrderdHotels.Count / 4); i++)
            {
                List<hotelClass> arr = new List<hotelClass>();

                for (int j = 0; j < 4; j++)
                {
                    try
                    {
                        arr.Add(OrderdHotels.ElementAt(counter));
                        counter++;
                    }
                    catch
                    {
                        break;
                    }

                }

                hotelsarr.Add(arr);
            }


            return View("GetHotels", hotelsarr);


        }


    }

    }
