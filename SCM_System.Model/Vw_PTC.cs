using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCM_System.Model
{
    public class Vw_PTC
    {
        [Key]
        public int PTID { get; set; }

        public int? PTParentID { get; set; }

        [StringLength(100)]
        public string PTName { get; set; }

        [StringLength(1000)]
        public string PTDes { get; set; }
        [Key]
        [StringLength(50)]
        public string ProID { get; set; }

        //public int? PTID { get; set; }

        public int? PUID { get; set; }

        public int? PCID { get; set; }

        public int? PSID { get; set; }

        [StringLength(100)]
        public string ProName { get; set; }

        [StringLength(100)]
        public string ProJP { get; set; }

        [StringLength(100)]
        public string ProTM { get; set; }

        [StringLength(200)]
        public string ProWorkShop { get; set; }

        public int? ProMax { get; set; }

        public int? ProMin { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ProInPrice { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ProPrice { get; set; }

        [StringLength(2000)]
        public string ProDesc { get; set; }

        [StringLength(6)]
        public string DepotID { get; set; }
        //[Key]
        //public int PCID { get; set; }

        [StringLength(50)]
        public string PCName { get; set; }
    }
}
