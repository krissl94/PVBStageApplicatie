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
    
    public partial class Periode
    {
        public Periode()
        {
            this.Stage = new HashSet<Stage>();
        }
    
        public int Periode1 { get; set; }
        public System.DateTime Begindatum { get; set; }
        public System.DateTime Einddatum { get; set; }
    
        public virtual ICollection<Stage> Stage { get; set; }
    }
}
