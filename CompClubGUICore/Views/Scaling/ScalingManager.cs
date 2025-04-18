using Avalonia;
using Avalonia.Controls;
using Avalonia.LogicalTree;

namespace CompClubGUICore.Views.Scaling
{
    // reference for scaling logic: https://github.com/frederik-hoeft/pmdbs2X.UI.Scaling
    public class ScalingManager
    {
        public double CurrentScaling { get; private set; } = 1d;

        public Dictionary<Type, HashSet<ScalableObject>> Bindings { get; } = [];

        public ScalingManager(Control window)
        {
            Queue<IEnumerable<ILogical>> logicals = new();
            logicals.Enqueue(window.GetLogicalChildren());
            RegisterControls(logicals, window.GetType());
        }

        public void SetScaling(double scalingFactor)
        {
            foreach (HashSet<ScalableObject> bindingContext in Bindings.Values)
            {
                SetScaling(scalingFactor, bindingContext);
            }
        }

        public void SetScaling(double scalingFactor, HashSet<ScalableObject> bindingContext)
        {
            foreach (ScalableObject scalableObject in bindingContext)
            {
                scalableObject.ApplyScaling(scalingFactor);
            }
            CurrentScaling = scalingFactor;
        }

        
        public void RegisterControls(ILogical root, Type key)
        {
            Queue<IEnumerable<ILogical>> logicals = new();
            logicals.Enqueue([root]);
            RegisterControls(logicals, key);
        }

        public void RegisterControls(Queue<IEnumerable<ILogical>> logicals, Type key)
        {
            if (!Bindings.TryGetValue(key, out HashSet<ScalableObject>? bindingContext))
            {
                bindingContext = [];
                Bindings.Add(key, bindingContext);
            }
            RegisterControls(logicals, bindingContext);
        }

        public static void RegisterControls(Queue<IEnumerable<ILogical>> logicals, HashSet<ScalableObject> bindingContext)
        {
            while (logicals.Count > 0)
            {
                foreach (ILogical child in logicals.Dequeue())
                {
                    logicals.Enqueue(child.GetLogicalChildren());
                    if (child is AvaloniaObject avaloniaObject)
                    {
                        bindingContext.Add(new ScalableObject(avaloniaObject));
                    }
                }
            }
        }

        public IScalable? GetScalable(Control control, string name)
        {
            HashSet<ScalableObject> objects = Bindings.FirstOrDefault().Value;
            ScalableObject? scalableObj = objects.Where(o => o.AvaloniaObj == control).FirstOrDefault();
            if (scalableObj == null) return null;
            return scalableObj.Bindings.Where(o => o.Key == name).FirstOrDefault().Value;
        }

        public void SetScale<T>(Scalable<T> scalable, T value)
        {
            scalable.DefaultValue = value;
            scalable.ApplyScaling(CurrentScaling);
        }
    }
}
