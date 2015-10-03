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
    public class Table
    {
        [Key]
        public int TableID { get; set; }
        [Required,Range(1,25)]
        public byte TableNumber { get; set; } //tinyint in sql
        public bool Smoking { get; set; }
        [Required]
        public int Capacity { get; set; }
        public bool Avaiable { get; set; }

        //Navigation Properties
        //the Reservation table (SQL) is a many to many
        //relationship to the Tables table (SQL)

        //sql solves this problem by having an associate table
        //that has a compound primary key created from Reservations
        //and Tables.

        //We will Not be creating a entity for this associate table.
        //Instead we will create on overload may in our DbContext class

        //However, we can still create the virtual navigation property to 
        //accomondate this relationship

        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<Bill> Bills { get; set; }

        public Table()
        {
            Avaiable = true;
            Smoking = false;
        }

    }
}
