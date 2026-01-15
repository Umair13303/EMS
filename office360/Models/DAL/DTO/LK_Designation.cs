using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace office360.Models.DAL.DTO
{
    public class LK_Designation
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; } = true;
    }
}