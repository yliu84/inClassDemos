using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using eRestaurantSystem.DAL;
using eRestaurantSystem.DAL.Entities;
using eRestaurantSystem.DAL.DTOs;
using eRestaurantSystem.DAL.POCOs;
using System.ComponentModel; // Object Data Source
#endregion

namespace eRestaurantSystem.BLL
{
    [DataObject]
    public class AdminController
    {
        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<SpecialEvent> SpecialEvents_List()
        {
            //connect to our DbContext class in the DAL
            //create an instance of the class
            //we will use a transaction to hold our query
            using (var context = new eRestaurantContext())
            {
                //method syntax
                //return context.SpecialEvents.OrderBy(x => x.Description).ToList();

                //Query syntax
                var results = from item in context.SpecialEvents
                             orderby item.Description
                             select item;
                return results.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<Reservation> GetReservationByEventCode(string eventcode)
        {
            using (var context = new eRestaurantContext())
            {
                //Query syntax
                var results = from item in context.Reservations
                              where item.EventCode.Equals(eventcode)
                             orderby item.CustomerName, item.ReservationDate
                             select item;
                return results.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<ReservationByDate> GetReservationByDate(string reservationDate)
        {
            using (var context = new eRestaurantContext())
            {
                //Linq is not very playful or cooperation with DateTime

                //Extract the year, months,and day ourselves out of the passed parameter value
                int theYear = (DateTime.Parse(reservationDate).Year);
                int theMonth = (DateTime.Parse(reservationDate).Month);
                int theDay = (DateTime.Parse(reservationDate).Day);

                var results = from eventItem in context.SpecialEvents
                              orderby eventItem.Description
                              select new ReservationByDate() //a new instance for each specialevent row on the table
                              {
                                  Description = eventItem.Description,
                                  Reservations = from row in eventItem.Reservations
                                                 where row.ReservationDate.Year == theYear
                                                 && row.ReservationDate.Month == theMonth
                                                 && row.ReservationDate.Day == theDay
                                                 select new ReservationDetail() // a new for each reservation of a particular specialevent code
                                                 {
                                                     CustomerName = row.CustomerName,
                                                     ReservationDate = row.ReservationDate,
                                                     NumberInParty = row.NumberInParty,
                                                     ContactPhone = row.ContactPhone,
                                                     ReservationStatus = row.ReservationStatus
                                                 }
                              };
                return results.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<MenuCategoryItems> MenuCategoryItems_List()
        {
            using (var context = new eRestaurantContext())
            {
                var results = from menuitem in context.MenuCategories
                              orderby menuitem.Description
                              select new MenuCategoryItems()
                              {
                                  Description = menuitem.Description,
                                  MenuItems = from row in menuitem.MenuItems
                              
                                                 select new MenuItem() 
                                                 {
                                                     Description = row.Description,
                                                     Price = row.CurrentPrice,
                                                     Calories = row.Calories,
                                                     Comment = row.Comment
                                                 }
                              };
                return results.ToList();
            }
        }
    }
}
