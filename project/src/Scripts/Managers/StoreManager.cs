namespace SCALE.Scripts.Managers;

public partial class StoreManager : Node
{
    
    private EventBus _eventBus = null!;
    private Storage _storage = null!;
    private Store _store = null!;
    
    public override void _EnterTree()
    {
        base._EnterTree();
        _eventBus = this.GetEventBus();
    }

    public override void _Ready()
    {
        base._Ready();
        _storage = new Storage();
        _store = new Store();
        
        var y = 0;
        foreach (var item in _storage.InStorage)
        {
            var label = new Label();
            label.Text = item.Name.ToString();
            label.Position = new Vector2(0, y);
            AddChild(label);
            y += 50;
        }
    }
}