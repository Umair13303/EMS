using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace office360.Models.General
{
    public class UserRightMenu
    {
        public class MenuViewModel
        {
            public string Menu { get; set; }
            public List<ModuleViewModel> SubMenu { get; set; } = new List<ModuleViewModel>();
        }
        public class ModuleViewModel
        {
            public string SubMenu { get; set; }
            public List<int> Id { get; set; } = new List<int>();
            public List<string> DisplayName { get; set; } = new List<string>();
            public List<string> Description { get; set; } = new List<string>();
            public List<string> DB_OperationType { get; set; } = new List<string>();
        }
    }
}