using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mail.Engine.Service.Shared.Models
{
    public class MessageLogModel
    {
        public System.Int64 MessageLogId { get; set; }
        public System.Int64 MessageLogHeaderId { get; set; }
        public System.String FromField { get; set; }
        public System.String FromName { get; set; }
        public System.String ToField { get; set; }
        public System.String CcField { get; set; }
        public System.String BccField { get; set; }
        public System.String Subject { get; set; }
        public System.String Body { get; set; }
        public System.Int32 MessageLogTypeId { get; set; }
        public System.Int32 MessageLogStatusId { get; set; }
        public System.DateTime? DateSent { get; set; }
        public System.String StatusMessage { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.Int32 CreatedBy { get; set; }
        public System.String MessageLogTypeName { get; set; }
        public System.String MessageLogTypeCode { get; set; }
        public System.String MessageLogStatusName { get; set; }
        public System.String MessageLogStatusCode { get; set; }
        public System.String SmtpUserName { get; set; }
        public System.Int32? SmtpConfigurationId { get; set; }
        public System.String Smtp { get; set; }
        public System.Boolean? SSL { get; set; }
        public System.Int32? Port { get; set; }
        public System.String SmtpEmailAddress { get; set; }
        public System.String SmtpPassword { get; set; }
        public System.Boolean? PickupDirectoryFromIis { get; set; }
        public System.String PickupDirectoryLocation { get; set; }
    }
}