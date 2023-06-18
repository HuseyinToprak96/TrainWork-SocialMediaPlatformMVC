using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CoreLayer.Entities.Page;
using RepositoryLayer.DataContext;
using Web.App.Filters;
using Web.App.Security;

namespace Web.App.Controllers
{
    [Authorize]
    [LoginFilter]
    [LogFilter]
    public class PagesController : BaseController
    {

        // GET: Pages
        public async Task<ActionResult> Index()
        {
            var result = await _pageService.GetAllAsync();
            return View(result.Data);
        }

        // GET: Pages/Details/5
        public async Task<ActionResult> Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var page = await _pageService.GetAsync(id);
            if (page.Data == null)
            {
                return HttpNotFound();
            }
            return View(page.Data);
        }

        // GET: Pages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Create([Bind(Include = "Id,LinkName,Route,Content,Title,IsDeleted,IsActive,CreatedDate,UpdatedDate")] Page page)
        {
            if (ModelState.IsValid)
            {
                page.Content = XSS.CleanInput(page.Content);
               await _pageService.AddAsync(page);
                return RedirectToAction("Index");
            }

            return View(page);
        }

        // GET: Pages/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var page =await _pageService.GetAsync(id);
            if (page.Data == null)
            {
                return HttpNotFound();
            }
            return View(page.Data);
        }

        // POST: Pages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,LinkName,Route,Content,Title,IsDeleted,IsActive,CreatedDate,UpdatedDate")] Page page)
        {
            if (ModelState.IsValid)
            {
                await _pageService.UpdateAsync(page); 
                return RedirectToAction("Index");
            }
            return View(page);
        }

        // GET: Pages/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var page =await _pageService.GetAsync(id);
            if (page.Data == null)
            {
                return HttpNotFound();
            }
            return View(page.Data);
        }

        // POST: Pages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var page =await _pageService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

    }
}
