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
    
    public partial class V_OutDepot
    {
        public int IODID { get; set; }
        public string DepotID { get; set; }
        public Nullable<int> IODType { get; set; }
        public string IODNum { get; set; }
        public Nullable<System.DateTime> IODDate { get; set; }
        public Nullable<int> IODUser { get; set; }
        public string IODDesc { get; set; }
        public string IODLend { get; set; }
        public string IODCus { get; set; }
        public string DepotName { get; set; }
        public string UsersName { get; set; }
        public string CusName { get; set; }
        public decimal SumMoney { get; set; }
    }
}
