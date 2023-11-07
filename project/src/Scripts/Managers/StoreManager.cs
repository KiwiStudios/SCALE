namespace SCALE.Scripts.Managers;

public partial class StoreManager : Node
{
    
    private EventBus _eventBus = null!;
    public Storage Storage = null!;
    public Store Store = null!;
    
    public override void _EnterTree()
    {
        base._EnterTree();
        _eventBus = this.GetEventBus();
    }

    public override void _Ready()
    {
        base._Ready();
        Storage = new Storage();
        Store = new Store();
        Store.items = Storage.InStorage;
        var y = 0;
        foreach (var item in Storage.InStorage)
        {
            var label = new Label();
            label.Text = item.Name.ToString();
            label.Position = new Vector2(0, y);
            AddChild(label);
            y += 50;
        }
    }
}