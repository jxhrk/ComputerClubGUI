using System;

namespace CompClubGUICore.Views.Scaling
{
    public interface IScalable : IDisposable
    {
        public void ApplyScaling(double scalingFactor);
    }
}