//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IMS_Final_Version2.Models.dbModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblRequisitionSlip
    {
        public tblRequisitionSlip()
        {
            this.tblRightPages = new HashSet<tblRightPage>();
        }
    
        public int RequisitionSlipId { get; set; }
        public Nullable<System.DateTime> RequisitionDate { get; set; }
        public string IssuingPurpose { get; set; }
        public string AllInventoryNameWithAmount { get; set; }
        public Nullable<int> ApplicantId { get; set; }
        public Nullable<int> RecommenderId { get; set; }
        public Nullable<int> DirectorId { get; set; }
        public string RecommenderResponse { get; set; }
        public string DirectorResponse { get; set; }
        public string ApplicationStatus { get; set; }
        public string ItemsId { get; set; }
        public Nullable<int> NotificationId { get; set; }
    
        public virtual tblNotification tblNotification { get; set; }
        public virtual tblUser tblUser { get; set; }
        public virtual tblUser tblUser1 { get; set; }
        public virtual tblUser tblUser2 { get; set; }
        public virtual ICollection<tblRightPage> tblRightPages { get; set; }
    }
}
