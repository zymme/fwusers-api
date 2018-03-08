using System;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FWUsersAPI.Entities
{
    [Table("users")]
    public class User
    {
        
        public string first_name { get; set; }


        public string last_name { get; set; }
        public DateTime birthdate { get; set; }

        [Key]
        public string username { get; set; }

        public string pwd { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int id { get; set; }
    }
}
