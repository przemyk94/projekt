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
    
    public partial class Participant
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Participant()
        {
            this.EventParticipants = new HashSet<EventParticipants>();
            this.Scores = new HashSet<Scores>();
            this.StartOrders = new HashSet<StartOrders>();
        }
    
        public long id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public long Sex { get; set; }
        public int BirthYear { get; set; }
        public Nullable<int> BirthMonth { get; set; }
        public Nullable<int> BirthDay { get; set; }
        public Nullable<long> Club { get; set; }
        public string City { get; set; }
        public Nullable<long> Nationality { get; set; }
        public string RegTime { get; set; }
    
        public virtual Club Club1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EventParticipants> EventParticipants { get; set; }
        public virtual Nation Nation { get; set; }
        public virtual Sex Sex1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Scores> Scores { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StartOrders> StartOrders { get; set; }
    }
}
