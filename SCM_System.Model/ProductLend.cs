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
    
    public partial class ProductLend : BaseModel
    {
        [System.ComponentModel.DataAnnotations.Key]
        public string PPID { get; set; }
        public string PPName { get; set; }
        public string PPCompany { get; set; }
        public string PPMan { get; set; }
        public string PPTel { get; set; }
        public string PPAddress { get; set; }
        public string PPFax { get; set; }
        public string PPEmail { get; set; }
        public string PPUrl { get; set; }
        public string PPBank { get; set; }
        public string PPGoods { get; set; }
        public string PPDesc { get; set; }
    }
}
