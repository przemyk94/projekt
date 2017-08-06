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
    
    public partial class StartOrders
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StartOrders()
        {
            this.Competitions = new HashSet<Competitions>();
        }
    
        public long id { get; set; }
        public Nullable<int> Number { get; set; }
        public Nullable<int> Heat { get; set; }
        public Nullable<int> Track { get; set; }
        public Nullable<long> FKevent { get; set; }
        public Nullable<long> FKeventParticipant { get; set; }
        public Nullable<long> FKcompetition { get; set; }
        public string EntryTime { get; set; }
        public Nullable<int> EntryScore { get; set; }
        public string FinalTime { get; set; }
        public Nullable<int> FinalScore { get; set; }
        public Nullable<long> FKParticipant { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Competitions> Competitions { get; set; }
        public virtual Competitions Competitions1 { get; set; }
        public virtual EventParticipants EventParticipants { get; set; }
        public virtual Events Events { get; set; }
        public virtual Participant Participant1 { get; set; }
    }
}
