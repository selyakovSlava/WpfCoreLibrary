﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace WpfCoreLibrary.Behaviors
{
    public class CloseMenuItemBehavior : Behavior<MenuItem>
    {
        protected override void OnAttached()
        {
            AssociatedObject.Click += AssociatedObject_Click;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Click -= AssociatedObject_Click;
        }

        private void AssociatedObject_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
