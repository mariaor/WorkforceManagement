using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WorkforceManagementClient.Models;

namespace WorkforceManagementClient.Controllers
{
    public class SkillController : Controller
    {
        // GET: skill
        public ActionResult Index()
        {
            IEnumerable<SkillViewModel> skills = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["APIURL"]);
                //HTTP GET
                var responseTask = client.GetAsync("skills");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<SkillViewModel>>();
                    readTask.Wait();

                    skills = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    skills = Enumerable.Empty<SkillViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(skills);
        }

        // GET: skill/details/5
        public ActionResult Details(int id)
        {
            SkillViewModel skill = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["APIURL"]);
                //HTTP GET
                var responseTask = client.GetAsync("skills/" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    Task<String> objResponse1 = result.Content.ReadAsStringAsync();

                    skill = JsonConvert.DeserializeObject<SkillViewModel>(objResponse1.Result.ToString());

                }
                else //web api sent error response 
                {
                    //log response status here..

                    skill = null;

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(skill);
        }

        // GET: skill/create
        public ActionResult Create()
        {
            return View();
        }

        // POST: skill/create
        [HttpPost]
        public ActionResult Create(SkillViewModel skill)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    //HTTP POST
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["APIURL"]);

                    var postTask = client.PostAsJsonAsync<SkillViewModel>("skills", skill);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else //web api sent error response 
                    {
                        //log response status here..
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }

                }
                return View(skill);
            }
            catch
            {
                return View(skill);
            }
        }

        // GET: skill/edit/5
        public ActionResult Edit(int id)
        {
            SkillViewModel skill = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["APIURL"]);
                //HTTP GET
                var responseTask = client.GetAsync("skills/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<SkillViewModel>();
                    readTask.Wait();

                    skill = readTask.Result;
                }
            }

            return View(skill);
        }

        // POST: skill/edit/5
        [HttpPost]
        public ActionResult Edit(SkillViewModel skill)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["APIURL"]);

                //HTTP PUT
                var putTask = client.PutAsJsonAsync<SkillViewModel>("skills/" + skill.SkillID, skill);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(skill);
        }

        // GET: skill/delete/5
        public ActionResult Delete(int id)
        {
            SkillViewModel skill = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["APIURL"]);
                //HTTP GET
                var responseTask = client.GetAsync("skills/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<SkillViewModel>();
                    readTask.Wait();

                    skill = readTask.Result;
                }
            }

            return View(skill);
        }

        // POST: skill/delete/5
        [HttpPost]
        public ActionResult Delete(SkillViewModel skill)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["APIURL"]);

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("skills/" + skill.SkillID);
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }
    }
}
