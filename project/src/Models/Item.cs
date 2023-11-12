using SCALE.Enums;

namespace SCALE.Models;

public partial class Item : RefCounted
{
    public Item(EItemNames name, int value)
    {
        Name = name;
        Value = value;
    }

    public string InstanceId { get; } = Guid.NewGuid().ToString("N");
    public EItemNames Name { get; set; }
    public int Value { get; set; }

    public virtual string DisplayName()
    {
        return Name.ToString();
    }
}