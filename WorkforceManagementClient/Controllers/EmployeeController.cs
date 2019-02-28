using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WorkforceManagementClient.Models;
using System.Web.Helpers;
using System.Web.Script.Serialization;
using Microsoft.AspNetCore.Cors;
using System.Configuration;
using System.Net;

namespace WorkforceManagementClient.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: employee
        public ActionResult Index()
        {
            IEnumerable<EmployeeViewModel> employees = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["APIURL"]);
                //HTTP GET
                var responseTask = client.GetAsync("employees");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<EmployeeViewModel>>();
                    readTask.Wait();

                    employees = readTask.Result;
                }
                else //web api sent error response 
                {
                    HttpStatusCode statuscode = result.StatusCode;

                    if (statuscode== HttpStatusCode.NotFound )
                    {
                        return View("NotFound");
                    }
                    else
                    {
                        return View("Error");
                    }
                }
            }
            return View(employees);
        }

        // GET: employee/details/5
        public ActionResult Details(int id)
        {
            EmployeeViewModel employee = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["APIURL"]);
                //HTTP GET
                var responseTask = client.GetAsync("employees/" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    Task<String> objResponse1 = result.Content.ReadAsStringAsync();

                    employee = JsonConvert.DeserializeObject<EmployeeViewModel>(objResponse1.Result.ToString());

                }
                else //web api sent error response 
                {
                    HttpStatusCode statuscode = result.StatusCode;

                    if (statuscode == HttpStatusCode.NotFound)
                    {
                        return View("NotFound");
                    }
                    else
                    {
                        return View("Error");
                    }
                }
            }
            return View(employee);
        }

        // GET: employee/create
        public ActionResult Create()
        {
            return View();
        }

        // POST: employee/create
        [HttpPost]
        public ActionResult Create(EmployeeViewModel emp)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    //HTTP POST
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["APIURL"]);

                    var postTask = client.PostAsJsonAsync<EmployeeViewModel>("employees", emp);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else //web api sent error response 
                    {
                        HttpStatusCode statuscode = result.StatusCode;

                        if (statuscode == HttpStatusCode.NotFound)
                        {
                            return View("NotFound");
                        }
                        else
                        {
                            return View("Error");
                        }
                    }
                    
                }
            }
            catch
            {
                return View("Error");
            }
        }

        // GET: employee/edit/5
        public ActionResult Edit(int id)
        {
            EmployeeViewModel emp = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["APIURL"]);
                //HTTP GET
                var responseTask = client.GetAsync("employees/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<EmployeeViewModel>();
                    readTask.Wait();

                    emp = readTask.Result;
                }
                else //web api sent error response 
                {
                    HttpStatusCode statuscode = result.StatusCode;

                    if (statuscode == HttpStatusCode.NotFound)
                    {
                        return View("NotFound");
                    }
                    else
                    {
                        return View("Error");
                    }
                }
            }

            return View(emp);
        }

        // POST: employee/edit
        [HttpPost]
        public ActionResult Edit(EmployeeViewModel emp)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["APIURL"]);

                //HTTP PUT
                var putTask = client.PutAsJsonAsync<EmployeeViewModel>("employees/" + emp.EmployeeID, emp);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
                else //web api sent error response 
                {
                    HttpStatusCode statuscode = result.StatusCode;

                    if (statuscode == HttpStatusCode.NotFound)
                    {
                        return View("NotFound");
                    }
                    else
                    {
                        return View("Error");
                    }
                }
            }
        }

        // GET: employee/delete/5
        public ActionResult Delete(int id)
        {
            EmployeeViewModel emp = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["APIURL"]);
                //HTTP GET
                var responseTask = client.GetAsync("employees/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<EmployeeViewModel>();
                    readTask.Wait();

                    emp = readTask.Result;
                }
                else //web api sent error response 
                {
                    HttpStatusCode statuscode = result.StatusCode;

                    if (statuscode == HttpStatusCode.NotFound)
                    {
                        return View("NotFound");
                    }
                    else
                    {
                        return View("Error");
                    }
                }
            }

            return View(emp);
        }

        // POST: employee/delete
        [HttpPost]
        public ActionResult Delete(EmployeeViewModel emp)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["APIURL"]);

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("employees/" + emp.EmployeeID);
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
                else //web api sent error response 
                {
                    HttpStatusCode statuscode = result.StatusCode;

                    if (statuscode == HttpStatusCode.NotFound)
                    {
                        return View("NotFound");
                    }
                    else
                    {
                        return View("Error");
                    }
                }
            }  
        }

        // GET: employee/skill/5
        [HttpGet]
        public ActionResult Skills(int id, string empsurname, string empfirstname)
        {
            IList<SkillViewModel> skills = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["APIURL"]);
                //HTTP GET
                var responseTask = client.GetAsync("employees/skills/?empid=" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    Task<String> objResponse1 = result.Content.ReadAsStringAsync();

                    skills = JsonConvert.DeserializeObject<IList<SkillViewModel>>(objResponse1.Result.ToString());

                    ViewData["employeeid"] = id;

                    ViewData["employeesurname"] = empsurname;

                    ViewData["employeefirstname"] = empfirstname;

                }
                else //web api sent error response 
                {
                    HttpStatusCode statuscode = result.StatusCode;

                    if (statuscode == HttpStatusCode.NotFound)
                    {
                        return View("NotFound");
                    }
                    else
                    {
                        return View("Error");
                    }
                }
            }
            return View("Skills", skills); 
        }

        // GET: employee/skill/delete/?empid=1&skillid=2
        public ActionResult SkillDelete(int empid, int skillid, string empsurname, string empfirstname)
        {
            IList<SkillViewModel> skill = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["APIURL"]);
                //HTTP GET
                var responseTask = client.GetAsync("employees/skills/?empid=" + empid + "&skillid=" + skillid);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<SkillViewModel>>();
                    readTask.Wait();

                    skill = readTask.Result;

                    ViewData["employeeid"] = empid;

                    ViewData["employeesurname"] = empsurname;

                    ViewData["employeefirstname"] = empfirstname;
                }
                else //web api sent error response 
                {
                    HttpStatusCode statuscode = result.StatusCode;

                    if (statuscode == HttpStatusCode.NotFound)
                    {
                        return View("NotFound");
                    }
                    else
                    {
                        return View("Error");
                    }
                }
            }

            if (skill.Count>0)
            {
                return View(skill[0]);
            }else
            {
                return View();
            }
        }

        // POST: employee/skills/?empid=1
        [HttpPost]
        public ActionResult SkillDelete(int empid, FormCollection form, SkillViewModel skill)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["APIURL"]);

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("employees/deleteskills/?empid=" + empid + "&skillid=" + skill.SkillID);
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Skills/" + empid, "Employee", new { empsurname = form.Get(1), empfirstname = form.Get(2) });
                }
                else //web api sent error response 
                {
                    HttpStatusCode statuscode = result.StatusCode;

                    if (statuscode == HttpStatusCode.NotFound)
                    {
                        return View("NotFound");
                    }
                    else
                    {
                        return View("Error");
                    }
                }
            }
        }

        // GET: employee/edit/5
        [HttpGet]
        public ActionResult AddSkills(int empid, string empsurname, string empfirstname)
        {
            IList<SkillViewModel> skills = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["APIURL"]);
                //HTTP GET
                var responseTask = client.GetAsync("employees/getemployeelackingskills/?empid=" + empid);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<SkillViewModel>>();
                    readTask.Wait();

                    ViewData["employeeid"] = empid;
                    skills = readTask.Result;

                    ViewData["employeesurname"] = empsurname;

                    ViewData["employeefirstname"] = empfirstname;
                }
                else //web api sent error response 
                {
                    HttpStatusCode statuscode = result.StatusCode;

                    if (statuscode == HttpStatusCode.NotFound)
                    {
                        return View("NotFound");
                    }
                    else
                    {
                        return View("Error");
                    }
                }
            }

            return View("AddSkills", skills);
        }

        // POST: employee/edit/5
        [HttpPost]
        public ActionResult AddNewSkills(int empid, FormCollection skills)
        {
            try
            {       
                List<Int32> lst = new List<Int32>();
                string[] skillids = skills.Get("SkillID").Split(',');
                string[] selected = skills.Get("Selected").Split(',');
                //for every checkbox in the table that is checked, two items are added in the selected array
                //the first that has value true and the second that has value false
                //for every unchecked checkbox in the table, only one item is added in the selected array
                //with the value false
                //we get the selected array, with the items as we described above, and we create a new array boolselected
                //were each item represents whether the corresponding item in the skills array (so, in the table) is selected or not
                Boolean[] boolselected = new Boolean[skillids.Length];
                int countselected = 0;

                for (int i = 0; i < skillids.Length; i++)
                {
                    if (Boolean.Parse(selected[countselected])==false)
                    {
                        boolselected[i] = false;
                    }
                    else
                    {
                        boolselected[i] = true;
                        countselected += 1;
                    }
                    countselected += 1;
                }

                for (int i=0;i<skillids.Length;i++)
                {
                    if (boolselected[i]==true)
                    {
                        lst.Add(Int32.Parse(skillids[i]));
                    }
                }
            
                using (var client = new HttpClient())
                {
                    //HTTP POST
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["APIURL"]);

                    var postTask = client.PostAsJsonAsync<IList<Int32>>("employees/postemployeeselectedskills/?empid=" + empid, lst);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Skills/" + empid, "Employee", new { empsurname = skills.Get(3), empfirstname = skills.Get(4) });
                    }
                    else //web api sent error response 
                    {
                        HttpStatusCode statuscode = result.StatusCode;

                        if (statuscode == HttpStatusCode.NotFound)
                        {
                            return View("NotFound");
                        }
                        else
                        {
                            return View("Error");
                        }
                    }

                }
            }
            catch
            {
                return View("Error");
            }
        }
    }
}
