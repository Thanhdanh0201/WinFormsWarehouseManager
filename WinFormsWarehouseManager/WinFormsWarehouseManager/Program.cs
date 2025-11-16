using System;
using System.Windows.Forms;

namespace WinFormsWarehouseManager
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.Run(new LocationAddingForm());
        }
    }
}
