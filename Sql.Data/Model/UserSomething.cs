using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Sql.Data.Model
{
    [Table("Test_Table")]
    public class UserSomething
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }  

        public string Autor { get; set; }

        public string Email { get; set; }

    }
}
