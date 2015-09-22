﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
#endregion

namespace eRestaurantSystem.DAL.Entities
{
    public class Reservation
    {
        [Key]
        public int ReservationID { get; set; }
        public string CustomerName { get; set; }
        public DateTime ReservationDate {get;set;}
        public int NumberInParty { get; set; }
        public string ContactPhone { get; set; }
        public string ReservationStatus { get; set; }
        public string EventCode { get; set; }

        //Navigation properties 
        public virtual SpecialEvent Event { get; set; }

        //the Reservation table (SQL) is a many to many
        //relationship to the Tables table (SQL)

        //sql solves this problem by having an associate table
        //that has a compound primary key created from Reservations
        //and Tables.

        //We will Not be creating a entity for this associate table.
        //Instead we will create on overload may in our DbContext class

        //However, we can still create the virtual navigation property to 
        //accomondate this relationship

        public virtual ICollection<Table> Tables { get; set; }

    }
}
