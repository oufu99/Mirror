using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Models
{
    public class Teachers
    {
        public Teachers(int Id, string name)
        {

            this.Id = Id; ;
            this.Name = name;
        }
        public Teachers()
        {

        }
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        [Column("Phone")]
        public string Email{ get; set; }
    }
}