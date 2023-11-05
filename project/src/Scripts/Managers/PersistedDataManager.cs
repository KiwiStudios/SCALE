using SCALE.GameData;

namespace SCALE.Scripts.Managers;

public partial class PersistedDataManager : Node
{
    public override void _EnterTree()
    {
        var eventBus = this.GetEventBus();
        eventBus.OnDataPropertyChanged += OnDataPropertyChanged;

    }

    private void OnDataPropertyChanged(string propertyname)
    {
        if (Root.IsGameInitialized)
        {
            Data.Save();
        }
    }
}