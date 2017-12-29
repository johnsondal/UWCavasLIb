using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using RestSharp;



namespace CanvasData.Biz
{

    public class canvasClient
    {

        // Public API CONSTANTS for API CALLS

        //Search 0:AccountID 1: PVI, LOGINID , email
        const string cUserSearch = "api/v1/accounts/{0}/users?search_term={1}";

        //Pull Course Enrollments with Grades
        //const string cCourseEnrollmentsGrades = "api/v1/courses/{0}/users?include[]=enrollments";
        const string cCourseEnrollmentsGrades = "api/v1/courses/{0}/enrollments";

       // https://canvas.wisc.edu:443/api/v1/courses/71555/enrollments

        // Pull Course Quizzes
        const string cCourseQuizzes = "api/v1/courses/{0}/quizzes";

        // Pull Quiz Submissions 
        const string cCourseQuizSubmissions = "/api/v1/courses/{0}/quizzes/{1}/submissions";


        // Pull Course Assignments
        const string cCourseAssignments = "api/v1/courses/{0}/assignments";

        // Pull Course AssignmentSubmissions
        const string cCourseAssignmentSubmissions = "";


        const string cEnrollUser = "/api/v1/courses/{0}/enrollments";

        // 0: AccountID
        const string cPUBLISHED_COURSES = "api/v1/accounts/{0}/courses?published=true&include[]=teachers&enrollment_term_id={1}&per_page=100";

        const string cPUBLISHED_COURSES_ALL = "api/v1/accounts/{0}/courses?include[]=teachers&per_page=100";

        const string cCOURSES_ALL = "api/v1/accounts/{0}/courses?per_page = 100";
        const string cCourse = "api/v1/courses/{0}?include[]=teachers&include=[]=sections";

        const string cSections = "api/v1/courses/{0}/sections";



        const string cPROFILE = "api/v1/users/{0}/profile";

        const string cENROLLMENT = "/api/v1/courses/{0}/users?include[]=email&enrollment_state[]=active";

        const string cENROLLMENTBYTYPE = "/api/v1/courses/{0}/enrollments?type[]={1}Enrollment&per_page=100";

        const string cAccountList = "api/v1/accounts/{0}/sub_accounts?recursive=true";


        const string cAdminList = "api/v1/accounts/{0}/admins";

        const string cCourseTabs = "/api/v1/courses/{0}/tabs?include[]=external";




        // 0: AccountID
        const string cACCOUNT = "api/v1/accounts/{0}";

        List<Biz.Model.Account> AccountCache = new List<Model.Account>();

        List<Biz.Model.Profile> ProfileCache = new List<Model.Profile>();


        private string AccessToken;
        // RestClient client;
        string WebcoursesUri;


        public canvasClient(string CanvasUrL, string token)
        {
            AccessToken = token;
            WebcoursesUri = CanvasUrL;

        }

        private void addAuth(ref RestRequest request)
        {
            request.AddParameter("Authorization",
            string.Format("Bearer " + AccessToken),
                        ParameterType.HttpHeader);

        }

        private string NextLink(IRestResponse response)
        {
            string NextLink = "";
            string link;
            if (response.Headers.Count(e => e.Name == "Link") > 0)
            {
                link = response.Headers.First(e => e.Name == "Link").Value.ToString();
                List<string> links = new List<string>();
                links.AddRange(link.Split((char)44));
                foreach (string s in links)
                {
                    if (s.Contains("rel=\"next\""))
                    {
                        NextLink = s.Split((char)59)[0];
                        return NextLink;
                    }
                }


            }

            return NextLink;

        }


        public List<Model.User> SearchUsers(string accountID, string SearchTerm)
        {

            List<Biz.Model.User> rtnValue = new List<Model.User>();

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            RestClient client = new RestClient(WebcoursesUri);
            RestRequest request = new RestRequest(string.Format(cUserSearch, accountID, SearchTerm), Method.GET);
            addAuth(ref request);

            var response = client.Execute(request);


            rtnValue.AddRange(JsonConvert.DeserializeObject<List<Biz.Model.User>>(response.Content, settings));

            string link = NextLink(response);
            while (link.Length != 0)
            {
                request = new RestRequest(link.Substring(1, link.Length - 2).Substring(WebcoursesUri.Length + 1), Method.GET);
                addAuth(ref request);

                response = client.Execute(request);
                rtnValue.AddRange(JsonConvert.DeserializeObject<List<Biz.Model.User>>(response.Content, settings));
                link = NextLink(response);

            }
            return rtnValue;
        }

