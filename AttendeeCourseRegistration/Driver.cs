using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using Microsoft.VisualBasic.FileIO;
using System.Text.RegularExpressions;
using System.Linq;

namespace AttendeeCourseRegistration
{
    public class Driver
    {
        static void Main(string[] args)
        {
            String courseFile,
                   attendeeFile,
                   registrationFile;

            List<Course> courses = new List<Course>();
            List<Attendee> attendees = new List<Attendee>();
            List<Registration> registrations = new List<Registration>();
            
            Console.Write("Where is the location of your course csv file?: ");
            courseFile = Console.ReadLine();
            
            Console.Write("Where is the location of your attendee csv file?: ");
            attendeeFile = Console.ReadLine();

            Console.Write("Where is the location of your registration csv file?: ");
            registrationFile = Console.ReadLine();
            
            Console.WriteLine();

            try
            {
  
                String[] courseSt = File.ReadAllLines(courseFile);
                String[] attendeeSt = File.ReadAllLines(attendeeFile);
                String[] registrationSt = File.ReadAllLines(registrationFile);

                courses = generateCoursesFromFile(courseSt);
                attendees = generateAttendeesFromFile(attendeeSt);
                registrations = generateRegistrationsFromFile(registrationSt);

                List<Registration> orderedAttendees = registrations.OrderBy(o => o.getDateTime())
                                                                   .Where(o => o.getCourseId() == courses[3].getId())
                                                                   .Take(courses[3].getCapacity())
                                                                   .ToList();

                outputToCSV(registrations, courses, attendees);

            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read.");
                Console.WriteLine(e.Message);
                Console.WriteLine(e.ToString());
            }

            Console.Read();
        }

        static public List<Course> generateCoursesFromFile(String[] courseSt)
        {
            List<Course> result = new List<Course>();
            Console.WriteLine(courseSt[1]);
            for (int i = 1; i < courseSt.Length; i++)
            {
                TextFieldParser parser;
                parser = new TextFieldParser(new StringReader(courseSt[i]));

                parser.HasFieldsEnclosedInQuotes = true;
                parser.SetDelimiters(",");

                String[] values = new String[0];

                while (!parser.EndOfData)
                {
                    values = parser.ReadFields();
                }

                int id,
                    capacity;
                String title,
                        description = null,
                        dateTime;

                id = int.Parse(values[0]);
                title = values[1];
                if (!String.IsNullOrEmpty(values[2]))
                {
                    description = values[2];
                }
                dateTime = values[3];
                capacity = int.Parse(values[4]);

                Course newCourse = new Course(id, capacity, title, description, dateTime);
                result.Add(newCourse);
            }

            return result;
        }

        static public List<Attendee> generateAttendeesFromFile(String[] attendeeSt)
        {
            List<Attendee> result = new List<Attendee>();

            for (int i = 1; i < attendeeSt.Length; i++)
            {
                TextFieldParser parser;
                parser = new TextFieldParser(new StringReader(attendeeSt[i]));

                parser.HasFieldsEnclosedInQuotes = true;
                parser.SetDelimiters(",");

                String[] values = new String[0];

                while (!parser.EndOfData)
                {
                    values = parser.ReadFields();
                }

                int id;
                String firstName,
                       lastName,
                       company = null,
                       email = null,
                       phone = null;

                id = int.Parse(values[0]);
                firstName = values[1];
                lastName = values[2];
                if (!String.IsNullOrEmpty(values[3]))
                {
                    company = values[3];
                }
                if (!String.IsNullOrEmpty(values[4]))
                {
                    email = values[4];
                }
                if (!String.IsNullOrEmpty(values[5]))
                {
                    phone = values[5];
                }

                Attendee newAttendee = new Attendee(id, firstName, lastName, company, email, phone);
                result.Add(newAttendee);
            }

            return result;
        }

        static public List<Registration> generateRegistrationsFromFile(String[] registrationSt)
        {
            List<Registration> result = new List<Registration>();

            for(int i = 1; i < registrationSt.Length; i++)
            {
                String[] values = registrationSt[i].Split(',');

                int courseId = int.Parse(values[0]),
                    attendeeId = int.Parse(values[1]);
                String dateTime = values[2];

                Registration newRegistration = new Registration(courseId, attendeeId, dateTime);
                result.Add(newRegistration);
            }

            return result;
        }

        static public void outputToCSV(List<Registration> registrations, List<Course> courses, List<Attendee> attendees)
        {
            StringBuilder csv = new StringBuilder();

            var titles = string.Format("{0},{1},{2},{3},{4},{5},{6},{7}", "CourseId", "CourseTitle", "AttendeeId",
                                                                          "FirstName", "LastName", "Email",
                                                                          "Phone", "EnrollmentStatus");
            csv.AppendLine(titles);

            for (int i = 0; i < registrations.Count; i++)
            {
                int courseId = registrations[i].getCourseId();
                List<Course> course = courses.Where(o => o.getId() == registrations[i].getCourseId())
                                             .Take(1)
                                             .ToList();
                String courseTitle = course[0].getTitle();
                int attendeeId = registrations[i].getAttendeeId();
                List<Attendee> attendee = attendees.Where(o => o.getId() == registrations[i].getAttendeeId())
                                                   .Take(1)
                                                   .ToList();
                String firstName = attendee[0].getFirstName();
                String lastName = attendee[0].getLastName();
                String email = attendee[0].getEmail();
                String phone = attendee[0].getPhone();
                String enrollmentStatus = registrations[i].getEnrollmentStatus(course[0], registrations, attendee[0]);

                var newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7}", courseId, courseTitle, attendeeId,
                                                                               firstName, lastName, email,
                                                                               phone, enrollmentStatus);
                csv.AppendLine(newLine);
            }

            File.WriteAllText("outputFile.csv", csv.ToString());
        }
    }
}
