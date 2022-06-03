using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SerbianRailways.utility
{
    public static class Commands
    {
        public static RoutedUICommand TrainsCRUDCommand = new RoutedUICommand(
                "Upravljanje vozovima", "TrainsCRUDCommand", typeof(RoutedCommand), new InputGestureCollection()
                {
                    new KeyGesture(Key.V,ModifierKeys.Control)
                });
            
    }
}
