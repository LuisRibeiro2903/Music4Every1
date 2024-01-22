using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Music4Every1.Server.Controllers
{
    public class SingleAuctionController : Controller
    {
        // GET: SingleAuctionController
        public ActionResult Index()
        {
            return View();
        }

        // GET: SingleAuctionController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SingleAuctionController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SingleAuctionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SingleAuctionController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SingleAuctionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SingleAuctionController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SingleAuctionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
