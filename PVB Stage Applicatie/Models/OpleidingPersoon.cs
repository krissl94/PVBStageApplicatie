//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PVB_Stage_Applicatie.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class OpleidingPersoon
    {
        public int OpleidingID { get; set; }
        public int PersoonsgegevensID { get; set; }
        public System.DateTime BeginDatum { get; set; }
        public System.DateTime EindDatum { get; set; }
    
        public virtual Opleiding Opleiding { get; set; }
        public virtual Persoonsgegevens Persoonsgegevens { get; set; }
    }
}