        public Model.EnrollmentRecord EnrollUser(string courseID, string userID, string sectionID)
        {

            Biz.Model.EnrollmentRecord rtnValue = new Model.EnrollmentRecord();

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            RestClient client = new RestClient(WebcoursesUri);
            RestRequest request = new RestRequest(string.Format(cEnrollUser, courseID), Method.POST);
            addAuth(ref request);
            request.AddParameter("enrollment[user_id]", userID);
            request.AddParameter("enrollment[enrollment_state]", "active");
            if (sectionID != null)
                request.AddParameter("enrollment[course_section_id]", sectionID);

            var response = client.Execute(request);


            rtnValue = JsonConvert.DeserializeObject<Biz.Model.EnrollmentRecord>(response.Content, settings);


            return rtnValue;
        }


        public List<Model.EnrollmentRecord> GetCourseEnrollmentsWithGrades(string CourseID, string search_term)
        {
            List<Biz.Model.EnrollmentRecord> rtnValue = new List<Model.EnrollmentRecord>();

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            RestClient client = new RestClient(WebcoursesUri);

            string searchURL = string.Format(cCourseEnrollmentsGrades, CourseID);
            if (search_term != null)
                searchURL = searchURL + "&search_term=" + search_term;


            RestRequest request = new RestRequest(searchURL, Method.GET);
            addAuth(ref request);

            var response = client.Execute(request);


            rtnValue.AddRange(JsonConvert.DeserializeObject<List<Biz.Model.EnrollmentRecord>>(response.Content, settings));

            string link = NextLink(response);
            while (link.Length != 0)
            {
                request = new RestRequest(link.Substring(1, link.Length - 2).Substring(WebcoursesUri.Length + 1), Method.GET);
                addAuth(ref request);

                response = client.Execute(request);
                rtnValue.AddRange(JsonConvert.DeserializeObject<List<Biz.Model.EnrollmentRecord>>(response.Content, settings));
                link = NextLink(response);

            }
            return rtnValue;

        }
        public List<Model.Assignment> GetCourseAssignments(string CourseID)
        {
            List<Biz.Model.Assignment> rtnValue = new List<Model.Assignment>();

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            RestClient client = new RestClient(WebcoursesUri);

            string searchURL = string.Format(cCourseAssignments, CourseID);



            RestRequest request = new RestRequest(searchURL, Method.GET);
            addAuth(ref request);

            var response = client.Execute(request);


            rtnValue.AddRange(JsonConvert.DeserializeObject<List<Biz.Model.Assignment>>(response.Content, settings));

            string link = NextLink(response);
            while (link.Length != 0)
            {
                request = new RestRequest(link.Substring(1, link.Length - 2).Substring(WebcoursesUri.Length + 1), Method.GET);
                addAuth(ref request);

                response = client.Execute(request);
                rtnValue.AddRange(JsonConvert.DeserializeObject<List<Biz.Model.Assignment>>(response.Content, settings));
                link = NextLink(response);

            }
            return rtnValue;

        }


        public Model.Course GetCourse(string CourseID)
        {
            Model.Course rtnValue = new Model.Course();

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            RestClient client = new RestClient(WebcoursesUri);

            string searchURL = string.Format(cCourse, CourseID);



            RestRequest request = new RestRequest(searchURL, Method.GET);
            addAuth(ref request);

            var response = client.Execute(request);


            rtnValue = JsonConvert.DeserializeObject<Biz.Model.Course>(response.Content, settings);

   
            return rtnValue;

        }

