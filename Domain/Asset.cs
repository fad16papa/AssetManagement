using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Domain
{
    public class Asset
    {
        public Guid Id { get; set; }
        public string HostName { get; set; }
        public string AssetNo { get; set; }
        public string SerialNo { get; set; }
        public string ExpressCode { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string Location { get; set; }
        public string IsAvailable { get; set; }
        public string IsAssinged { get; set; }
        public string Remarks { get; set; }
        public DateTime IssuedOn { get; set; }
        public DateTime ReturnedOn { get; set; }
        public virtual IList<UserAssets> UserAssets { get; set; }
    }
}