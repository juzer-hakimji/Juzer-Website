//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BusinessEntities.Entities.Entity_Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class WebsiteErrorLog
    {
        public int LogId { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string MethodName { get; set; }
        public string TypeName { get; set; }
        public System.DateTime Date { get; set; }
        public System.TimeSpan Time { get; set; }
    }
}