        public List<Model.Section> GetCourseSections(string CourseID)
        {
            List<Biz.Model.Section> rtnValue = new List<Model.Section>();

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            RestClient client = new RestClient(WebcoursesUri);

            string searchURL = string.Format(cSections, CourseID);



            RestRequest request = new RestRequest(searchURL, Method.GET);
            addAuth(ref request);

            var response = client.Execute(request);


            rtnValue.AddRange(JsonConvert.DeserializeObject<List<Biz.Model.Section>>(response.Content, settings));

            string link = NextLink(response);
            while (link.Length != 0)
            {
                request = new RestRequest(link.Substring(1, link.Length - 2).Substring(WebcoursesUri.Length + 1), Method.GET);
                addAuth(ref request);

                response = client.Execute(request);
                rtnValue.AddRange(JsonConvert.DeserializeObject<List<Biz.Model.Section>>(response.Content, settings));
                link = NextLink(response);

            }
            return rtnValue;

        }

        public List<Model.Submission> GetCourseSubmissions(string courseID, string AssignmentID)
        {
            List<Biz.Model.Submission> rtnValue = new List<Model.Submission>();

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            RestClient client = new RestClient(WebcoursesUri);

            string searchURL = string.Format(cCourseAssignmentSubmissions, courseID, AssignmentID);

            RestRequest request = new RestRequest(searchURL, Method.GET);
            addAuth(ref request);

            var response = client.Execute(request);



            rtnValue.AddRange((JsonConvert.DeserializeObject<List<Biz.Model.Submission>>(response.Content, settings)));

            string link = NextLink(response);
            while (link.Length != 0)
            {
                request = new RestRequest(link.Substring(1, link.Length - 2).Substring(WebcoursesUri.Length + 1), Method.GET);
                addAuth(ref request);

                response = client.Execute(request);
                rtnValue.AddRange((JsonConvert.DeserializeObject<List<Biz.Model.Submission>>(response.Content, settings)));
                link = NextLink(response);

            }


            List<Model.CourseEnrollment> enrollments = getEnrollments(courseID);

            foreach (Biz.Model.Submission item in rtnValue)
            {
                int UserID = item.user_id;
                if (enrollments.Exists(e => e.id == UserID))
                    item.SetUser = enrollments.First(e => e.id == UserID);



            }



            return rtnValue;



        }
        public List<Model.Quiz> GetCourseQuizzes(string CourseID)
        {
            List<Biz.Model.Quiz> rtnValue = new List<Model.Quiz>();

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            RestClient client = new RestClient(WebcoursesUri);

            string searchURL = string.Format(cCourseQuizzes, CourseID);



            RestRequest request = new RestRequest(searchURL, Method.GET);
            addAuth(ref request);

            var response = client.Execute(request);


            rtnValue.AddRange(JsonConvert.DeserializeObject<List<Biz.Model.Quiz>>(response.Content, settings));

            string link = NextLink(response);
            while (link.Length != 0)
            {
                request = new RestRequest(link.Substring(1, link.Length - 2).Substring(WebcoursesUri.Length + 1), Method.GET);
                addAuth(ref request);

                response = client.Execute(request);
                rtnValue.AddRange(JsonConvert.DeserializeObject<List<Biz.Model.Quiz>>(response.Content, settings));
                link = NextLink(response);

            }
            return rtnValue;

        }

        public List<Model.QuizSubmission> GetCourseQuizSubmissions(string courseID, string quizID)
        {
            List<Biz.Model.QuizSubmission> rtnValue = new List<Model.QuizSubmission>();

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            RestClient client = new RestClient(WebcoursesUri);

            string searchURL = string.Format(cCourseQuizSubmissions, courseID, quizID);

            RestRequest request = new RestRequest(searchURL, Method.GET);
            addAuth(ref request);

            var response = client.Execute(request);



            rtnValue.AddRange((JsonConvert.DeserializeObject<Biz.Model.QuizSubmissionContainer>(response.Content, settings).quiz_submissions));

            string link = NextLink(response);
            while (link.Length != 0)
            {
                request = new RestRequest(link.Substring(1, link.Length - 2).Substring(WebcoursesUri.Length + 1), Method.GET);
                addAuth(ref request);

                response = client.Execute(request);
                rtnValue.AddRange((JsonConvert.DeserializeObject<Biz.Model.QuizSubmissionContainer>(response.Content, settings).quiz_submissions));
                link = NextLink(response);

            }


            List<Model.CourseEnrollment> enrollments = getEnrollments(courseID);

            foreach (Biz.Model.QuizSubmission item in rtnValue)
            {
                int UserID = item.user_id;
                if (enrollments.Exists(e => e.id == UserID))
                    item.SetUser = enrollments.First(e => e.id == UserID);



            }



            return rtnValue;



        }

