using Avalonia;

namespace CompClubGUICore.Views.Scaling
{
    public abstract class Scalable<T> : IScalable
    {
        public T DefaultValue { get; set; }

        private bool isInitialized = false;

        AvaloniaObject avaloniaObject;

        AvaloniaProperty<T> property;

        private protected Scalable()
        {
        }

        public virtual void Initialize(AvaloniaObject avaloniaObject, AvaloniaProperty<T> avaloniaProperty, T defaultValue)
        {
            if (!isInitialized)
            {
                this.avaloniaObject = avaloniaObject;
                this.property = avaloniaProperty;
                DefaultValue = defaultValue;
                isInitialized = true;
            }
        }

        public abstract void ApplyScaling(double scalingFactor);

        private protected void SetValue(T value)
        {
            avaloniaObject.SetValue(property, value);
        }

        public void Dispose()
        {

        }
    }
}