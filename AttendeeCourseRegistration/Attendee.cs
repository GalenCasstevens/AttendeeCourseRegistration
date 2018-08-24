using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendeeCourseRegistration
{
    public class Attendee
    {
        private BigInteger id;
        private String firstName,
                       lastName,
                       company,
                       email,
                       phone;

        public Attendee()
        {
            this.id = 0;
            this.firstName = null;
            this.lastName = null;
            this.company = null;
            this.email = null;
            this.phone = null;
        }

        public Attendee(BigInteger id, String firstName, String lastName, String company, String email, String phone)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.company = company;
            this.email = email;
            this.phone = phone;
        }

        public void setId(BigInteger id)
        {
            this.id = id;
        }

        public void setFirstName(String firstName)
        {
            this.firstName = firstName;
        }

        public void setLastName(String lastName)
        {
            this.lastName = lastName;
        }

        public void setCompany(String company)
        {
            this.company = company;
        }

        public void setEmail(String email)
        {
            this.email = email;
        }

        public void setPhone(String phone)
        {
            this.phone = phone;
        }

        public BigInteger getId()
        {
            return id;
        }
        
        public String getFirstName()
        {
            return firstName;
        }

        public String getLastName()
        {
            return lastName;
        }

        public String getCompany()
        {
            return company;
        }

        public String getEmail()
        {
            return email;
        }

        public String getPhone()
        {
            return phone;
        }
     }
}