        public List<Biz.Model.Course> getPublishedCourses(string accountID, string enrollment_term_id)
        {
            List<Biz.Model.Course> rtnValue = new List<Model.Course>();

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            RestClient client = new RestClient(WebcoursesUri);
            RestRequest request = new RestRequest(string.Format(cPUBLISHED_COURSES, accountID, enrollment_term_id), Method.GET);
            addAuth(ref request);

            var response = client.Execute(request);


            rtnValue.AddRange(JsonConvert.DeserializeObject<List<Biz.Model.Course>>(response.Content, settings));

            string link = NextLink(response);
            while (link.Length != 0)
            {
                request = new RestRequest(link.Substring(1, link.Length - 2).Substring(WebcoursesUri.Length + 1), Method.GET);
                addAuth(ref request);

                response = client.Execute(request);
                rtnValue.AddRange(JsonConvert.DeserializeObject<List<Biz.Model.Course>>(response.Content, settings));
                link = NextLink(response);

            }



            foreach (Biz.Model.Course item in rtnValue)
            {
                Biz.Model.Account account = getAccount(item.account_id);
                item.account_name = account.name;

                if (account.parent_account_id != null)
                {
                    item.account_parent_id = account.parent_account_id.ToString();
                    item.account_parent_name = getAccount(account.parent_account_id).name;
                }

                //item.cntTeachers = getEnrollmentsbyType(item.id.ToString(), "Teacher").Count;
                //   item.cntTAs = getEnrollmentsbyType(item.id.ToString(), "TA").Count;
                // item.cntStudents = getEnrollmentsbyType(item.id.ToString(), "Student").Count;
                //  item.cntObservers = getEnrollmentsbyType(item.id.ToString(), "Observers").Count;
                //  item.cntDesigners = getEnrollmentsbyType(item.id.ToString(), "Designers").Count;

                item.Tabs = GetCourseTabs(item.id.ToString()).FindAll(e => e.label == "Syllabus" && e.hidden != true);
                if (item.Tabs.Count > 0)
                {
                    item.SyllabusLink = item.Tabs.First(e => e.label == "Syllabus").full_url;

                }

                item.Tabs = null;

            }


            return rtnValue;
        }


        public List<Biz.Model.Course> getPublishedCourseswithSections(string accountID, string enrollment_term_id)
        {

            List<Biz.Model.Course> rtnValue = new List<Model.Course>();


           rtnValue = getPublishedCourses(accountID, enrollment_term_id);

            foreach (Biz.Model.Course item in rtnValue)
            {
                item.sections = GetCourseSections(item.id.ToString());
                

            }


            return rtnValue;
        }



        public List<Biz.Model.CourseTab> GetCourseTabs(string id)
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            RestClient client = new RestClient(WebcoursesUri);
            RestRequest request = new RestRequest(string.Format(cCourseTabs, id), Method.GET);
            addAuth(ref request);

            var response = client.Execute(request);

            List<Biz.Model.CourseTab> rtnValue = new List<Model.CourseTab>();
            rtnValue.AddRange(JsonConvert.DeserializeObject<List<Biz.Model.CourseTab>>(response.Content, settings));


            string link = NextLink(response);
            while (link.Length != 0)
            {
                request = new RestRequest(link.Substring(1, link.Length - 2).Substring(WebcoursesUri.Length + 1), Method.GET);
                addAuth(ref request);

                response = client.Execute(request);
                rtnValue.AddRange(JsonConvert.DeserializeObject<List<Biz.Model.CourseTab>>(response.Content, settings));
                link = NextLink(response);

            }

            return rtnValue;



        }


