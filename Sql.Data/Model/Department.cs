using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Sql.Data.Model
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public decimal prive { get; set; }

        public string Email { get; set; }
    }
}
