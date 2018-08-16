using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AttendeeCourseRegistration;

namespace AttendeeCourseRegistrationTests
{
    [TestClass]
    public class AttendeeTests
    {   
        [TestMethod]
        public void constructorTest()
        {
            Attendee a = new Attendee();

            Assert.AreEqual(0, a.getId());
            Assert.AreEqual(null, a.getFirstName());
            Assert.AreEqual(null, a.getLastName());
            Assert.AreEqual(null, a.getCompany());
            Assert.AreEqual(null, a.getEmail());
            Assert.AreEqual(null, a.getPhone());
        }

        [TestMethod]
        public void loadedConstructorTest()
        {
            Attendee a = new Attendee(60, "Bob", "Jones", "The Great Company", "bjones@aol.com", "(704)555-6789");

            Assert.AreEqual(60, a.getId());
            Assert.AreEqual("Bob", a.getFirstName());
            Assert.AreEqual("Jones", a.getLastName());
            Assert.AreEqual("The Great Company", a.getCompany());
            Assert.AreEqual("bjones@aol.com", a.getEmail());
            Assert.AreEqual("(704)555-6789", a.getPhone());
        }

        [TestMethod]
        public void setIdTest()
        {
            Attendee a = new Attendee();

            a.setId(99);

            Assert.AreEqual(99, a.getId());
        }

        [TestMethod]
        public void setFirstNameTest()
        {
            Attendee a = new Attendee();

            a.setFirstName("Galen");

            Assert.AreEqual("Galen", a.getFirstName());
        }

        [TestMethod]
        public void setLastNameTest()
        {
            Attendee a = new Attendee();

            a.setLastName("Casstevens");

            Assert.AreEqual("Casstevens", a.getLastName());
        }

        [TestMethod]
        public void setCompanyTest()
        {
            Attendee a = new Attendee();

            a.setCompany("The Awesome Company");

            Assert.AreEqual("The Awesome Company", a.getCompany());
        }

        [TestMethod]
        public void setEmailTest()
        {
            Attendee a = new Attendee();

            a.setEmail("galen.casstevens@agdata.com");

            Assert.AreEqual("galen.casstevens@agdata.com", a.getEmail());
        }

        [TestMethod]
        public void setPhoneTest()
        {
            Attendee a = new Attendee();

            a.setPhone("(336)444-1234");

            Assert.AreEqual("(336)444-1234", a.getPhone());
        }
    }
}