        public List<Biz.Model.EnrollmentReportActReport> getEnrollmentDetails(string accountID, string enrollment_term_id)
        {

            List<Biz.Model.EnrollmentReportActReport> rtnValue = new List<Model.EnrollmentReportActReport>();

            //  List<Biz.Model.EnrollmentRecordPlus> rtnValue = new List<Model.EnrollmentRecordPlus>();
            List<Biz.Model.Course> Courses = new List<Model.Course>();

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            RestClient client = new RestClient(WebcoursesUri);

            RestRequest request = new RestRequest();

            if (enrollment_term_id == "-1")
                request = new RestRequest(string.Format(cPUBLISHED_COURSES_ALL, accountID), Method.GET);
            else
                request = new RestRequest(string.Format(cPUBLISHED_COURSES, accountID, enrollment_term_id), Method.GET);


            addAuth(ref request);

            var response = client.Execute(request);


            Courses.AddRange(JsonConvert.DeserializeObject<List<Biz.Model.Course>>(response.Content, settings));

            string link = NextLink(response);
            while (link.Length != 0)
            {
                request = new RestRequest(link.Substring(1, link.Length - 2).Substring(WebcoursesUri.Length + 1), Method.GET);
                addAuth(ref request);

                response = client.Execute(request);
                Courses.AddRange(JsonConvert.DeserializeObject<List<Biz.Model.Course>>(response.Content, settings));
                link = NextLink(response);

            }


            System.Diagnostics.Debug.Print("Course Pull Done!");
            System.Diagnostics.Debug.Print("-Course Count = " + Courses.Count.ToString());
            int i = 0;

            List<Model.Account> accounts = getAccounts("1");
            accounts.Add(getAccount(1));

            foreach (Biz.Model.Course item in Courses)
            {

                Biz.Model.Account account = getAccount(item.account_id);
                item.account_name = account.name;
                item.path = GetAccountPath(item.account_id, accounts);

                if (account.parent_account_id != null)
                {
                    item.account_parent_id = account.parent_account_id.ToString();
                    item.account_parent_name = getAccount(account.parent_account_id).name;
                }


                foreach (Biz.Model.EnrollmentRecord eR in getEnrollmentsbyType(item.id.ToString(), "Teacher"))
                {
                    Biz.Model.EnrollmentRecordPlus erp = new Model.EnrollmentRecordPlus(eR);
                    erp.CourseInfo = item;
                    erp.UserEmail = getProfile(erp.user_id.ToString()).primary_email;

                    rtnValue.Add(new Biz.Model.EnrollmentReportActReport(erp));


                }
                System.Diagnostics.Debug.Print(i.ToString());
                i++;

            }


            return rtnValue;
        }


        public Biz.Model.Account getAccount(int id)
        {
            Biz.Model.Account rtnValue = new Model.Account();

            if (AccountCache.Count(e => e.id == id) > 0)
                return AccountCache.First(e => e.id == id);

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            RestClient client = new RestClient(WebcoursesUri);
            RestRequest request = new RestRequest(string.Format(cACCOUNT, id), Method.GET);
            addAuth(ref request);

            var response = client.Execute(request);


            rtnValue = JsonConvert.DeserializeObject<Biz.Model.Account>(response.Content, settings);

            AccountCache.Add(rtnValue);
            return rtnValue;


        }

        public Biz.Model.Profile getProfile(string id)
        {
            Biz.Model.Profile rtnValue = new Model.Profile();
            int ID = int.Parse(id);
            if (ProfileCache.Count(e => e.id == ID) > 0)
                return ProfileCache.First(e => e.id == ID);

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            RestClient client = new RestClient(WebcoursesUri);
            RestRequest request = new RestRequest(string.Format(cPROFILE, id), Method.GET);
            addAuth(ref request);

            var response = client.Execute(request);


            rtnValue = JsonConvert.DeserializeObject<Biz.Model.Profile>(response.Content, settings);

            ProfileCache.Add(rtnValue);
            return rtnValue;


        }

