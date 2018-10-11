using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ContactWeb.Models;
using Microsoft.AspNet.Identity;
using System.Collections;

namespace ContactWeb.Controllers
{
    public class ContactsController : Controller
    {
        private ContactWebContext db = new ContactWebContext();
        private GroupContext dbGroups = new GroupContext();
        

        // GET: Contacts
        [Authorize]
        public ActionResult Index()
        {
            //ViewBag.Groups = dbGroups.Groups.ToList();

            //try
            //{
            //    var userId = new Guid(User.Identity.GetUserId());
            //    var userName = User.Identity.GetUserName();

            //    ViewBag.UserId = userId;
            //    ViewBag.UserName = userName;

            //    ViewData["UserId"] = userId;
            //    ViewData["UserName"] = userName;
            //}
            //catch (System.Exception ex)
            //{

            //}

            

            var userId = GetCurrentUserId();
            //Only get the contact records for the logged in user.
            return View(db.Contacts.Where(x => x.UserId == userId).ToList());
        }

        // GET: Contacts/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null || !EnsureIsUserContact(contact))
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // GET: Contacts/Create
        [Authorize]
        public ActionResult Create()
        {

            ViewBag.UserId = GetCurrentUserId();

            //var model = new GroupModel()
            //{
            //    Groups = dbGroups.GroupModels.Select(c => new SelectListItem
            //    {
            //        Value = c.Id.ToString(),
            //        Text = c.Group
            //    })
            //};
            
            SelectList list = new SelectList(dbGroups.GroupModels, "Group", "Group");
            ViewBag.GroupValues = list;
            
            return View();
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Id,UserId,GroupName,FirstName,LastName,Email,PhonePrimary,PhoneSecondary,BirthDate,StreetAddress1,StreetAddress2,City,State,ZipCode")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                if (contact.GroupName == "-Select-") { contact.GroupName = String.Empty; }
                db.Contacts.Add(contact);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = GetCurrentUserId();
            return View(contact);
        }

        // GET: Contacts/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            //string groupID = "0";
            //foreach (GroupModel record in dbGroups.GroupModels)
            //{
            //    if (record.Group == contact.GroupName)
            //    {
            //        groupID = record.Id.ToString();
            //    }
            //}
            
            if (contact == null || !EnsureIsUserContact(contact))
            {
                return HttpNotFound();
            }

            //var vm = new Contact();
            //vm.GroupName = contact.GroupName; 

            SelectList list = new SelectList(dbGroups.GroupModels, "Group", "Group");
            ViewBag.GroupValues = list;

            //SelectList list = new SelectList(dbGroups.GroupModels.ToList(), "Id", "Group", groupID);
            
            //foreach (SelectListItem item in list)
            //{
            //    if (item.Text == contact.GroupName)
            //    {
            //        item.Selected = true;
            //        break;
            //    };
            //}

            //ViewBag.GroupValues = list;

            //if (String.IsNullOrEmpty(contact.GroupName))
            //{
            //    ViewBag.SelectedItem = "-Select-";
            //}
            //else
            //{
            //    ViewBag.SelectedItem = contact.GroupName;
            //};
            
            ViewBag.UserId = GetCurrentUserId();
            return View(contact);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "Id,UserId,GroupName,FirstName,LastName,Email,PhonePrimary,PhoneSecondary,BirthDate,StreetAddress1,StreetAddress2,City,State,ZipCode")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                //string g = contact.GroupName;
                

                db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            ViewBag.UserId = GetCurrentUserId();
            return View(contact);
        }

        // GET: Contacts/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null || !EnsureIsUserContact(contact))
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Contact contact = db.Contacts.Find(id);
            if (!EnsureIsUserContact(contact))
            {
                return HttpNotFound();
            }

            db.Contacts.Remove(contact);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public Guid GetCurrentUserId()
        {
            return new Guid(User.Identity.GetUserId());
        }

        private bool EnsureIsUserContact(Contact contact)
        {
            return contact.UserId == GetCurrentUserId();
        }

        

        
    }
}
