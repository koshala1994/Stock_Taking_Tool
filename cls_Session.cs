using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_Taking_Tool
{
    public static class cls_Session
    {
        public static string Username { get; set; }
        public static string Role { get; set; }

        public static string Cashier_Name { get; set; }
        public static bool IsAdmin
        {
            get { return Role == "ADMIN"; }
        }
    }
}
