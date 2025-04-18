using System;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Media;

namespace CompClubGUICore.Views.Controls
{
    public class NavTextBox : TextBox, IIndicatableControl
    {
        protected override Type StyleKeyOverride { get { return typeof(TextBox); } }

        public int MinCount = 1;

        BrushTransition IndicationTransition;

        public NavTextBox() : base()
        {
            NavigationControlUtils.Bind(this);
            StopIndication();
            IndicationTransition = new BrushTransition()
            {
                Property = BorderBrushProperty
            };
            Transitions = [IndicationTransition];
        }

        public void StopIndication()
        {
            (this as IIndicatableControl).StopIndicationInternal();
        }
        public void IndicateRed()
        {
            (this as IIndicatableControl).IndicateRedInternal();
        }

        public bool ValidateAndGet(out string value)
        {
            bool isValid = Text != null && Text.Length >= MinCount;
            if (!isValid) IndicateRed();
            else StopIndication();
            value = Text;
            return isValid;
        }

        void IIndicatableControl.SetBrush(SolidColorBrush brush)
        {
            BorderBrush = brush;
        }

        BrushTransition IIndicatableControl.GetTransition()
        {
            return IndicationTransition;
        }
    }
}
