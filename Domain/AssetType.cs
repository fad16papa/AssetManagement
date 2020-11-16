using System.ComponentModel;

namespace Domain
{
    public enum AssetType
    {
        [Description("Laptop")]
        Laptop,
        [Description("Server")]
        Server,
        [Description("Monitor")]
        Monitor,
        [Description("Keyboard")]
        Keyboard,
        [Description("Mouse")]
        Mouse,
        [Description("LaptopBag")]
        LaptopBag,
    }
}