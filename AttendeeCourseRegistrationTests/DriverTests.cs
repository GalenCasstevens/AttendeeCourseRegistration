using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using Microsoft.VisualBasic.FileIO;
using System.Text.RegularExpressions;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AttendeeCourseRegistration;

namespace AttendeeCourseRegistrationTests
{
    [TestClass]
    public class DriverTests
    {
        [TestMethod]
        public void generateCoursesFromFileTest()
        {
            String[] testStrings = new String[]
            {
                "CourseId,Title,Description,DateTime,Capcity",
                "2,\"A Very Popular Title Indeed\",\"An awesome description.\",2009-12-07 12:44:21,300",
                "3,\"The Most Popular Title Indeed\",\"A magnificent description.\",2003-12-25 11:55:33,400"
            };
            List<Course> testCourses = Driver.generateCoursesFromFile(testStrings);

            //.WriteLine(testCourses[0].getId().ToString());

            Assert.AreEqual(2, testCourses[0].getId());
            Assert.AreEqual("A Very Popular Title Indeed", testCourses[0].getTitle());
            Assert.AreEqual("An awesome description.", testCourses[0].getDescription());
            Assert.AreEqual("2009-12-07 12:44:21", testCourses[0].getDateTime());
            Assert.AreEqual(300, testCourses[0].getCapacity());
            Assert.AreEqual(3, testCourses[1].getId());
            Assert.AreEqual("The Most Popular Title Indeed", testCourses[1].getTitle());
            Assert.AreEqual("A magnificent description.", testCourses[1].getDescription());
            Assert.AreEqual("2003-12-25 11:55:33", testCourses[1].getDateTime());
            Assert.AreEqual(400, testCourses[1].getCapacity());
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void invalidDataFromCourseFile()
        {
            String[] testStrings = new String[]
            {
                "CourseId,Title,Description,DateTime,Capcity",
                "\"\",\"A Very Popular Title Indeed\",\"An awesome description.\",2009-12-07 12:44:21,300",
                "\"String shouldn't be here\",\"The Most Popular Title Indeed\",\"A magnificent description.\",2003-12-25 11:55:33,400"
            };
            List<Course> testCourses = Driver.generateCoursesFromFile(testStrings);
        }

        [TestMethod]
        public void validateMessageCourseInvalidIDOrCapacity()
        {
            String[] testStrings = new String[]
            {
                "CourseId,Title,Description,DateTime,Capcity",
                "\"\",\"A Very Popular Title Indeed\",\"An awesome description.\",2009-12-07 12:44:21,\"efewfewfewf\"",
                "\"String shouldn't be here\",\"The Most Popular Title Indeed\",\"A magnificent description.\",2003-12-25 11:55:33,400"
            };
            
            try
            {
                List<Course> testCourses = Driver.generateCoursesFromFile(testStrings);
            }
            catch(FormatException e)
            {
                Assert.AreEqual("The data in your course file is not in the correct format.", e.Message);
            }
        }

        [TestMethod]
        public void validateMessageCourseDuplicateID()
        {
            String[] testStrings = new String[]
            {
                "CourseId,Title,Description,DateTime,Capcity",
                "5,\"A Very Popular Title Indeed\",\"An awesome description.\",2009-12-07 12:44:21,300",
                "5,\"The Most Popular Title Indeed\",\"A magnificent description.\",2003-12-25 11:55:33,400"
            };

            try
            {
                List<Course> testCourses = Driver.generateCoursesFromFile(testStrings);
            }
            catch (Exception e)
            {
                Assert.AreEqual("IDs in the course file must be unique.", e.Message);
            }
        }

        [TestMethod]
        public void validateMessageCourseEmptyTitle()
        {
            String[] testStrings = new String[]
            {
                "CourseId,Title,Description,DateTime,Capcity",
                "5,\"\",\"\",2009-12-07 12:44:21,300",
                "6,\"The Most Popular Title Indeed\",\"A magnificent description.\",2003-12-25 11:55:33,400"
            };

            try
            {
                List<Course> testCourses = Driver.generateCoursesFromFile(testStrings);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Title in the course file can't be empty", e.Message);
            }
        }

        [TestMethod]
        public void validateMessageCourseInvalidDateTime()
        {
            String[] testStrings = new String[]
            {
                "CourseId,Title,Description,DateTime,Capcity",
                "5,\"A Very Popular Title Indeed\",\"An awesome description.\",2009-12-07 12:44:21,300",
                "6,\"The Most Popular Title Indeed\",\"A magnificent description.\",23,400"
            };

            try
            {
                List<Course> testCourses = Driver.generateCoursesFromFile(testStrings);
            }
            catch (Exception e)
            {
                Assert.AreEqual("The date in your course file is in the incorrect format.", e.Message);
            }
        }

        [TestMethod]
        public void generateAttendeesFromFileTest()
        {
            String[] testStrings = new String[]
            {
                "AttendeeId,FirstName,LastName,Company,Email,Phone",
                "324,\"Joe\",\"Richards\",\"Killer Company\",\"jrichards@yahoo.com\",\"(704)123-1234\"",
                "924,\"Jim\",\"Ron\",\"Nice Company\",\"jr123@gmail.com\",\"(704)777-7777\""
            };
            List<Attendee> testAttendees = Driver.generateAttendeesFromFile(testStrings);

            Assert.AreEqual(324, testAttendees[0].getId());
            Assert.AreEqual("Joe", testAttendees[0].getFirstName());
            Assert.AreEqual("Richards", testAttendees[0].getLastName());
            Assert.AreEqual("Killer Company", testAttendees[0].getCompany());
            Assert.AreEqual("jrichards@yahoo.com", testAttendees[0].getEmail());
            Assert.AreEqual("(704)123-1234", testAttendees[0].getPhone());
            Assert.AreEqual(924, testAttendees[1].getId());
            Assert.AreEqual("Jim", testAttendees[1].getFirstName());
            Assert.AreEqual("Ron", testAttendees[1].getLastName());
            Assert.AreEqual("Nice Company", testAttendees[1].getCompany());
            Assert.AreEqual("jr123@gmail.com", testAttendees[1].getEmail());
            Assert.AreEqual("(704)777-7777", testAttendees[1].getPhone());
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void invalidDataFromAttendeeFile()
        {
            String[] testStrings = new String[]
            {
                "AttendeeId,FirstName,LastName,Company,Email,Phone",
                "\"\",\"Joe\",\"Richards\",\"Killer Company\",\"jrichards@yahoo.com\",\"(704)123-1234\"",
                "\"String should't go here!\",\"\",\"\",\"Nice Company\",\"jr123@gmail.com\",\"(704)777-7777\""
            };
            List<Attendee> testAttendees = Driver.generateAttendeesFromFile(testStrings);
        }

        [TestMethod]
        public void validateMessageAttendeeInvalidID()
        {
            String[] testStrings = new String[]
            {
                "AttendeeId,FirstName,LastName,Company,Email,Phone",
                "\"rgrgregergrgregerg\",\"Joe\",\"Richards\",\"Killer Company\",\"jrichards@yahoo.com\",\"(704)123-1234\"",
                "34,\"FirstName\",\"LastName\",\"Nice Company\",\"jr123@gmail.com\",\"(704)777-7777\""
            };

            try
            {
                List<Attendee> testAttendees = Driver.generateAttendeesFromFile(testStrings);
            }
            catch (FormatException e)
            {
                Assert.AreEqual("The data in your attendee file is not in the correct format.", e.Message);
            }
        }

        [TestMethod]
        public void validateMessageAttendeeEmptyFN()
        {
            String[] testStrings = new String[]
            {
                "AttendeeId,FirstName,LastName,Company,Email,Phone",
                "23,\"Joe\",\"Richards\",\"Killer Company\",\"jrichards@yahoo.com\",\"(704)123-1234\"",
                "34,\"\",\"LastName\",\"Nice Company\",\"jr123@gmail.com\",\"(704)777-7777\""
            };

            try
            {
                List<Attendee> testAttendees = Driver.generateAttendeesFromFile(testStrings);
            }
            catch(Exception e)
            {
                Assert.AreEqual("First name in the attendee file can't be empty.", e.Message);
            }
        }

        [TestMethod]
        public void validateMessageAttendeeEmptyLN()
        {
            String[] testStrings = new String[]
            {
                "AttendeeId,FirstName,LastName,Company,Email,Phone",
                "23,\"Joe\",\"Richards\",\"Killer Company\",\"jrichards@yahoo.com\",\"(704)123-1234\"",
                "34,\"FirstName\",\"\",\"Nice Company\",\"jr123@gmail.com\",\"(704)777-7777\""
            };

            try
            {
                List<Attendee> testAttendees = Driver.generateAttendeesFromFile(testStrings);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Last name in the attendee file can't be empty.", e.Message);
            }
        }

        [TestMethod]
        public void generateRegistrationsFromFileTest()
        {
            String[] testStrings = new String[]
            {
                "CourseId,AttendeeId,DateTime",
                "23,45,1967-06-01 06:55:06",
                "123,567,1824-02-15 03:11:04"
            };
            List<Registration> testRegistrations = Driver.generateRegistrationsFromFile(testStrings);

            Assert.AreEqual(23, testRegistrations[0].getCourseId());
            Assert.AreEqual(45, testRegistrations[0].getAttendeeId());
            Assert.AreEqual("1967-06-01 06:55:06", testRegistrations[0].getDateTime());
            Assert.AreEqual(123, testRegistrations[1].getCourseId());
            Assert.AreEqual(567, testRegistrations[1].getAttendeeId());
            Assert.AreEqual("1824-02-15 03:11:04", testRegistrations[1].getDateTime());
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void invalidDataFromRegistrationFile()
        {
            String[] testStrings = new String[]
            {
                "CourseId,AttendeeId,DateTime",
                "23,45,trhgthrth",
                "123,\"grgergeg\",1824-02-15 03:11:04"
            };
            List<Registration> testRegistrations = Driver.generateRegistrationsFromFile(testStrings);
        }

        [TestMethod]
        public void validateMessageRegistrationInvalidCourseID()
        {
            String[] testStrings = new String[]
            {
                "CourseId,AttendeeId,DateTime",
                "\"grgergeg\",45,trhgthrth",
                "123,234,1824-02-15 03:11:04"
            };

            try
            {
                List<Registration> testRegistrations = Driver.generateRegistrationsFromFile(testStrings);
            }
            catch(FormatException e)
            {
                Assert.AreEqual("The data in your registration file is not in the correct format.", e.Message);
            }
        }

        [TestMethod]
        public void validateMessageRegistrationInvalidAttendeeID()
        {
            String[] testStrings = new String[]
            {
                "CourseId,AttendeeId,DateTime",
                "67,\"rgrgeregregregreg\",trhgthrth",
                "123,234,1824-02-15 03:11:04"
            };

            try
            {
                List<Registration> testRegistrations = Driver.generateRegistrationsFromFile(testStrings);
            }
            catch (FormatException e)
            {
                Assert.AreEqual("The data in your registration file is not in the correct format.", e.Message);
            }
        }

        [TestMethod]
        public void validateMessageRegistrationInvalidDateTime()
        {
            String[] testStrings = new String[]
            {
                "CourseId,AttendeeId,DateTime",
                "67,34,\"trhgthrth\"",
                "123,234,1824-02-15 03:11:04"
            };

            try
            {
                List<Registration> testRegistrations = Driver.generateRegistrationsFromFile(testStrings);
            }
            catch (FormatException e)
            {
                Assert.AreEqual("The date in your registration file is in the incorrect format.", e.Message);
            }
        }

        [TestMethod]
        public void validateMessageRegistrationAttendeeRegisteredSameCourseTwice()
        {
            String[] testStrings = new String[]
            {
                "CourseId,AttendeeId,DateTime",
                "67,234,1824-02-15 03:11:04",
                "67,234,1824-02-15 03:11:04"
            };

            try
            {
                List<Registration> testRegistrations = Driver.generateRegistrationsFromFile(testStrings);
            }
            catch (Exception e)
            {
                Assert.AreEqual("An attendee cannot be registered for the same course twice.", e.Message);
            }
        }

        [TestMethod]
        public void outputToCSVTest()
        {
            Attendee a1 = new Attendee(60, "Bob", "Jones", "The Great Company", "bjones@aol.com", "(704)555-6789"),
                     a2 = new Attendee(61, "Freddy", "Kruger", "Guitar Place", "fredfred@msn.com", "(704)123-6789"),
                     a3 = new Attendee(62, "Jason", "Myers", "Cool Institution", "myers124@gmail.com", "(704)545-6789");
            List<Attendee> testAttendees = new List<Attendee>();
            Course c = new Course(1, 2, "The Main Course", "Cool stuff here.", "2007-08-23 14:00:00");
            List<Course> testCourses = new List<Course>();
            Registration r1 = new Registration(1, 61, "2007-02-25 23:11:11"),
                         r2 = new Registration(1, 60, "2007-03-23 12:12:12"),
                         r3 = new Registration(1, 62, "2007-05-12 03:03:03");
            List<Registration> testRegistrations = new List<Registration>();
            String testEnrollmentFile = System.Configuration.ConfigurationManager.AppSettings["testEnrollmentFile"];

            testAttendees.Add(a1);
            testAttendees.Add(a2);
            testAttendees.Add(a3);
            testCourses.Add(c);
            testRegistrations.Add(r1);
            testRegistrations.Add(r2);
            testRegistrations.Add(r3);
            Console.WriteLine(testRegistrations[0].getCourseId() + testCourses[0].getId());
            Driver.outputToCSV(testRegistrations, testCourses, testAttendees);
            String[] enrollmentSt = File.ReadAllLines(testEnrollmentFile);
            TextFieldParser parser;
            parser = new TextFieldParser(new StringReader(enrollmentSt[3]));
            parser.HasFieldsEnclosedInQuotes = true;
            parser.SetDelimiters(",");
            String[] values = new String[0];
            while (!parser.EndOfData)
            {
                values = parser.ReadFields();
            }
         
            Assert.AreEqual(1, int.Parse(values[0]));
            Assert.AreEqual("The Main Course", values[1]);
            Assert.AreEqual(62, int.Parse(values[2]));
            Assert.AreEqual("Jason", values[3]);
            Assert.AreEqual("Myers", values[4]);
            Assert.AreEqual("myers124@gmail.com", values[5]);
            Assert.AreEqual("(704)545-6789", values[6]);
            
            TextFieldParser parser2;
            parser2 = new TextFieldParser(new StringReader(enrollmentSt[1]));
            parser2.HasFieldsEnclosedInQuotes = true;
            parser2.SetDelimiters(",");
            String[] values2 = new String[0];
            while (!parser2.EndOfData)
            {
                values2 = parser2.ReadFields();
            }
            
            Assert.AreEqual(1, int.Parse(values2[0]));
            Assert.AreEqual("The Main Course", values2[1]);
            Assert.AreEqual(61, int.Parse(values2[2]));
            Assert.AreEqual("Freddy", values2[3]);
            Assert.AreEqual("Kruger", values2[4]);
            Assert.AreEqual("fredfred@msn.com", values2[5]);
            Assert.AreEqual("(704)123-6789", values2[6]);
        }
    }
}
