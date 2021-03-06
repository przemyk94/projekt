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
    
    public partial class Competitions
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Competitions()
        {
            this.EventCompetitions = new HashSet<EventCompetitions>();
            this.StartOrders1 = new HashSet<StartOrders>();
        }
    
        public long id { get; set; }
        public string CompetitionName { get; set; }
        public Nullable<long> StartOrder { get; set; }
        public Nullable<long> Scores { get; set; }
        public Nullable<long> BasicPoints { get; set; }
    
        public virtual Scores Scores1 { get; set; }
        public virtual StartOrders StartOrders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EventCompetitions> EventCompetitions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StartOrders> StartOrders1 { get; set; }
    }
}
