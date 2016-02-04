namespace PlatformTestApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CS401_DB.Customers")]
    public partial class Customer
    {
        public int CustomerID { get; set; }

        [StringLength(200)]
        public string CustomerName { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        [StringLength(30)]
        public string Province { get; set; }

        [StringLength(7)]
        public string PostalCode { get; set; }
    }
}
