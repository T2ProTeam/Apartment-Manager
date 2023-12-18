using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace AM.UI.Utilities
{
    public class ControlObject
    {
        public ControlObject()
        { }

        public Button FindChildButton(DependencyObject parent, string name)
        {
            if (parent is Button button && button.Name == name)
                return button;

            int childCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childCount; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                Button foundButton = FindChildButton(child, name);
                if (foundButton != null)
                    return foundButton;
            }

            return null;
        }

        public bool EnableLeft(Button a)
        {
            if (a.Name.Equals("button1"))
                return false;
            return true;
        }

        public bool EnableRight(Button a, string last)
        {
            if (a.Name.Equals(last))
                return false;
            return true;
        }
    }
}