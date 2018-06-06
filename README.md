# UW C# Canvas Bridge Library

## Introduction
This library utilizes the C#/ .NET 4.5.2 stack and is intended to bridge the gap between Campus systems and the Canvas LMS.  

The solution file contains two products; 
* CanvasData - Library
* WebTestHarness - Implements the library and can be used to demonstrate and test implemented functions. Extremly Basic!

#### The following are important Canvas conventations.

* accountID - This is the organizational identifier and sets the scope where the a call is made.  The top level account is "1".
* userID - Internal Canvas user id.
* sis_user_id - Unidenifer to used to link the user account to the student infomormation system.  In UW-Madisons ucase this is the users PVI
* integration_id - This a secondary sis identifier.  For UW-Madison this will be the user's netID.
* sis_login_id / login_id - SIS linked login ID, this is linked to the users EPPN.



To implement the library you must have a valid token and URL for Canvas. API tokens should be set to expire! Please don't leave the application on auto pilot.  

### Basic intitialization
The following is an example is how to initialize the library and make a call

```c#
  public ContentResult SearchUsers(string id, string searchterm)
        {
            // Model Class to hold return data        
            List<CanvasData.Biz.Model.User> Users;
            
            
            // Pull API Token and URL. This example is pulling the webConfig in a MVC based project
            string token = WebConfigurationManager.AppSettings["CanvasToken"].ToString();
            string webURL = WebConfigurationManager.AppSettings["CavasURL"].ToString();

            // Intialize Biz Class
            CanvasData.Biz.canvasClient client = new CanvasData.Biz.canvasClient(webURL, token);
            
            
            // Perform User Search and store.
            Users = client.SearchUsers(id.ToString(), searchterm);
            
            ////////////////////////////////////////////////////////////////////////////////
            // Note you this portion is only being done to display the result to the user.
            // Turn User Class back to JSON to display to user
            ///////////////////////////////////////////////////////////////////////////////
            // If you are presenting data back to the users via JSON UI, do not use the JSONResult, it does not handle large result sets well.
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            serializer.MaxJsonLength = Int32.MaxValue;

            var result = new ContentResult
            {
                Content = serializer.Serialize(Users),
                ContentType = "application/json"

            };

            return result;
       
        }
```

## Biz Functions

### ```SearchUsers (string accountID, string SearchTerm)```

* Function returns a POCO Canvas User object - minus enrollments.  https://canvas.instructure.com/doc/api/users.html
* *TestWebHarness* Call: http://yourwebserver/data/SearchUsers?id=1&searchterm=UserXYZ
* Search Term will accept partial name or full ID of the users to match and return in the results list. Must be at least 3 characters. The API will prefer matching on canonical user ID if the ID has a numeric form. It will only search against other fields if non-numeric in form, or if the numeric value doesn't yield any matches. Queries by administrative users will search on SIS ID, login ID, name, or email address; non-administrative queries will only be compared against name.</mark>

### ```EnrollUser (string CourseID, string userID, string sectionID)```

* Function returns a POCO Canvas Enrollment object. https://canvas.instructure.com/doc/api/enrollments.html
* *TestWebHarness* Call: http://yourwebserver/data/EnrollUser?courseID=111111&userID=3
* **Note: The function can be executed to determine a users final grade**
* sectionID is not required and can be passed as a null value.  If not included Canvas will create a section if one does not exist.

### ```GetCourseEnrollmentWithGrades (string CourseID, string search_term)```

* Function returns List of POCO Canvas Enrollment objects. https://canvas.instructure.com/doc/api/enrollments.html
* *testWebHarness* Call: http://yourwebserver/data/GetCourseEnrollmentsWithGrades?courseID=73706
* search_term is optional, The partial name or full ID of the users to match and return in the results list.
* Grades will be nested in the enrollment object.


### ```GetCourseQuizzes (string CourseID)```

* **Note: The orginal Quiz tool will be replaced in the upcoming year, with an LTI tool.  Use Assignment Methods. **
* Function returns a List of POCO Canvas Quiz objects. https://canvas.instructure.com/doc/api/quizzes.html#method.quizzes/quizzes_api.index
* *testWebHarness* Call: http://yourwebserver/data/GetCourseQuizzes?courseID=73706

### ```GetCourseQuizSubmissions(string courseID, string quizID)```
* **Note: The orginal Quiz tool will be replaced in the upcoming year, with an LTI tool.  Use Assignment Methods. **
* Function returns a List of POCO Canvas Quiz objects. 
* Function returns a List of POCO Canvas Quiz Submission Objects.  https://canvas.instructure.com/doc/api/quiz_submissions.html
* *testWebHarness* Call: http://yourwebserver/data/GetCourseQuizSubmissions?courseID=51177&quizID=32665

### ```GetCourseAssignments (string CourseID)```

* Function returns a List of POCO Canvas Assignment objects. https://canvas.instructure.com/doc/api/assignments.html
* *testWebHarness* Call: http://yourwebserver/data/GetCourseAssignments?courseID=73706

### ```GetCourseSubmissions(string courseID, string AssignmentID)```

* Function returns a List of POCO Canvas Submission Objects.  https://canvas.instructure.com/doc/api/submissions.html
* *testWebHarness* Call: http://yourwebserver/data/GetCourseSubmissions?courseID=51177&AssignmentID=32665
