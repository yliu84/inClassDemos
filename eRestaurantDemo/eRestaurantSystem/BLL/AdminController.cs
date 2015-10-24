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
        #region Queries

        
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
        #endregion

        #region Add, Update, Delete of CRUD for CQRS
        [DataObjectMethod(DataObjectMethodType.Insert,false)]
        public void SpecialEvent_Add(SpecialEvent item)
        {   
            using (eRestaurantContext context = new eRestaurantContext())
            {
                //these methods are execute using an instance level item
                //set up a instance pointer and initialize to null

                SpecialEvent added = null;
                //set up the command to execute the add
                added = context.SpecialEvents.Add(item);
                //command is not executed until it is actually saved
                context.SaveChanges();
            }
            
        }

        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void SpecialEvent_Update(SpecialEvent item)
        {
            using (eRestaurantContext context = new eRestaurantContext())
            {
                //indicate the updating item instance
                //alter the modified satatus flag for this instance

                context.Entry<SpecialEvent>(context.SpecialEvents.Attach(item)).State = System.Data.Entity.EntityState.Modified;
                //command is not executed until it is actually saved
                context.SaveChanges();
            }

        }

        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public void SpecialEvent_Delete(SpecialEvent item)
        {
            using (eRestaurantContext context = new eRestaurantContext())
            {
                //lookup the instance and record if found (set pointer to instance)

                SpecialEvent existing = context.SpecialEvents.Find(item.EventCode);

                //set up the command to execute the delete
                context.SpecialEvents.Remove(existing);
                //command is not executed until it is actually saved
                context.SaveChanges();
            }

        }
        #endregion

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Waiter> Waiters_List()
        {
            //connect to our DbContext class in the DAL
            //create an instance of the class
            //we will use a transaction to hold our query
            using (var context = new eRestaurantContext())
            {
                //method syntax
                //return context.SpecialEvents.OrderBy(x => x.Description).ToList();

                //Query syntax
                var results = from item in context.Waiters
                              orderby item.LastName, item.FirstName
                              select item;
                return results.ToList(); //none, 1 or more rows
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public Waiter WaiterByID(int waiterid)
        {
            //connect to our DbContext class in the DAL
            //create an instance of the class
            //we will use a transaction to hold our query
            using (var context = new eRestaurantContext())
            {
                //method syntax
                //return context.SpecialEvents.OrderBy(x => x.Description).ToList();

                //Query syntax
                var results = from item in context.Waiters
                              where item.WaiterID == waiterid
                              select item;
                return results.FirstOrDefault(); //one row at most
            }
        }

        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public int Waiters_Add(Waiter item)
        {
            using (eRestaurantContext context = new eRestaurantContext())
            {
                //these methods are execute using an instance level item
                //set up a instance pointer and initialize to null

                Waiter added = null;
                //set up the command to execute the add
                added = context.Waiters.Add(item);
                //command is not executed until it is actually saved
                context.SaveChanges();
                //the waiter instance added contains the newly inserted
                //record to sql including the generated pkey value

                return added.WaiterID;
            }

        }

        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void Waiter_Update(Waiter item)
        {
            using (eRestaurantContext context = new eRestaurantContext())
            {
                //indicate the updating item instance
                //alter the modified satatus flag for this instance

                context.Entry<Waiter>(context.Waiters.Attach(item)).State = System.Data.Entity.EntityState.Modified;
                //command is not executed until it is actually saved
                context.SaveChanges();
            }

        }

        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public void Waiter_Delete(Waiter item)
        {
            using (eRestaurantContext context = new eRestaurantContext())
            {
                //lookup the instance and record if found (set pointer to instance)

                Waiter existing = context.Waiters.Find(item.WaiterID);

                //set up the command to execute the delete
                context.Waiters.Remove(existing);
                //command is not executed until it is actually saved
                context.SaveChanges();
            }

        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<CategoryMenuItems> GetReportCategoryMenuItems()
        {
            using (eRestaurantContext context = new eRestaurantContext())
            {
                var results = from cat in context.Items
                              orderby cat.Category.Description, cat.Description
                              select new CategoryMenuItems
                              {
                                  CategoryDescription = cat.Category.Description,
                                  ItemDescription = cat.Description,
                                  Price = cat.CurrentPrice,
                                  Calories = cat.Calories,
                                  Comment = cat.Comment
                              };

                return results.ToList(); // this was .Dump() in Linqpad
            }
        }


        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<WaiterBilling> GetWaiterBillingReport()
        {
            using (eRestaurantContext context = new eRestaurantContext())
            {
                var results = from abillrow in context.Bills
                              where abillrow.BillDate.Month == 5
                              orderby abillrow.BillDate,
                                      abillrow.Waiter.LastName,
                                      abillrow.Waiter.FirstName
                              select new WaiterBilling()
                              {
                                  BillDate = abillrow.BillDate.Year + "/" +
                                             abillrow.BillDate.Month + "/" +
                                             abillrow.BillDate.Day,

                                  WaiterName = abillrow.Waiter.LastName + "," + abillrow.Waiter.FirstName,
                                  BillID = abillrow.BillID,
                                  BillTotal = abillrow.Items.Sum(eachbillitemrow => eachbillitemrow.Quantity * eachbillitemrow.SalePrice),
                                  PartySize = abillrow.NumberInParty,
                                  Contact = abillrow.Reservation.CustomerName
                              };

                return results.ToList(); // this was .Dump() in Linqpad
            }
        }

    }//eof class
}//eof namespace
