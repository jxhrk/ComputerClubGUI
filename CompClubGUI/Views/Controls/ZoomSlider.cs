using System;
using Avalonia.Controls;
using Avalonia.Input;

namespace CompClubGUI.Views.Controls
{
    public class ZoomSlider : Slider
    {
        protected override Type StyleKeyOverride { get { return typeof(Slider); } }

        public SettingsView ParentView;

        public ZoomSlider() : base()
        {

        }

        protected override void OnThumbDragCompleted(VectorEventArgs e)
        {
            base.OnThumbDragCompleted(e);
            ParentView.UpdateZoom(Value);
        }
    }
}
