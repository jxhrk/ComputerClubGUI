using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;

namespace CompClubGUICore.Views.Controls
{
    public class NavMaskedTextBox : MaskedTextBox, IIndicatableControl
    {
        protected override Type StyleKeyOverride { get { return typeof(TextBox); } }

        private int LastIndex = 0;

        public int MinCount = 1;

        BrushTransition IndicationTransition;

        public NavMaskedTextBox() : base()
        {
            NavigationControlUtils.Bind(this);
            StopIndication();
            IndicationTransition = new BrushTransition()
            {
                Property = BorderBrushProperty
            };
            Transitions = [IndicationTransition];

            GotFocus += NavMaskedTextBox_GotFocus;
            TextChanged += NavMaskedTextBox_TextChanged;
        }

        private void NavMaskedTextBox_TextChanged(object? sender, TextChangedEventArgs e)
        {
            if (MaskProvider == null || Text == null) { return; }

            var en = MaskProvider.EditPositions;
            List<int> indexes = new List<int>();
            while (en.MoveNext())
            {
                indexes.Add((int)en.Current);
            }
            en.Reset();

            for (int i = Text.Length - 1; i >= 0; i--)
            {
                if (indexes.Contains(i) && Text.ElementAt(i) != PromptChar)
                {
                    LastIndex = i + 1;
                    break;
                }
            }
        }

        protected override void OnPointerMoved(PointerEventArgs e)
        {
            
        }

        protected override void OnPointerPressed(PointerPressedEventArgs e)
        {
            base.OnPointerPressed(e);
            SelectionStart = 0;
            SelectionEnd = 0;
            CaretIndex = LastIndex;
        }

        private void NavMaskedTextBox_GotFocus(object? sender, GotFocusEventArgs e)
        {
            CaretIndex = LastIndex;
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
