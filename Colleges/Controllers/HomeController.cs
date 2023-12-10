using Colleges.DBModels;
using Colleges.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Colleges.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DbContextFile _context;

        public HomeController(ILogger<HomeController> logger, DbContextFile dbContextFile)
        {
            _logger = logger;
            _context = dbContextFile;
        }

        public IActionResult Index()
        {
            //var values = (from std in _context.Students
            //              from sc in std.StudentCourses
            //              from su in std.StudentUniversities
            //              select new
            //              {
            //                  Name = std.Name,
            //                  Course = sc.CidNavigation.Name,
            //                  University = su.UidNavigation.Name,
            //              }).ToList();
            //var values2 = (from std in _context.Students
            //              join sc in _context.StudentCourses on std.Id equals sc.Sid
            //              join su in _context.StudentUniversities on std.Id equals su.Sid
            //              select new
            //              {
            //                  StudentId = std.Id,
            //                  StudentName = std.Name,
            //                  CourseId = sc.CidNavigation.Id,
            //                  CourseName = sc.CidNavigation.Name,
            //                  UniversityId = su.UidNavigation.Id,
            //                  UniversityName = su.UidNavigation.Name,
            //              }).ToList();


            List<Student_Course_UniversityDetails> Details = (from std in _context.Students
                                                              join cour in _context.Courses
                                                              on std.Id equals cour.Id
                                                              join u in _context.Universities
                                                              on std.Id equals u.Id
                                                              select new Student_Course_UniversityDetails
                                                              {
                                                                  Student_Id = std.Id,
                                                                  Student_Name = std.Name,
                                                                  Course_Id = cour.Id,
                                                                  Course_Name = cour.Name,
                                                                  University_Id = u.Id,
                                                                  University_Name = u.Name,
                                                              }).ToList();

            return View(Details);
        }
        public IActionResult AddToStudent_Course_University(Student_Course_University data)
        {
            try
            {
                // Insert into Student table
                var student = new Student { Name = data.Student_Name };
                _context.Students.Add(student);
                //_context.SaveChanges();

                // Insert into Course table
                var course = new Course { Name = data.Course_Name };
                _context.Courses.Add(course);
                //_context.SaveChanges();

                // Insert into University table
                var university = new University { Name = data.University_Name };
                _context.Universities.Add(university);
                _context.SaveChanges();

                // Insert into StudentCourse table
                var studentCourse = new StudentCourse
                {
                    Sid = student.Id,
                    Cid = course.Id
                };
                _context.StudentCourses.Add(studentCourse);
                _context.SaveChanges();

                var studentUniversity = new StudentUniversity
                {
                    Sid = student.Id,
                    Uid = university.Id
                };
                _context.StudentUniversities.Add(studentUniversity);
                _context.SaveChanges();
                // Insert into CourseUniversity table
                var courseUniversity = new CourseUniversity
                {
                    Cid = course.Id,
                    Uid = university.Id
                };
                _context.CourseUniversities.Add(courseUniversity);
                _context.SaveChanges();

                // Commit the transaction
                //transaction.Commit();

                return View();
            }
            catch (Exception ex)
            {
                // Handle the exception and optionally log it
                //transaction.Rollback();
                Console.WriteLine(ex.Message);
                return BadRequest("Error occurred during transaction.");
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult EditDetails()
        {

            List<Student_Course_UniversityDetails> Data = (from std in _context.Students
                                                           join sc in _context.StudentCourses on std.Id equals sc.Sid
                                                           join su in _context.StudentUniversities on std.Id equals su.Sid
                                                           select new Student_Course_UniversityDetails
                                                           {
                                                               Student_Id = std.Id,
                                                               Student_Name = std.Name,
                                                               Course_Id = sc.CidNavigation.Id,
                                                               Course_Name = sc.CidNavigation.Name,
                                                               University_Id = su.UidNavigation.Id,
                                                               University_Name = su.UidNavigation.Name,
                                                           }).ToList();
            return View(Data);
        }
        [HttpPost]
        public IActionResult EditDetails(Student_Course_UniversityDetails student_Course_UniversityDetails)
        {
            var std = _context.Students.Where(x => x.Id == student_Course_UniversityDetails.Student_Id).FirstOrDefault();
            if (std != null)
            {
                std.Name = student_Course_UniversityDetails.Student_Name;
            }
            _context.Students.Update(std);
            var course = _context.Courses.Where(x => x.Id == student_Course_UniversityDetails.Course_Id).FirstOrDefault();
            if(course != null)
            {
                course.Name = student_Course_UniversityDetails.Course_Name;
            }
            _context.Courses.Update(course);
            var university = _context.Universities.Where(x => x.Id == student_Course_UniversityDetails.University_Id).FirstOrDefault();
            if (university != null)
            {
                university.Name = student_Course_UniversityDetails.University_Name;
            }
            _context.Universities.Update(university);
            _context.SaveChanges();
            return RedirectToAction(nameof(EditDetails));
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}