        public List<Biz.Model.CourseEnrollment> getEnrollments(string id)
        {


            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            RestClient client = new RestClient(WebcoursesUri);
            RestRequest request = new RestRequest(string.Format(cENROLLMENT, id), Method.GET);
            addAuth(ref request);

            var response = client.Execute(request);

            List<Biz.Model.CourseEnrollment> rtnValue = new List<Model.CourseEnrollment>();
            rtnValue.AddRange(JsonConvert.DeserializeObject<List<Biz.Model.CourseEnrollment>>(response.Content, settings));


            string link = NextLink(response);
            while (link.Length != 0)
            {
                request = new RestRequest(link.Substring(1, link.Length - 2).Substring(WebcoursesUri.Length + 1), Method.GET);
                addAuth(ref request);

                response = client.Execute(request);
                rtnValue.AddRange(JsonConvert.DeserializeObject<List<Biz.Model.CourseEnrollment>>(response.Content, settings));
                link = NextLink(response);

            }

            return rtnValue;





        }

        public List<Biz.Model.EnrollmentRecord> getEnrollmentsbyType(string id, string role)
        {
            List<Biz.Model.EnrollmentRecord> rtnValue = new List<Model.EnrollmentRecord>();

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            RestClient client = new RestClient(WebcoursesUri);
            RestRequest request = new RestRequest(string.Format(cENROLLMENTBYTYPE, id, role), Method.GET);
            addAuth(ref request);

            var response = client.Execute(request);


            rtnValue.AddRange(JsonConvert.DeserializeObject<List<Biz.Model.EnrollmentRecord>>(response.Content, settings));


            string link = NextLink(response);
            while (link.Length != 0)
            {
                request = new RestRequest(link.Substring(1, link.Length - 2).Substring(WebcoursesUri.Length + 1), Method.GET);
                addAuth(ref request);

                response = client.Execute(request);
                rtnValue.AddRange(JsonConvert.DeserializeObject<List<Biz.Model.EnrollmentRecord>>(response.Content, settings));
                link = NextLink(response);

            }

            return rtnValue;

        }

        public List<Biz.Model.PubProfile> getPublishedTeachers(List<Biz.Model.Course> courses)
        {
            List<Biz.Model.Profile> Profiles = new List<Model.Profile>();
            foreach (Biz.Model.Course cs in courses)
                foreach (Biz.Model.Teacher ts in cs.teachers)
                    if (Profiles.Count(e => e.id == ts.id) == 0)
                        Profiles.Add(getProfile(ts.id.ToString()));

            List<Biz.Model.PubProfile> rtnValue = new List<Model.PubProfile>();
            foreach (Biz.Model.Profile profile in Profiles)
                rtnValue.Add(new Model.PubProfile(profile.id, profile.name, profile.primary_email, profile.sis_login_id));

            return rtnValue;
        }

        public List<Biz.Model.Account> getAccounts(string rootAccount)
        {
            List<Biz.Model.Account> rtnValue = new List<Model.Account>();

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            RestClient client = new RestClient(WebcoursesUri);
            RestRequest request = new RestRequest(string.Format(cAccountList, rootAccount), Method.GET);
            addAuth(ref request);

            var response = client.Execute(request);


            rtnValue.AddRange(JsonConvert.DeserializeObject<List<Biz.Model.Account>>(response.Content, settings));


            string link = NextLink(response);
            while (link.Length != 0)
            {
                request = new RestRequest(link.Substring(1, link.Length - 2).Substring(WebcoursesUri.Length + 1), Method.GET);
                addAuth(ref request);

                response = client.Execute(request);
                rtnValue.AddRange(JsonConvert.DeserializeObject<List<Biz.Model.Account>>(response.Content, settings));
                link = NextLink(response);

            }

            rtnValue.Add(getAccount(int.Parse(rootAccount)));
            return rtnValue;



        }

        public string GetAccountPath(int AccountID, List<Biz.Model.Account> accounts)
        {
            string rtnValue = "";
            int Parent = AccountID;
            try
            {
                do
                {
                    Biz.Model.Account account = accounts.First(e => e.id == Parent);
                    if (rtnValue.Length > 0)
                        rtnValue = account.name + "\\" + rtnValue;
                    else rtnValue = account.name;

                    Parent = account.parent_account_id;

                } while (Parent > 0);
            }
            catch (System.Exception ee)
            {
                System.Console.WriteLine(ee.ToString());
            }
            return rtnValue;

        }

        public List<Biz.Model.Admin> getAdmins(string rootAccount)
        {
            List<Biz.Model.Admin> admins = new List<Model.Admin>();
            List<Biz.Model.Account> accounts = getAccounts(rootAccount);

            foreach (Biz.Model.Account item in accounts)
            {
                admins.AddRange(getAccountAdmins(item, accounts));

            }



            return admins;

        }

