
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Configuration;
using CanvasData;
using CanvasData.Biz;


namespace WebTestHarness.Controllers
{
    public class DataController : Controller
    {
        // GET: Data
        public ActionResult Index()
        {
            return View();
        }

        public ContentResult SearchUsers(string id, string searchterm)
        {
            List<CanvasData.Biz.Model.User> Users;
            string token = WebConfigurationManager.AppSettings["CanvasToken"].ToString();
            string webURL = WebConfigurationManager.AppSettings["CavasURL"].ToString();

            CanvasData.Biz.canvasClient client = new CanvasData.Biz.canvasClient(webURL, token);

            Users = client.SearchUsers(id.ToString(), searchterm);

            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            serializer.MaxJsonLength = Int32.MaxValue;

            var result = new ContentResult
            {
                Content = serializer.Serialize(Users),
                ContentType = "application/json"

            };

            return result;

        }

        public ContentResult GetCourseEnrollmentsWithGrades(string courseID, string searchTerm)
        {

            List<CanvasData.Biz.Model.EnrollmentRecord> Enrollments;
            string token = WebConfigurationManager.AppSettings["CanvasToken"].ToString();
            string webURL = WebConfigurationManager.AppSettings["CavasURL"].ToString();

            CanvasData.Biz.canvasClient client = new CanvasData.Biz.canvasClient(webURL, token);

            Enrollments = client.GetCourseEnrollmentsWithGrades(courseID, searchTerm);


            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            serializer.MaxJsonLength = Int32.MaxValue;

            var result = new ContentResult
            {
                Content = serializer.Serialize(Enrollments),
                ContentType = "application/json"

            };

            return result;


        }


        public ContentResult GetCourseQuizzes(string courseID)
        {

            List<CanvasData.Biz.Model.Quiz> Quizzes;
            string token = WebConfigurationManager.AppSettings["CanvasToken"].ToString();
            string webURL = WebConfigurationManager.AppSettings["CavasURL"].ToString();

            CanvasData.Biz.canvasClient client = new CanvasData.Biz.canvasClient(webURL, token);

            Quizzes = client.GetCourseQuizzes(courseID);


            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            serializer.MaxJsonLength = Int32.MaxValue;

            var result = new ContentResult
            {
                Content = serializer.Serialize(Quizzes),
                ContentType = "application/json"

            };

            return result;


        }

        public ContentResult GetCourseAssignments(string courseID)
        {

            List<CanvasData.Biz.Model.Assignment> Assignments;
            string token = WebConfigurationManager.AppSettings["CanvasToken"].ToString();
            string webURL = WebConfigurationManager.AppSettings["CavasURL"].ToString();

            CanvasData.Biz.canvasClient client = new CanvasData.Biz.canvasClient(webURL, token);

            Assignments = client.GetCourseAssignments(courseID);


            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            serializer.MaxJsonLength = Int32.MaxValue;

            var result = new ContentResult
            {
                Content = serializer.Serialize(Assignments),
                ContentType = "application/json"

            };

            return result;


        }


        public ContentResult GetCourseSubmissions(string courseID, string AssignmentID)
        {
            List<CanvasData.Biz.Model.Submission> submissions;
            string token = WebConfigurationManager.AppSettings["CanvasToken"].ToString();
            string webURL = WebConfigurationManager.AppSettings["CavasURL"].ToString();

            CanvasData.Biz.canvasClient client = new CanvasData.Biz.canvasClient(webURL, token);

            submissions = client.GetCourseSubmissions(courseID,AssignmentID);


            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            serializer.MaxJsonLength = Int32.MaxValue;

            var result = new ContentResult
            {
                Content = serializer.Serialize(submissions),
                ContentType = "application/json"

            };

            return result;



        }

        public ContentResult GetCourseQuizSubmissions(string courseID, string quizID)
        {
            List<CanvasData.Biz.Model.QuizSubmission> submissions;
            string token = WebConfigurationManager.AppSettings["CanvasToken"].ToString();
            string webURL = WebConfigurationManager.AppSettings["CavasURL"].ToString();

            CanvasData.Biz.canvasClient client = new CanvasData.Biz.canvasClient(webURL, token);

            submissions = client.GetCourseQuizSubmissions(courseID, quizID);


            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            serializer.MaxJsonLength = Int32.MaxValue;

            var result = new ContentResult
            {
                Content = serializer.Serialize(submissions),
                ContentType = "application/json"

            };

            return result;



        }

