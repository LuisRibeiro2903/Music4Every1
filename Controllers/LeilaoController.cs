using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Music4Every1.Controllers
{
    public class LeilaoController : Controller
    {
        // GET: LeilaoController
        public ActionResult Index()
        {
            return View();
        }

        // GET: LeilaoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LeilaoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LeilaoController/Create
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

        // GET: LeilaoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LeilaoController/Edit/5
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

        // GET: LeilaoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LeilaoController/Delete/5
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

        public ActionResult ShowSearchForm()
        { 
            return View(); 
        }

        // PoST: Leilao/ShowSearchResults
        public ActionResult ShowSeachResults(String SearchPhrase)
        {
            return View();
        }
    }
}
