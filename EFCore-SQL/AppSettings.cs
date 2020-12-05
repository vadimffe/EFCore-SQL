using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore_SQL
{
    [Table("Appsettings")]
    public class Appsettings
    {
        //public int Id { get; set; }
        public string SettingKey { get; set; }
        public string Value { get; set; }
    }

}
