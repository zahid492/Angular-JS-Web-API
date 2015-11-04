namespace OdataAngular
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Class")]
    public partial class Class
    {
        public Class()
        {
            Basic_Information = new HashSet<Basic_Information>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Class1 { get; set; }

        public virtual ICollection<Basic_Information> Basic_Information { get; set; }
    }
}