        public ContentResult EnrollUser(string courseID, string userID, string sectionID)
        {
            CanvasData.Biz.Model.EnrollmentRecord enrollment;
            string token = WebConfigurationManager.AppSettings["CanvasToken"].ToString();
            string webURL = WebConfigurationManager.AppSettings["CavasURL"].ToString();

            CanvasData.Biz.canvasClient client = new CanvasData.Biz.canvasClient(webURL, token);

            enrollment = client.EnrollUser(courseID, userID, sectionID);

            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            serializer.MaxJsonLength = Int32.MaxValue;

            var result = new ContentResult
            {
                Content = serializer.Serialize(enrollment),
                ContentType = "application/json"
            };

            return result;


        }

        public ActionResult PublishedCourses(int id, string enrollment_term_id)
        {
            List<CanvasData.Biz.Model.Course> courses;
            string token = WebConfigurationManager.AppSettings["CanvasToken"].ToString();
            string webURL = WebConfigurationManager.AppSettings["CavasURL"].ToString();

            CanvasData.Biz.canvasClient client = new CanvasData.Biz.canvasClient(webURL, token);

            courses = client.getPublishedCourses(id.ToString(), enrollment_term_id);

            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            serializer.MaxJsonLength = Int32.MaxValue;

            var result = new ContentResult
            {
                Content = serializer.Serialize(courses),
                ContentType = "application/json"

            };

            return result;


        }


        public ActionResult EnrollmentDetails(int id, string enrollment_term_id)
        {
            List<CanvasData.Biz.Model.EnrollmentReportActReport> courses;
            string token = WebConfigurationManager.AppSettings["CanvasToken"].ToString();
            string webURL = WebConfigurationManager.AppSettings["CavasURL"].ToString();

            CanvasData.Biz.canvasClient client = new CanvasData.Biz.canvasClient(webURL, token);

            courses = client.getEnrollmentDetails(id.ToString(), enrollment_term_id);



            //var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            //serializer.MaxJsonLength = Int32.MaxValue;

            //var result = new ContentResult
            //{
            //    Content = serializer.Serialize(courses),
            //    ContentType = "application/json"

            //};

            //return result;
            System.IO.MemoryStream mio = new System.IO.MemoryStream();

            ServiceStack.Text.CsvSerializer.SerializeToStream(courses, mio);
            mio.Position = 0;
            FileStreamResult fs = new FileStreamResult(mio, "text/csv");
            fs.FileDownloadName = "EnrollmentDetails.csv";
            return fs;

        }

        public ActionResult PublishedTeachers(int id, string enrollment_term_id)
        {
            List<CanvasData.Biz.Model.Course> courses;
            string token = WebConfigurationManager.AppSettings["CanvasToken"].ToString();
            string webURL = WebConfigurationManager.AppSettings["CavasURL"].ToString();

            CanvasData.Biz.canvasClient client = new CanvasData.Biz.canvasClient(webURL, token);

            courses = client.getPublishedCourses(id.ToString(), enrollment_term_id);
            List<CanvasData.Biz.Model.PubProfile> profiles = client.getPublishedTeachers(courses);

            return Json(profiles, JsonRequestBehavior.AllowGet);


        }

        public ActionResult AccountList(int id)
        {
            string AccountID = id.ToString();
            List<CanvasData.Biz.Model.Account> accounts;
            string token = WebConfigurationManager.AppSettings["CanvasToken"].ToString();
            string webURL = WebConfigurationManager.AppSettings["CavasURL"].ToString();

            CanvasData.Biz.canvasClient client = new CanvasData.Biz.canvasClient(webURL, token);

            accounts = client.getAccounts(AccountID);


            return Json(accounts, JsonRequestBehavior.AllowGet);



        }

        public ActionResult AdminList(int id)
        {
            string AccountID = id.ToString();
            List<CanvasData.Biz.Model.Admin> admins;
            string token = WebConfigurationManager.AppSettings["CanvasToken"].ToString();
            string webURL = WebConfigurationManager.AppSettings["CavasURL"].ToString();

            CanvasData.Biz.canvasClient client = new CanvasData.Biz.canvasClient(webURL, token);

            admins = client.getAdmins(AccountID);

            List<CanvasData.Biz.Model.AdminRPT> adminsRPT = new List<CanvasData.Biz.Model.AdminRPT>();

            foreach (CanvasData.Biz.Model.Admin admin in admins)
            {
                adminsRPT.Add(new CanvasData.Biz.Model.AdminRPT(admin));


            }
            return Json(adminsRPT, JsonRequestBehavior.AllowGet);



        }

    }
}