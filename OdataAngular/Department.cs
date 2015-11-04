namespace OdataAngular
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Department")]
    public partial class Department
    {
        public Department()
        {
            Basic_Information = new HashSet<Basic_Information>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Department_Name { get; set; }

        public virtual ICollection<Basic_Information> Basic_Information { get; set; }
    }
}
