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
    
    public partial class EventParticipants
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EventParticipants()
        {
            this.StartOrders = new HashSet<StartOrders>();
        }
    
        public long id { get; set; }
        public long EventID { get; set; }
        public long ParticipantID { get; set; }
    
        public virtual Participant Participant { get; set; }
        public virtual Events Events { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StartOrders> StartOrders { get; set; }
    }
}
