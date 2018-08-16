using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendeeCourseRegistration
{
    public class Registration
    {
        private int courseId,
                    attendeeId;
        private String dateTime;

        public Registration()
        {
            this.courseId = 0;
            this.attendeeId = 0;
            this.dateTime = null;
        }
                    
        public Registration(int courseId, int attendeeId, String dateTime)
        {
            this.courseId = courseId;
            this.attendeeId = attendeeId;
            this.dateTime = dateTime;
        }

        public void setCourseId(int courseId)
        {
            this.courseId = courseId;
        }

        public void setAttendeeId(int attendeeId)
        {
            this.attendeeId = attendeeId;
        }

        public void setDateTime(String dateTime)
        {
            this.dateTime = dateTime;
        }

        public int getCourseId()
        {
            return courseId;
        }

        public int getAttendeeId()
        {
            return attendeeId;
        }

        public String getDateTime()
        {
            return dateTime;
        }

        public String getEnrollmentStatus(Course course, List<Registration> registrations, Attendee attendee)
        {
            String enrollmentStatus = "Wait Listed";
            List<Registration> enrolledAttendees = registrations.OrderBy(o => o.getDateTime())
                                                                .Where(o => o.getCourseId() == course.getId())
                                                                .Take(course.getCapacity())
                                                                .ToList();
            for(int i = 0; i < enrolledAttendees.Count; i++)
            {
                if(attendee.getId() == enrolledAttendees[i].getAttendeeId()) enrollmentStatus = "Registered";
            }

            return enrollmentStatus;
        }
    }
}
