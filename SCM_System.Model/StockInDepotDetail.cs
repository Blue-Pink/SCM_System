//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace SCM_System.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class StockInDepotDetail : BaseModel
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int SIDDID { get; set; }
        public string ProID { get; set; }
        public string SIDID { get; set; }
        public Nullable<int> SIDDAmount { get; set; }
        public Nullable<decimal> SIDDPrice { get; set; }
        public string SIDDDesc { get; set; }
    }
}
