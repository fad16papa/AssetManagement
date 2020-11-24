using System;

namespace AssetManagementWeb.Models.DTO
{
    public class AssetsDTO
    {
        public Guid Id { get; set; }
        public string HostName { get; set; }
        public string SerialNo { get; set; }
        public string ExpressCode { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string Location { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime IssuedOn { get; set; }
        public DateTime ReturnedOn { get; set; }

    }
}