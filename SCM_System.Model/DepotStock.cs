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
    
    public partial class DepotStock : BaseModel
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int DSID { get; set; }
        public string DepotID { get; set; }
        public string ProID { get; set; }
        public Nullable<int> DSAmount { get; set; }
        public Nullable<decimal> DSPrice { get; set; }
    }
}
