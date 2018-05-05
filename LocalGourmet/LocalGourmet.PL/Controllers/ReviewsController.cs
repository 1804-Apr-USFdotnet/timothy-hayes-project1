using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using LocalGourmet.BLL.Models;
using LocalGourmet.PL.ViewModels;

namespace LocalGourmet.PL.Controllers
{
    public class ReviewsController : Controller
    {
        // GET: Reviews
        public ActionResult Index()
        {
            var rrViewModel = new PL.ViewModels.ReviewsIndexVM();
            return View(rrViewModel);
        }

        // GET: Reviews/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                BLL.Models.Review r =
                      (BLL.Models.Review.GetReviewByID(id));
                if (r == null) { throw new ArgumentNullException("id"); }
                return View(r);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Reviews/Create
        public ActionResult Create()
        {
            ReviewsCreateVM vm = new ReviewsCreateVM();
            return View(vm);
        }

        // POST: Reviews/Create
        [HttpPost]
        public async Task<ActionResult> Create(ReviewsCreateVM vm)
        {
            try
            {
                if(ModelState.IsValid) // server-side validation
                {
                    await vm.Review.AddReviewAsync();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(ModelState);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Reviews/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                BLL.Models.Review r =
                     BLL.Models.Review.GetReviewByID(id);
                if (r == null) { throw new ArgumentNullException("id"); }
                return View(r);
            }
            catch
            {
                throw;
            }
        }

        // POST: Reviews/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, Review review)
        {
            try
            {
                if(ModelState.IsValid) // server-side validation
                {
                    await review.UpdateReviewAsync(review);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(ModelState);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Reviews/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                BLL.Models.Review r =
                     BLL.Models.Review.GetReviewByID(id);
                if (r == null) { throw new ArgumentNullException("id"); }
                return View(r);
            }
            catch
            {
                throw;
            }
        }

        // POST: Reviews/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, BLL.Models.Review review)
        {
            try
            {
                BLL.Models.Review r =
                                   BLL.Models.Review.GetReviewByID(id);
                if (r == null) { throw new ArgumentNullException("id"); }
                await r.DeleteReviewAsync();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(review);
            }
        }
    }
}
