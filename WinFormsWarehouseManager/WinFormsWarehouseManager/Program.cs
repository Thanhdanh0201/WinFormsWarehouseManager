using System;
using System.Windows.Forms;
using WinFormsWarehouseManager.Services;
using WinFormsWarehouseManager.db;
using WinFormsWarehouseManager.Models;
using WinFormsWarehouseManager.Helpers;
using WinFormsWarehouseManager.Forms;
namespace WinFormsWarehouseManager
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.Run(new LoginForm());
        }
    }
}
