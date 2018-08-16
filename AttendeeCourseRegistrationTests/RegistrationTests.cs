using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AttendeeCourseRegistration;

namespace AttendeeCourseRegistrationTests
{
    [TestClass]
    public class RegistrationTests
    {
        [TestMethod]
        public void constructorTest()
        {
            Registration r = new Registration();

            Assert.AreEqual(0, r.getCourseId());
            Assert.AreEqual(0, r.getAttendeeId());
            Assert.AreEqual(null, r.getDateTime());
        }

        [TestMethod]
        public void loadedConstructorTest()
        {
            Registration r = new Registration(1, 2, "1997-07-23 06:55:12");

            Assert.AreEqual(1, r.getCourseId());
            Assert.AreEqual(2, r.getAttendeeId());
            Assert.AreEqual("1997-07-23 06:55:12", r.getDateTime());
        }

        [TestMethod]
        public void setCourseIdTest()
        {
            Registration r = new Registration();

            r.setCourseId(1);

            Assert.AreEqual(1, r.getCourseId());
        }

        [TestMethod]
        public void setAttendeeIdTest()
        {
            Registration r = new Registration();

            r.setAttendeeId(2);

            Assert.AreEqual(2, r.getAttendeeId());
        }

        [TestMethod]
        public void setdateTimeTest()
        {
            Registration r = new Registration();

            r.setDateTime("1993-02-26 05:55:12");

            Assert.AreEqual("1993-02-26 05:55:12", r.getDateTime());
        }

        [TestMethod]
        public void getEnrollmentStatusTest()
        {
            Course c = new Course(435, 2, "The Title", "The description", "2005-01-13 15:30:00");
            List<Registration> testRegistrations = new List<Registration>();
            Attendee a1 = new Attendee(1, "Dillan", "Hodges", "Cool Company", "dhodge@yahoo.com", "(704)555-6789");
            Attendee a2 = new Attendee(3, "Rob", "Fulk", "Cooler Company", "robby@yahoo.com", "(704)525-6789");

            testRegistrations.Add(new Registration(435, 3, "2004-10-25 12:01:01"));
            testRegistrations.Add(new Registration(435, 2, "2004-10-26 22:14:02"));
            testRegistrations.Add(new Registration(435, 1, "2004-11-11 01:22:23"));

            Assert.AreEqual("Registered", testRegistrations[0].getEnrollmentStatus(c, testRegistrations, a2));
            Assert.AreEqual("Wait Listed", testRegistrations[2].getEnrollmentStatus(c, testRegistrations, a1));
        }
    }
}
