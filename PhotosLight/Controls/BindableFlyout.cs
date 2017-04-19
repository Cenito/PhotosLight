using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace PhotosLight.Controls
{
    public class BindableFlyout : Flyout
    {
        public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register(
          "IsOpen",
          typeof(bool),
          typeof(BindableFlyout),
          new PropertyMetadata(null, OnOpenPropertyChanged));       

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        public static readonly DependencyProperty ParentProperty = DependencyProperty.Register(
         "Parent",
         typeof(FrameworkElement),
         typeof(BindableFlyout),
         new PropertyMetadata(null));

        public FrameworkElement Parent
        {
            get { return (FrameworkElement)GetValue(ParentProperty); }
            set { SetValue(ParentProperty, value); }
        }

        private static void OnOpenPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var bindableFlyout = d as BindableFlyout;
            if (bindableFlyout == null)
                return;

            if ((bool)e.NewValue)
            {
                bindableFlyout.Closed += OnFlyoutClosed;
                bindableFlyout.ShowAt(bindableFlyout.Parent);
            }
            else
            {
                bindableFlyout.Closed -= OnFlyoutClosed;
                bindableFlyout.Hide();
            }
        }
        private static void OnFlyoutClosed(object sender, object e)
        {
            // When the flyout is closed, sets its IsOpen attached property to false.
            var bindableFlyout = sender as BindableFlyout;
            bindableFlyout.IsOpen = false;
        }
    }
}

