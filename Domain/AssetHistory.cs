using System;

namespace Domain
{
    public class AssetHistory
    {
        public string AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }
        public Guid AssetId { get; set; }
        public virtual Asset Asset { get; set; }
    }
}