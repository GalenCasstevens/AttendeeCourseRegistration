using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendeeCourseRegistration
{
    public class Course
    {
        private int id,
                    capacity;
        private String title,
                       description,
                       dateTime;

        public Course()
        {
            this.id = 0;
            this.capacity = 0;
            this.title = null;
            this.description = null;
            this.dateTime = null;
        }

        public Course(int id, int capacity, String title, String description, String dateTime)
        {
            this.id = id;
            this.capacity = capacity;
            this.title = title;
            this.description = description;
            this.dateTime = dateTime;
        }

        public void setId(int id)
        {
            this.id = id;
        }

        public void setCapacity(int capacity)
        {
            this.capacity = capacity;
        }

        public void setTitle(String title)
        {
            this.title = title;
        }

        public void setDescription(String description)
        {
            this.description = description;
        }

        public void setDateTime(String dateTime)
        {
            this.dateTime = dateTime;
        }

        public int getId()
        {
            return id;
        }

        public int getCapacity()
        {
            return capacity;
        }

        public String getTitle()
        {
            return title;
        }

        public String getDescription()
        {
            return description;
        }

        public String getDateTime()
        {
            return dateTime;
        }

        public String toString()
        {
            return "id: " + this.id + Environment.NewLine + "title: " + this.title +
                Environment.NewLine + "description: " + this.description + Environment.NewLine +
                "date/time: " + this.dateTime + Environment.NewLine + "capacity: " + capacity;
        }
    }
}
