using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasData.Biz.Model
{
    public class Teacher
    {
        public int id { get; set; }
        public string display_name { get; set; }
        public string avatar_image_url { get; set; }
        public string html_url { get; set; }
    }

    public class Calendar
    {
        public string ics { get; set; }
    }

    public class Account
    {
        public int id { get; set; }
        public string name { get; set; }
        public string workflow_state { get; set; }
        public int parent_account_id { get; set; }
        public int root_account_id { get; set; }
        public int default_storage_quota_mb { get; set; }
        public int default_user_storage_quota_mb { get; set; }
        public int default_group_storage_quota_mb { get; set; }
        public string default_time_zone { get; set; }

        public string parent_account_name { get; set; }
        public string account_path { get; set; }
    }
    public class Course
    {
        public int id { get; set; }
        public string name { get; set; }

        public string path { get; set; }
        public int account_id { get; set; }

        public string account_name { get; set; }
        public string account_parent_id { get; set; }

        public string account_parent_name { get; set; }
        public string start_at { get; set; }
        public int? grading_standard_id { get; set; }
        public bool is_public { get; set; }
        public string course_code { get; set; }
        public string default_view { get; set; }
        public int root_account_id { get; set; }
        public int enrollment_term_id { get; set; }
        public string end_at { get; set; }
        public bool public_syllabus { get; set; }
        public int storage_quota_mb { get; set; }
        public bool is_public_to_auth_users { get; set; }
        public bool apply_assignment_group_weights { get; set; }
        public List<Teacher> teachers { get; set; }

        public string teacherList
        {
            get
            {
                string rtnValue = "";
                foreach (Teacher item in teachers)
                    rtnValue = rtnValue + item.display_name + ", ";
                return rtnValue;
            }
        }
        public Calendar calendar { get; set; }
        public string time_zone { get; set; }
        public string sis_course_id { get; set; }
        public object integration_id { get; set; }
        public bool hide_final_grades { get; set; }
        public string workflow_state { get; set; }
        public bool restrict_enrollments_to_course_dates { get; set; }

        public int cntTeachers { get; set; }
        public int cntTAs { get; set; }
        public int cntStudents { get; set; }
        public int cntObservers { get; set; }
        public int cntDesigners { get; set; }

        public List<CourseTab> Tabs { get; set; }
        public string SyllabusLink { get; set; }
    }

    public class Profile
    {
        public int id { get; set; }
        public string name { get; set; }
        public string short_name { get; set; }
        public string sortable_name { get; set; }
        public string sis_user_id { get; set; }
        public string sis_login_id { get; set; }
        public string login_id { get; set; }
        public string avatar_url { get; set; }
        public string integration_id { get; set; }
        public object title { get; set; }
        public object bio { get; set; }
        public string primary_email { get; set; }
        public string time_zone { get; set; }
        public object locale { get; set; }

        // Not part of intial canvas class

        public int cntTeachers { get; set; }
        public int cntTAs { get; set; }
        public int ctnStudents { get; set; }
        public int cntDesigners { get; set; }

        public int cntObserver { get; set; }

    }

    public class PubProfile
    {
        public int id { get; set; }
        public string name { get; set; }
        public string primary_email { get; set; }

        public string netID { get; set; }

        public PubProfile(int ID, string Name, string Primary_email, string NetID)
        {
            id = ID;
            name = Name;
            primary_email = Primary_email;
            netID = NetID;

        }
    }

    public class CourseEnrollment
    {

        public int id { get; set; }
        public string name { get; set; }
        public string sortable_name { get; set; }
        public string short_name { get; set; }
        public string sis_user_id { get; set; }
        public string integration_id { get; set; }
        public string sis_login_id { get; set; }
        public int sis_import_id { get; set; }
        public string login_id { get; set; }
        public string email { get; set; }


    }


    public class User
    {


        public int id { get; set; }
        public string name { get; set; }
        public string sortable_name { get; set; }
        public string short_name { get; set; }
        public string sis_user_id { get; set; }
        public string integration_id { get; set; }
        public string sis_login_id { get; set; }
        public int sis_import_id { get; set; }
        public string login_id { get; set; }
        public EnrollmentRecord enrollments { get; set; }
    }

    public class Grades
    {
        // Changed score types to string
        //11-27-2017

        public string html_url { get; set; }
        public string current_score { get; set; }
        public string current_grade { get; set; }
        public string final_score { get; set; }
        public string final_grade { get; set; }

    }
    public class EnrollmentRecord
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public int course_id { get; set; }
        public string type { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public object associated_user_id { get; set; }
        public object start_at { get; set; }
        public object end_at { get; set; }
        public int course_section_id { get; set; }
        public int root_account_id { get; set; }
        public bool limit_privileges_to_course_section { get; set; }
        public string enrollment_state { get; set; }
        public string role { get; set; }
        public int role_id { get; set; }
        public string last_activity_at { get; set; }
        public int total_activity_time { get; set; }
        public int sis_import_id { get; set; }
        public string sis_account_id { get; set; }
        public string sis_course_id { get; set; }
        public object course_integration_id { get; set; }
        public string sis_section_id { get; set; }
        public object section_integration_id { get; set; }
        public string sis_user_id { get; set; }
        public string html_url { get; set; }

        public Grades grades { get; set; }
        public User user { get; set; }
    }

    public class CourseEnrollments
    {
        public int id { get; set; }
        public string name { get; set; }
        public string sortable_name { get; set; }
        public string short_name { get; set; }
        public string sis_user_id { get; set; }
        public string integration_id { get; set; }
        public string sis_login_id { get; set; }
        public int? sis_import_id { get; set; }
        public string login_id { get; set; }
        public List<EnrollmentRecord> enrollments { get; set; }
        public string analytics_url { get; set; }
    }

    public class AllDate
    {
        public DateTime? due_at { get; set; }
        public DateTime? unlock_at { get; set; }
        public DateTime? lock_at { get; set; }
        public bool @base { get; set; }
    }

    public class Permissions
    {
        public bool read_statistics { get; set; }
        public bool manage { get; set; }
        public bool read { get; set; }
        public bool update { get; set; }
        public bool create { get; set; }
        public bool submit { get; set; }
        public bool preview { get; set; }
        public bool delete { get; set; }
        public bool grade { get; set; }
        public bool review_grades { get; set; }
        public bool view_answer_audits { get; set; }
        public bool attach { get; set; }

        public bool reply { get; set; }

    }

    public class DiscussionTopic
    {
        public int id { get; set; }
        public string title { get; set; }
        public object last_reply_at { get; set; }
        public object delayed_post_at { get; set; }
        public object posted_at { get; set; }
        public int assignment_id { get; set; }
        public object root_topic_id { get; set; }
        public int position { get; set; }
        public bool podcast_has_student_posts { get; set; }
        public string discussion_type { get; set; }
        public object lock_at { get; set; }
        public bool allow_rating { get; set; }
        public bool only_graders_can_rate { get; set; }
        public bool sort_by_rating { get; set; }
        public object user_name { get; set; }
        public int discussion_subentry_count { get; set; }
        public Permissions permissions { get; set; }
        public object require_initial_post { get; set; }
        public bool user_can_see_posts { get; set; }
        public object podcast_url { get; set; }
        public string read_state { get; set; }
        public int unread_count { get; set; }
        public bool subscribed { get; set; }
        public List<object> topic_children { get; set; }
        public List<object> attachments { get; set; }
        public bool published { get; set; }
        public bool can_unpublish { get; set; }
        public bool locked { get; set; }
        public bool can_lock { get; set; }
        public bool comments_disabled { get; set; }
        public object author { get; set; }
        public string html_url { get; set; }
        public string url { get; set; }
        public bool pinned { get; set; }
        public object group_category_id { get; set; }
        public bool can_group { get; set; }
        public bool locked_for_user { get; set; }
        public string message { get; set; }
    }


    public class Quiz
    {
        public int id { get; set; }
        public string title { get; set; }
        public string html_url { get; set; }
        public string mobile_url { get; set; }
        public string description { get; set; }
        public string quiz_type { get; set; }
        public object time_limit { get; set; }
        public bool shuffle_answers { get; set; }
        public bool show_correct_answers { get; set; }
        public string scoring_policy { get; set; }
        public int allowed_attempts { get; set; }
        public bool one_question_at_a_time { get; set; }
        public int question_count { get; set; }
        public decimal points_possible { get; set; }
        public bool cant_go_back { get; set; }
        public object access_code { get; set; }
        public object ip_filter { get; set; }
        public DateTime? due_at { get; set; }
        public DateTime? lock_at { get; set; }
        public DateTime? unlock_at { get; set; }
        public bool published { get; set; }
        public bool unpublishable { get; set; }
        public bool locked_for_user { get; set; }
        public object hide_results { get; set; }
        public object show_correct_answers_at { get; set; }
        public object hide_correct_answers_at { get; set; }
        public List<AllDate> all_dates { get; set; }
        public bool can_unpublish { get; set; }
        public bool can_update { get; set; }
        public bool require_lockdown_browser { get; set; }
        public bool require_lockdown_browser_for_results { get; set; }
        public bool require_lockdown_browser_monitor { get; set; }
        public string lockdown_browser_monitor_data { get; set; }
        public string speed_grader_url { get; set; }
        public Permissions permissions { get; set; }
        public string quiz_reports_url { get; set; }
        public string quiz_statistics_url { get; set; }
        public string message_students_url { get; set; }
        public int section_count { get; set; }
        public string quiz_submission_versions_html_url { get; set; }
        public int? assignment_id { get; set; }
        public bool one_time_results { get; set; }
        public bool only_visible_to_overrides { get; set; }
        public int assignment_group_id { get; set; }
        public bool show_correct_answers_last_attempt { get; set; }
        public int version_number { get; set; }
        public List<string> question_types { get; set; }
        public bool has_access_code { get; set; }
        public bool? post_to_sis { get; set; }
    }


    public class Assignment
    {
        public int id { get; set; }
        public string description { get; set; }
        public DateTime? due_at { get; set; }
        public DateTime? unlock_at { get; set; }
        public DateTime? lock_at { get; set; }
        public decimal points_possible { get; set; }
        public string grading_type { get; set; }
        public int assignment_group_id { get; set; }
        public object grading_standard_id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public bool peer_reviews { get; set; }
        public bool automatic_peer_reviews { get; set; }
        public int position { get; set; }
        public bool grade_group_students_individually { get; set; }
        public bool anonymous_peer_reviews { get; set; }
        public object group_category_id { get; set; }
        public bool post_to_sis { get; set; }
        public bool moderated_grading { get; set; }
        public bool omit_from_final_grade { get; set; }
        public bool intra_group_peer_reviews { get; set; }
        public bool anonymous_instructor_annotations { get; set; }
        public string secure_params { get; set; }
        public int course_id { get; set; }
        public string name { get; set; }
        public List<string> submission_types { get; set; }
        public bool has_submitted_submissions { get; set; }
        public bool due_date_required { get; set; }
        public int max_name_length { get; set; }
        public bool in_closed_grading_period { get; set; }
        public bool is_quiz_assignment { get; set; }
        public ExternalToolTagAttributes external_tool_tag_attributes { get; set; }
        public bool muted { get; set; }
        public string html_url { get; set; }
        public bool has_overrides { get; set; }
        public string url { get; set; }
        public int needs_grading_count { get; set; }
        public object integration_id { get; set; }
        public object integration_data { get; set; }
        public bool published { get; set; }
        public bool unpublishable { get; set; }
        public bool only_visible_to_overrides { get; set; }
        public bool locked_for_user { get; set; }
        public string submissions_download_url { get; set; }
        public int? quiz_id { get; set; }
        public bool? anonymous_submissions { get; set; }
        public bool? use_rubric_for_grading { get; set; }
        public bool? free_form_criterion_comments { get; set; }
        public List<Rubric> rubric { get; set; }
        public RubricSettings rubric_settings { get; set; }
        public DiscussionTopic discussion_topic { get; set; }
    }


    public class EnrollmentRecordPlus : EnrollmentRecord
    {
        public EnrollmentRecordPlus(EnrollmentRecord e)
        {
            id = e.id;
            user_id = e.user_id;
            this.associated_user_id = e.associated_user_id;
            this.course_id = e.course_id;
            this.course_integration_id = e.course_integration_id;
            this.course_section_id = e.course_section_id;
            this.created_at = e.created_at;
            this.end_at = e.end_at;
            this.enrollment_state = e.enrollment_state;
            this.html_url = e.html_url;
            this.last_activity_at = e.last_activity_at;
            this.limit_privileges_to_course_section = e.limit_privileges_to_course_section;
            this.role = e.role;
            this.role_id = e.role_id;
            this.root_account_id = e.root_account_id;
            this.section_integration_id = e.section_integration_id;
            this.sis_section_id = e.sis_section_id;
            this.sis_user_id = e.sis_user_id;
            this.start_at = e.start_at;
            this.total_activity_time = e.total_activity_time;
            this.type = e.type;
            this.updated_at = e.updated_at;
            this.user = e.user;
            this.user_id = e.user_id;


        }

        public String UserEmail { get; set; }
        public Course CourseInfo { get; set; }


    }

    public class EnrollmentReportActReport
    {
        public EnrollmentReportActReport(EnrollmentRecordPlus item)
        {
            this.account_name = item.CourseInfo.account_name;
            this.associated_user_id = item.associated_user_id;
            this.course_id = item.course_id;

            this.course_integration_id = item.course_integration_id;
            this.course_section_id = item.course_section_id;
            this.CourseName = item.CourseInfo.name;
            this.CourseSISID = item.CourseInfo.sis_course_id;
            this.created_at = item.created_at;
            this.email = item.UserEmail;
            this.end_at = item.end_at;
            this.enrollment_state = item.enrollment_state;
            this.enrollment_term_id = item.CourseInfo.enrollment_term_id.ToString();
            this.html_url = item.html_url;
            this.id = item.id;
            this.last_activity_at = item.last_activity_at;
            this.limit_privileges_to_course_section = item.limit_privileges_to_course_section;
            this.name = item.user.name;
            this.parent_name = item.CourseInfo.account_parent_name;
            this.role = item.role;
            this.role_id = item.role_id;
            this.root_account_id = item.root_account_id;
            this.section_integration_id = item.section_integration_id;
            this.sis_account_id = item.sis_account_id;
            this.sis_course_id = item.course_id.ToString();
            this.sis_import_id = item.sis_import_id;
            this.sis_section_id = item.sis_section_id;
            this.sortable_name = item.user.sortable_name;
            this.start_at = item.start_at;
            this.total_activity_time = item.total_activity_time;
            this.type = item.type;
            this.updated_at = item.updated_at;
            this.user_id = item.user_id;
            this.path = item.CourseInfo.path;


        }


        public int id { get; set; }

        public int user_id { get; set; }

        public string name { get; set; }
        public string sortable_name { get; set; }

        public string sis_user_id { get; set; }
        public string email { get; set; }


        public int course_id { get; set; }

        public string CourseName { get; set; }
        public string CourseSISID { get; set; }

        public string path { get; set; }

        public string account_name { get; set; }
        public string parent_name { get; set; }

        public string enrollment_term_id { get; set; }
        public string type { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public object associated_user_id { get; set; }
        public object start_at { get; set; }
        public object end_at { get; set; }
        public int course_section_id { get; set; }
        public int root_account_id { get; set; }
        public bool limit_privileges_to_course_section { get; set; }
        public string enrollment_state { get; set; }
        public string role { get; set; }
        public int role_id { get; set; }
        public string last_activity_at { get; set; }
        public int total_activity_time { get; set; }
        public int sis_import_id { get; set; }
        public string sis_account_id { get; set; }
        public string sis_course_id { get; set; }
        public object course_integration_id { get; set; }
        public string sis_section_id { get; set; }
        public object section_integration_id { get; set; }

        public string html_url { get; set; }





    }
    public class Admin
    {
        public int id { get; set; }
        public string role { get; set; }
        public int role_id { get; set; }
        public User user { get; set; }

        public Account account { get; set; }

        public string Email { get; set; }
    }

    public class AdminRPT
    {
        public AdminRPT(Admin admin)
        {
            account_path = admin.account.account_path;
            account_name = admin.account.name;
            account_id = admin.account.id;
            parent_id = admin.account.parent_account_id;
            parent_name = admin.account.parent_account_name;
            user_id = admin.user.id;
            user_name = admin.user.name;
            user_sis_login_id = admin.user.sis_login_id;
            user_email = admin.Email;
            role = admin.role;


        }

        public string account_path { get; set; }
        public string account_name { get; set; }
        public int account_id { get; set; }
        public string parent_name { get; set; }

        public int parent_id { get; set; }

        public int user_id { get; set; }
        public string user_name { get; set; }
        public string user_sis_login_id { get; set; }
        public string user_email { get; set; }

        public string role { get; set; }


    }


    public class ExternalToolTagAttributes
    {
        public string url { get; set; }
        public bool new_tab { get; set; }
        public string resource_link_id { get; set; }
    }

    public class Rating
    {
        public string id { get; set; }
        public double points { get; set; }
        public string description { get; set; }
    }

    public class Rubric
    {
        public string id { get; set; }
        public int points { get; set; }
        public string description { get; set; }
        public string long_description { get; set; }
        public List<Rating> ratings { get; set; }
    }

    public class RubricSettings
    {
        public int id { get; set; }
        public string title { get; set; }
        public int points_possible { get; set; }
        public bool free_form_criterion_comments { get; set; }
    }

    public class Submission
    {
        public int id { get; set; }
        public object body { get; set; }
        public object url { get; set; }
        public string grade { get; set; }
        public int? score { get; set; }
        public object submitted_at { get; set; }
        public int assignment_id { get; set; }
        public int user_id { get; set; }
        public object submission_type { get; set; }
        public string workflow_state { get; set; }
        public bool grade_matches_current_submission { get; set; }
        public DateTime? graded_at { get; set; }
        public int? grader_id { get; set; }
        public object attempt { get; set; }
        public object cached_due_date { get; set; }
        public bool? excused { get; set; }
        public object late_policy_status { get; set; }
        public object points_deducted { get; set; }
        public object grading_period_id { get; set; }
        public bool late { get; set; }
        public bool missing { get; set; }
        public int seconds_late { get; set; }
        public string entered_grade { get; set; }
        public int? entered_score { get; set; }
        public string preview_url { get; set; }

        // These Values are not included in the base class.  Expanded to make more user friendly.
        public string name { get; set; }
        public string sortable_name { get; set; }
        public string short_name { get; set; }
        public string sis_user_id { get; set; }
        public string integration_id { get; set; }
        public string sis_login_id { get; set; }

        public string login_id { get; set; }

        public CourseEnrollment SetUser
        {
            set
            {
                name = value.name;
                sortable_name = value.sortable_name;
                short_name = value.short_name;
                sis_user_id = value.sis_user_id;
                integration_id = value.integration_id;
                sis_login_id = value.sis_login_id;
                login_id = value.login_id;


            }
        }

    }





    public class QuizSubmissionContainer
    {
        public List<QuizSubmission> quiz_submissions { get; set; }

    }

    public class QuizSubmission
    {
        public int id { get; set; }
        public int quiz_id { get; set; }
        public int quiz_version { get; set; }
        public int user_id { get; set; }



        public int submission_id { get; set; }
        public decimal score { get; set; }
        public decimal kept_score { get; set; }
        public DateTime started_at { get; set; }
        public DateTime end_at { get; set; }
        public DateTime finished_at { get; set; }
        public int attempt { get; set; }
        public string workflow_state { get; set; }
        public decimal fudge_points { get; set; }
        public decimal quiz_points_possible { get; set; }
        public object extra_attempts { get; set; }
        public object extra_time { get; set; }
        public object manually_unlocked { get; set; }
        public string validation_token { get; set; }
        public decimal score_before_regrade { get; set; }
        public bool has_seen_results { get; set; }
        public int time_spent { get; set; }
        public int attempts_left { get; set; }
        public bool overdue_and_needs_submission { get; set; }
        public bool __invalid_name__excused { get; set; }
        public string html_url { get; set; }
        public string result_url { get; set; }

        // These Values are not included in the base class.  Expanded to make more user friendly.
        public string name { get; set; }
        public string sortable_name { get; set; }
        public string short_name { get; set; }
        public string sis_user_id { get; set; }
        public string integration_id { get; set; }
        public string sis_login_id { get; set; }

        public string login_id { get; set; }

        public CourseEnrollment SetUser
        {
            set
            {
                name = value.name;
                sortable_name = value.sortable_name;
                short_name = value.short_name;
                sis_user_id = value.sis_user_id;
                integration_id = value.integration_id;
                sis_login_id = value.sis_login_id;
                login_id = value.login_id;


            }
        }
    }
    public class CourseTab
    {
        public string id { get; set; }
        public string html_url { get; set; }
        public string full_url { get; set; }
        public int position { get; set; }
        public string visibility { get; set; }
        public string label { get; set; }
        public string type { get; set; }
        public bool? unused { get; set; }
        public bool? hidden { get; set; }
        public string url { get; set; }

    }
}



