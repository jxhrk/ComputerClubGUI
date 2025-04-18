using Avalonia;
using Avalonia.Controls;

namespace CompClubGUICore
{
    public class AppBase : Application
    {
        public virtual void SwitchFrame(Control control) { }
    }
}
