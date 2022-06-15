using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlfaBank.DataAccess.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Login { get; set; }

        private DateTime registrationDate;
        public DateTime RegistrationDate 
        { 
            get { return registrationDate.Date; } 
            set { this.registrationDate = value; } 
        }
        public bool IsDeleted { get; set; }
    }
}
