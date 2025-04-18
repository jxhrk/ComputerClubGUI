using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Media.Transformation;
namespace CompClubGUICore.Views.Scaling
{
    public class ScalableObject
    {
        public Dictionary<string, IScalable> Bindings { get; } = new Dictionary<string, IScalable>();

        public AvaloniaObject AvaloniaObj;

        public ScalableObject(AvaloniaObject avaloniaObject)
        {
            AvaloniaObj = avaloniaObject;
            if (avaloniaObject is TextBlock textBlock)
            {
                Register(TextBlock.FontSizeProperty, textBlock.FontSize);
            }
            if (avaloniaObject is TemplatedControl templatedControl)
            {
                Register(TemplatedControl.FontSizeProperty, templatedControl.FontSize);
            }
            if (avaloniaObject is Border border)
            {
                Register(Border.CornerRadiusProperty, border.CornerRadius);
            }
            if (avaloniaObject is Decorator decorator)
            {
                Register(Decorator.PaddingProperty, decorator.Padding);
            }
            if (avaloniaObject is Layoutable layoutable)
            {
                Register(Layoutable.MaxWidthProperty, layoutable.MaxWidth);
                Register(Layoutable.WidthProperty, layoutable.Width);
                Register(Layoutable.MaxHeightProperty, layoutable.MaxHeight);
                Register(Layoutable.HeightProperty, layoutable.Height);
                Register(Layoutable.MarginProperty, layoutable.Margin);
            }
            if (avaloniaObject is Button button)
            {
                Register(TemplatedControl.CornerRadiusProperty, button.CornerRadius);
                Register(TemplatedControl.PaddingProperty, button.Padding);
            }
            if (avaloniaObject is TextBox textBox)
            {
                Register(TemplatedControl.CornerRadiusProperty, textBox.CornerRadius);
            }
            if (avaloniaObject is StackPanel stackPanel)
            {
                Register(StackPanel.SpacingProperty, stackPanel.Spacing);
            }
        }

        public Scalable<double> Register(AvaloniaProperty<double> avaloniaProperty, double defaultValue)
        {
            return Register<ScalableDouble, double>(avaloniaProperty, defaultValue);
        }

        public Scalable<CornerRadius> Register(AvaloniaProperty<CornerRadius> avaloniaProperty, CornerRadius defaultValue)
        {
            return Register<ScalableCornerRadius, CornerRadius>(avaloniaProperty, defaultValue);
        }

        public Scalable<Thickness> Register(AvaloniaProperty<Thickness> avaloniaProperty, Thickness defaultValue)
        {
            return Register<ScalableThickness, Thickness>(avaloniaProperty, defaultValue);
        }

        public Scalable<ITransform> Register(AvaloniaProperty<ITransform> avaloniaProperty, ITransform defaultValue)
        {
            return Register<ScalableRenderTransform, ITransform>(avaloniaProperty, defaultValue);
        }

        private T1 Register<T1, T2>(AvaloniaProperty<T2> avaloniaProperty, T2 defaultValue) where T1 : Scalable<T2>, new()
        {
            string key = avaloniaProperty.Name;
            if (!Bindings.ContainsKey(key))
            {
                T1 newScalable = new T1();
                newScalable.Initialize(AvaloniaObj, avaloniaProperty, defaultValue);
                Bindings.Add(key, newScalable);
                return newScalable;
            }
            Bindings.TryGetValue(key, out IScalable value);
            return value as T1;
        }

        public void ApplyScaling(double scalingFactor)
        {
            foreach (IScalable binding in Bindings.Values)
            {
                binding.ApplyScaling(scalingFactor);
            }
        }

        private sealed class ScalableDouble : Scalable<double>
        {
            public ScalableDouble()
            {
            }

            public override void ApplyScaling(double scalingFactor)
            {
                SetValue(Math.Abs(DefaultValue * scalingFactor));
            }
        }

        private sealed class ScalableThickness : Scalable<Thickness>
        {
            public ScalableThickness()
            {
            }

            public override void ApplyScaling(double scalingFactor)
            {
                SetValue(new Thickness(DefaultValue.Left * scalingFactor, DefaultValue.Top * scalingFactor, DefaultValue.Right * scalingFactor, DefaultValue.Bottom * scalingFactor));
            }
        }

        private sealed class ScalableCornerRadius : Scalable<CornerRadius>
        {
            public ScalableCornerRadius()
            {
            }

            public override void ApplyScaling(double scalingFactor)
            {
                CornerRadius cornerRadius = new CornerRadius(DefaultValue.TopLeft * scalingFactor, DefaultValue.TopRight * scalingFactor, DefaultValue.BottomRight * scalingFactor, DefaultValue.BottomLeft * scalingFactor);
                SetValue(cornerRadius);
            }
        }

        private sealed class ScalableRenderTransform : Scalable<ITransform>
        {
            public ScalableRenderTransform()
            {
            }

            public override void ApplyScaling(double scalingFactor)
            {
                int value = Convert.ToInt32(DefaultValue.Value.M31 * scalingFactor);
                SetValue(TransformOperations.Parse($"translateX({value}px)"));
            }
        }
    }
}
