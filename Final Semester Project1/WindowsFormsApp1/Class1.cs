using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Class1
    {

        public string Email { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Birthday { get; set; }

        public FormGender Gender { get; set; }

        public Class1( string email,string password, string name, string bd1, string bd2, string bd3, FormGender gender)
        {
           
            Email = email;
            Password = password;
            Name = name;
            Birthday = bd1 + bd2 + bd3;
            Gender = gender;

        }
        public Class1() { }

}
       public enum FormGender
{
    FEMALE,MALE,CUSTOM
}
}
