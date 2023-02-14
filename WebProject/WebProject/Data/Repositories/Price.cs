namespace WebProject.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Price
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PriceID { get; set; }

        [Column("Price")]
        public double Price1 { get; set; }

        public DateTime DateModified { get; set; }

        public int GasTypeID { get; set; }

        public int LocationID { get; set; }

        public virtual GasType GasType { get; set; }

        public virtual Location Location { get; set; }
    }
}
