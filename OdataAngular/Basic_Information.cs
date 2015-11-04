namespace OdataAngular
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Basic_Information
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Phone Required")]
        [StringLength(50)]
        public string Phone { get; set; }
        public string ImageURL { get; set; }
        public int Class_id { get; set; }

        public int Department_id { get; set; }

        public virtual Class Class { get; set; }

        public virtual Department Department { get; set; }
    }
}
