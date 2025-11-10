using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace CFBROrders.Web.Areas.Identity.Data
{
    public class ApplicationUser 
    {
        public int id { get; set; }
        public string uname { get; set; }
    }
}