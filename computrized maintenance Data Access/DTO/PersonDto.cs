using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace computrized_maintenance_Data_Access.DTO
{
    public class PersonDto
    {
        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("-------------------");
            sb.AppendLine($"ID     :{this.PersonID} ");
            sb.AppendLine($"First Name :{this.FirstName}");
            sb.AppendLine($"Last  Name :{this.LastName}");
            sb.AppendLine($"Birth Day  :{this.BirthDay.ToShortDateString()}");
            sb.AppendLine($"Addrees    :{this.Addrees}");
            sb.AppendLine($"Email      :{this.Email}");
            sb.AppendLine($"Phone      :{this.Phone}");
            sb.AppendLine("-------------------");

            return sb.ToString();
        }

        public int? PersonID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public DateTime BirthDay { get; set; }
        public string? Addrees { get; set; }

    }
}
