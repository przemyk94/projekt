//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Modern_Pentathlon_Event_Manager
{
    using System;
    using System.Collections.Generic;
    
    public partial class Scores
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Scores()
        {
            this.Competitions = new HashSet<Competitions>();
        }
    
        public long id { get; set; }
        public Nullable<long> Participant { get; set; }
        public string Score_Time { get; set; }
        public Nullable<long> Score_Points { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Competitions> Competitions { get; set; }
        public virtual Participant Participant1 { get; set; }
    }
}
