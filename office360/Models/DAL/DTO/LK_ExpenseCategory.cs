using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace office360.Models.DAL.DTO
{
    public class LK_ExpenseCategory
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; } = true;
    }
}