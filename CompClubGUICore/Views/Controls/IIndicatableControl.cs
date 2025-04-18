using Avalonia.Animation;
using Avalonia.Media;
using System;
using System.Threading.Tasks;

namespace CompClubGUICore.Views.Controls
{
    internal interface IIndicatableControl
    {
        protected static SolidColorBrush IndicationColor = new(Color.FromRgb(198, 0, 100));
        protected static SolidColorBrush BorderTransparentColor = new(Color.FromArgb(0, 198, 0, 100));

        public async void IndicateRedInternal()
        {
            SetTransitionDuration(0.1);
            SetBrush(IndicationColor);

            await Task.Delay(200);

            SetTransitionDuration(0.2);
            SetBrush(BorderTransparentColor);
        }

        public void StopIndicationInternal()
        {
            SetBrush(BorderTransparentColor);
        }

        private void SetTransitionDuration(double seconds)
        {
            GetTransition().Duration = TimeSpan.FromSeconds(seconds);
        }

        protected abstract void SetBrush(SolidColorBrush brush);

        protected abstract BrushTransition GetTransition();

        public abstract bool ValidateAndGet(out string value);
        
    }
}
