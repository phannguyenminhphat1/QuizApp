using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuizApp.Models;

namespace QuizApp.Controllers
{
    public class HomeController : Controller
    {
        QuizAppEntities db = new QuizAppEntities();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        //Giáo viên
        public ActionResult tlogin()
        {
            return View();
        }
        [HttpPost]
        //Giáo viên
        public ActionResult tlogin(tbl_admin ad)
        {
            tbl_admin a = db.tbl_admin.Where(x => x.ad_name == ad.ad_name && x.ad_pass == ad.ad_pass).SingleOrDefault();
            if (a!=null)
            {
                Session["ad_id"] = ad.ad_id;
                return RedirectToAction("Dashboard");
            }
            else
            {
                ViewBag.msg = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();
        }

        //Học sinh
        public ActionResult slogin()
        {
            return View();
        }
        

        //Dashboard
        public ActionResult Dashboard()
        {
            return View();
        }

        //Category
        [HttpGet]
        public ActionResult Add_Category()
        {
            Session["ad_id"] = 1; // remove soon

            int ad_id = Convert.ToInt32(Session["ad_id"].ToString());
            List<tbl_category> catLi = db.tbl_category.Where(x => x.cat_fk_ad_id == ad_id).OrderByDescending(x => x.cat_id).ToList();
            ViewData["list"] = catLi;
            return View();
        }

        [HttpPost]
        public ActionResult Add_Category(tbl_category cat)
        {


            List<tbl_category> catLi = db.tbl_category.OrderByDescending(x => x.cat_id).ToList();
            ViewData["list"] = catLi;


            tbl_category c = new tbl_category();
            c.cat_name = cat.cat_name;
            c.cat_fk_ad_id = Convert.ToInt32(Session["ad_id"].ToString());


            db.tbl_category.Add(c);
            db.SaveChanges();
            return RedirectToAction("Add_Category");
        }

        //AddQuestions
        [HttpGet]
        public ActionResult Add_Questions()
        {
            int s_id = Convert.ToInt32(Session["ad_id"]);
            List<tbl_category> li = db.tbl_category.Where(x => x.cat_fk_ad_id == s_id).ToList();
            ViewBag.list = new SelectList(li, "cat_id", "cat_name");

            return View();
        }
        [HttpPost]
        public ActionResult Add_Questions(tbl_question q)
        {
            int s_id = Convert.ToInt32(Session["ad_id"]);
            List<tbl_category> li = db.tbl_category.Where(x => x.cat_fk_ad_id == s_id).ToList();
            ViewBag.list = new SelectList(li, "cat_id", "cat_name");
            tbl_question qa = new tbl_question();
            qa.q_text = q.q_text;
            qa.QA = q.QA;
            qa.QB = q.QB;
            qa.QC = q.QC;
            qa.QD = q.QD;
            qa.QCorrectAns = q.QCorrectAns;
            qa.q_fk_catid = q.q_fk_catid;
            db.tbl_question.Add(qa);  
            db.SaveChanges();
            ViewBag.ms = "Thêm mới câu hỏi thành công";


            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}