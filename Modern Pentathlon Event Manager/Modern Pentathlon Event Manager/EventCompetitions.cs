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
    
    public partial class EventCompetitions
    {
        public long id { get; set; }
        public long EventID { get; set; }
        public long CompetitionID { get; set; }
        public Nullable<long> Order { get; set; }
        public Nullable<short> ScoresDone { get; set; }
    
        public virtual Competitions Competitions { get; set; }
        public virtual Events Events { get; set; }
    }
}