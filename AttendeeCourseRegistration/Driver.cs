﻿using System;
using System.Numerics;
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
        static List<String> errors = new List<String>();
        static void Main(string[] args)
        {
            String courseFile = System.Configuration.ConfigurationManager.AppSettings["courseFile"],
                   attendeeFile = System.Configuration.ConfigurationManager.AppSettings["attendeeFile"],
                   registrationFile = System.Configuration.ConfigurationManager.AppSettings["registrationFile"];
            List<Course> courses = new List<Course>();
            List<Attendee> attendees = new List<Attendee>();
            List<Registration> registrations = new List<Registration>();
            String[] courseSt = new String[]{};
            String[] attendeeSt = new String[]{};
            String[] registrationSt = new String[]{};

            Console.WriteLine("_.~\"~._.~\"~._.~\"~._.~\"~._.~\"~._.~\"~._.~\"~._.~\"~._.~\"~._.~\"~._.~\"~._.~\"~._.~\"~._.~\"~._.~\"~._.~\"~.\n");
            Console.WriteLine("Welcome to Attendee Course Registratin App!!");
            Console.WriteLine("\n_.~\"~._.~\"~._.~\"~._.~\"~._.~\"~._.~\"~._.~\"~._.~\"~._.~\"~._.~\"~._.~\"~._.~\"~._.~\"~._.~\"~._.~\"~._.~\"~.\n");

            /*         
            Console.Write("Where is the location of your course csv file?: ");
            courseFile = Console.ReadLine();
            
            Console.Write("Where is the location of your attendee csv file?: ");
            attendeeFile = Console.ReadLine();

            Console.Write("Where is the location of your registration csv file?: ");
            registrationFile = Console.ReadLine();
            */

            try
            {
                courseSt = File.ReadAllLines(courseFile);
                attendeeSt = File.ReadAllLines(attendeeFile);
                registrationSt = File.ReadAllLines(registrationFile);

                courses = generateCoursesFromFile(courseSt);
                attendees = generateAttendeesFromFile(attendeeSt);
                registrations = generateRegistrationsFromFile(registrationSt);

                if(errors.Count > 0)
                {
                    Console.WriteLine("Errors: \n");
                    for (int i = 0; i < errors.Count; i++)
                    {
                        Console.WriteLine(i + 1 + ". " + errors[i]);
                    }
                    Console.WriteLine("\n_.~\"~._.~\"~._.~\"~._.~\"~._.~\"~._.~\"~._.~\"~._.~\"~._.~\"~._.~\"~._.~\"~._.~\"~._.~\"~._.~\"~._.~\"~._.~\"~.");
                    throw new Exception("");
                }

                outputToCSV(registrations, courses, attendees);
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.WriteLine("Press enter to exit.");
                Console.WriteLine("\n_.~\"~._.~\"~._.~\"~._.~\"~._.~\"~._.~\"~._.~\"~._.~\"~._.~\"~._.~\"~._.~\"~._.~\"~._.~\"~._.~\"~._.~\"~._.~\"~.\n");
            }
            Console.Read();
        }

        static public List<Course> generateCoursesFromFile(String[] courseSt)
        {
            List<Course> result = new List<Course>();

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

                if (values.Length > 5)
                {
                    errors.Add("The only columns in your course file should be: " +
                               "ID, Title, Description, DateTime and Capcity on row "
                               + (i + 1) + ".");
                }

                BigInteger id = 0,
                    capacity = 0;
                String title = null,
                       description = null,
                       dateTime = null;
                try
                {
                    id = int.Parse(values[0]);
                }
                catch (FormatException e)
                {
                    errors.Add("An ID in your course file is invalid on row " + (i + 1) + ".");
                    continue;
                }

                title = values[1];
                if (!String.IsNullOrEmpty(values[2]))
                {
                    description = values[2];
                }
                DateTime test;
                if(!DateTime.TryParse(values[3], out test))
                {
                    errors.Add("The date in your course file is in the incorrect format on row " + (i + 1) + ".");
                    continue;
                }
                dateTime = values[3];

                try
                {
                    capacity = int.Parse(values[4]);
                }
                catch (FormatException e)
                {
                    errors.Add("A capacity value in your course file is invalid on row " + (i + 1) + ".");
                    continue;
                }

                if(String.IsNullOrEmpty(title))
                {
                    errors.Add("Title in the course file can't be empty on row " + (i + 1) + ".");
                    continue;
                }

                for(int j = 0; j < result.Count; j++)
                {
                    if(id == result[j].getId())
                    {
                        errors.Add("IDs in the course file must be unique on row " + (j + 2)
                                            + " and row " + (i + 1) + ".");
                    }
                }

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

                if (values.Length > 6)
                {
                    errors.Add("The only columns in your attendee file should be: " +
                                        "ID, FirstName, LastName, Company, Email, Phone "
                                        + "and Capcity on row " + (i + 1) + ".");
                }

                BigInteger id = 0;
                String firstName = null,
                       lastName = null,
                       company = null,
                       email = null,
                       phone = null;
                try
                {
                    id = int.Parse(values[0]);
                }
                catch (FormatException e)
                {
                    errors.Add("An ID in your attendee file is invalid on row " + (i + 1) + ".");
                    continue;
                }
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

                if(String.IsNullOrEmpty(firstName))
                {
                    errors.Add("First name in the attendee file can't be empty on row " + (i + 1) + ".");
                    continue;
                }
                if(String.IsNullOrEmpty(lastName))
                {
                    errors.Add("Last name in the attendee file can't be empty on row " + (i + 1) + ".");
                    continue;
                }
                
                if(firstName.All(Char.IsNumber))
                {
                    errors.Add("First name in the attendee file cannot contain numbers on row " + (i + 1) + ".");
                    continue;
                }
                if (lastName.All(Char.IsNumber))
                {
                    errors.Add("Last name in the attendee file cannot contain numbers onc row " + (i + 1) + ".");
                    continue;
                }

                for(int j = 0; j < result.Count; j++)
                {
                    if (id == result[j].getId())
                    {
                        errors.Add("IDs in the attendee file must be unique on row " + (j + 2) +
                                   " and row " + (i + 1) + ".");
                        continue;
                    }
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
                int courseId = 0,
                    attendeeId = 0;
                String dateTime = null;

                if (values.Length > 3)
                {
                    errors.Add("The only columns in your registration file should be: " +
                               "CourseId, AttendeeID and DateTime on row " + (i + 1) + ".");
                }

                try
                {
                    courseId = int.Parse(values[0]);
                }
                catch (FormatException e)
                {
                    errors.Add("A CourseID in your registration file is invalid on row " + (i + 1) + ".");
                    continue;
                }
                try
                {
                    attendeeId = int.Parse(values[1]);
                }
                catch (FormatException e)
                {
                    errors.Add("An AttendeeID in your registration file is invalid on row " + (i + 1) + ".");
                    continue;
                }
                dateTime = values[2];

                DateTime test;
                if (!DateTime.TryParse(values[2], out test))
                {
                    errors.Add("The date in your registration file is invalid on row " + (i + 1) + ".");
                    continue;
                }

                for (int j = 0; j < result.Count; j++)
                {
                    if(courseId == result[j].getCourseId() && attendeeId == result[j].getAttendeeId())
                    {
                        errors.Add("An attendee cannot be registered for the same course twice on row "
                                   + (j + 2) + " and row " + (i + 1) + ".");
                        continue;
                    }
                }

                Registration newRegistration = new Registration(courseId, attendeeId, dateTime);
                result.Add(newRegistration);
            }

            return result;
        }

        static public void outputToCSV(List<Registration> registrations, List<Course> courses, List<Attendee> attendees)
        {
            try
            {
                StringBuilder csv = new StringBuilder();
                String outputFile = System.Configuration.ConfigurationManager.AppSettings["outputFile"],
                       testEnrollmentFile = System.Configuration.ConfigurationManager.AppSettings["testEnrollmentFile"];
                var titles = string.Format("{0},{1},{2},{3},{4},{5},{6},{7}", "CourseId", "CourseTitle", "AttendeeId",
                                                                              "FirstName", "LastName", "Email",
                                                                              "Phone", "EnrollmentStatus");
                csv.AppendLine(titles);

                for (int i = 0; i < registrations.Count; i++)
                {
                    BigInteger courseId = registrations[i].getCourseId();
                    List<Course> course = courses.Where(o => o.getId() == registrations[i].getCourseId())
                                                 .Take(1)
                                                 .ToList();
                    String courseTitle = course[0].getTitle();
                    BigInteger attendeeId = registrations[i].getAttendeeId();
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

                File.WriteAllText(outputFile, csv.ToString());
                Console.WriteLine("Reading from and outputting to files was successful.");
                Console.WriteLine("\n_.~\"~._.~\"~._.~\"~._.~\"~._.~\"~._.~\"~._.~\"~._.~\"~._.~\"~._.~\"~._.~\"~._.~\"~._.~\"~._.~\"~._.~\"~._.~\"~.\n");
            }
            catch
            {
                throw new Exception("There was an error outputting to the file");
            }
            
        }
    }
}
