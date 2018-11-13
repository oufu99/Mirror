using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Movies
    {
        [Key]
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string MovieName { get; set; }
        public int EpisodeID { get; set; }
        public int IsDelete { get; set; }

        public string FilePath { get; set; }
    }
}