        public List<Biz.Model.Admin> getAccountAdmins(Biz.Model.Account account, List<Biz.Model.Account> accounts)
        {


            List<Biz.Model.Admin> rtnValue = new List<Model.Admin>();

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            RestClient client = new RestClient(WebcoursesUri);
            RestRequest request = new RestRequest(string.Format(cAdminList, account.id.ToString()), Method.GET);
            addAuth(ref request);

            var response = client.Execute(request);


            rtnValue.AddRange(JsonConvert.DeserializeObject<List<Biz.Model.Admin>>(response.Content, settings));


            string link = NextLink(response);
            while (link.Length != 0)
            {
                request = new RestRequest(link.Substring(1, link.Length - 2).Substring(WebcoursesUri.Length + 1), Method.GET);
                addAuth(ref request);

                response = client.Execute(request);
                rtnValue.AddRange(JsonConvert.DeserializeObject<List<Biz.Model.Admin>>(response.Content, settings));
                link = NextLink(response);

            }

            foreach (Biz.Model.Admin item in rtnValue)
            {
                item.account = account;
                try
                {
                    item.Email = getProfile((item.user.id.ToString())).primary_email;
                    item.account.parent_account_name = accounts.First(e => e.id == account.parent_account_id).name;
                    item.account.account_path = GetAccountPath(item.account.id, accounts);

                }
                catch (System.Exception e) { }
            }

            return rtnValue;




        }


        public Biz.Model.MetricReport getMetrics(string accountID, string enrollment_term_id)
        {
            Biz.Model.MetricReport rtnValue = new Model.MetricReport();
            List<Biz.Model.Course> courses = new List<Model.Course>();

            
            List<string> teachers = new List<string>();
            List<string> students = new List<string>();
            

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            string url = string.Format(cCOURSES_ALL, accountID);

            if (enrollment_term_id != "-1")
                url = url + "&published=true&enrollment_term_id=" + enrollment_term_id;


            RestClient client = new RestClient(WebcoursesUri);
            RestRequest request = new RestRequest( url, Method.GET);
            addAuth(ref request);

            var response = client.Execute(request);


            courses.AddRange(JsonConvert.DeserializeObject<List<Biz.Model.Course>>(response.Content, settings));

            string link = NextLink(response);
            while (link.Length != 0)
            {
                request = new RestRequest(link.Substring(1, link.Length - 2).Substring(WebcoursesUri.Length + 1), Method.GET);
                addAuth(ref request);

                response = client.Execute(request);
                courses.AddRange(JsonConvert.DeserializeObject<List<Biz.Model.Course>>(response.Content, settings));
                link = NextLink(response);

            }



            foreach (Biz.Model.Course item in courses)
            {
             //   Biz.Model.Account account = getAccount(item.account_id);
             //   item.account_name = account.name;


             //       item.account_parent_id = account.parent_account_id.ToString();
             //       item.account_parent_name = getAccount(account.parent_account_id).name;

                List<Biz.Model.EnrollmentRecord> _teachers = getEnrollmentsbyType(item.id.ToString(), "Teacher");

                List<Biz.Model .EnrollmentRecord> _students = getEnrollmentsbyType(item.id.ToString(), "Student");
                //  item.cntObservers = getEnrollmentsbyType(item.id.ToString(), "Observers").Count;

                foreach (Biz.Model.EnrollmentRecord _item in _teachers)
                {
                    if (teachers.Contains(_item.user.login_id) == false)
                        teachers.Add(_item.user.login_id);


                }

                foreach (Biz.Model.EnrollmentRecord _item in _students)
                {
                    if (students.Contains(_item.user.login_id) == false)
                        students.Add(_item.user.login_id);

                }

            }

            rtnValue.term = enrollment_term_id;
            rtnValue.courses_created = courses.Count();
            rtnValue.published_courses = courses.Count(e => e.workflow_state == "available");
            rtnValue.Unique_teachers = teachers.Count();
            rtnValue.Unique_students = students.Count();


         return rtnValue;
        }
    }


}

