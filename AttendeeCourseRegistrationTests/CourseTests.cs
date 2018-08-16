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
    public class CourseTests
    {
        [TestMethod]
        public void constructorTest()
        {
            Course c = new Course();

            Assert.AreEqual(0, c.getId());
            Assert.AreEqual(00, c.getCapacity());
            Assert.AreEqual(null, c.getTitle());
            Assert.AreEqual(null, c.getDescription());
            Assert.AreEqual(null, c.getDateTime());
        }

        [TestMethod]
        public void loadedConstructorTest()
        {
            Course c = new Course(18, 200, "Best Title Ever", "Cool description.", "1934-04-23");

            Assert.AreEqual(18, c.getId());
            Assert.AreEqual(200, c.getCapacity());
            Assert.AreEqual("Best Title Ever", c.getTitle());
            Assert.AreEqual("Cool description.", c.getDescription());
            Assert.AreEqual("1934-04-23", c.getDateTime());
            
        }

        [TestMethod]
        public void setIdTest()
        {
            Course c = new Course();

            c.setId(5);

            Assert.AreEqual(5, c.getId());
        }

        [TestMethod]
        public void setCapacityTest()
        {
            Course c = new Course();

            c.setCapacity(300);

            Assert.AreEqual(300, c.getCapacity());
        }

        [TestMethod]
        public void setTitleTest()
        {
            Course c = new Course();

            c.setTitle("Awesome course title");

            Assert.AreEqual("Awesome course title", c.getTitle());
        }

        [TestMethod]
        public void setDescriptionTest()
        {
            Course c = new Course();

            c.setDescription("A very interesting course description.");

            Assert.AreEqual("A very interesting course description.", c.getDescription());
        }

        [TestMethod]
        public void setDateTimeTest()
        {
            Course c = new Course();

            c.setDateTime("2002-04-16 10:22:34");

            Assert.AreEqual("2002-04-16 10:22:34", c.getDateTime());
        }
    }
}
