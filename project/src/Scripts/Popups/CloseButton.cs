using SCALE.Enums;

namespace SCALE.Scripts.Popups;

public partial class CloseButton : Button
{
	[Export] private EPopupNames _popupName;
	
	public override void _Pressed()
	{
		var eventBus = this.GetEventBus();
		eventBus.EmitOnPopupClose(_popupName);
	}
}
