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
    
    public partial class PayOffs : BaseModel
    {
        [System.ComponentModel.DataAnnotations.Key]
        public string POID { get; set; }
        public string DepotID { get; set; }
        public Nullable<System.DateTime> PODate { get; set; }
        public Nullable<int> UserID { get; set; }
        public string PODesc { get; set; }
        public Nullable<int> POState { get; set; }
    }
}
