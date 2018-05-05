using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using LocalGourmet.BLL.Models;
using LocalGourmet.PL.ViewModels;
using NLog;

namespace LocalGourmet.PL.Controllers
{
    public class ReviewsController : Controller
    {
        private Logger log;

        public ReviewsController()
        {
            log = LogManager.GetLogger("file");
        }

        // GET: Reviews
        public ActionResult Index()
        {
            try
            {
                var rrViewModel = new PL.ViewModels.ReviewsIndexVM();
                return View(rrViewModel);
            }
            catch(Exception e)
            {
                log.Error($"[Reviews Controller] [Index] Exception thrown: {e.Message}");
                return RedirectToAction("Index");
            }
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
            catch(Exception e)
            {
                log.Error($"[Reviews Controller] [Details] Exception thrown: {e.Message}");
                return RedirectToAction("Index");
            }
        }

        // GET: Reviews/Create
        public ActionResult Create()
        {
            try
            {
                ReviewsCreateVM vm = new ReviewsCreateVM();
                return View(vm);
            }
            catch (Exception e)
            {
                log.Error($"[Reviews Controller] [Create] Exception thrown: {e.Message}");
                return RedirectToAction("Index");
            }
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
            catch (Exception e)
            {
                log.Error($"[Reviews Controller] [Create] Exception thrown: {e.Message}");
                return View();
            }
        }

        // GET: Reviews/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                ReviewsEditVM vm = new ReviewsEditVM(id);
                if (vm.Review == null) { throw new ArgumentNullException("id"); }
                return View(vm);
            }
            catch (Exception e)
            {
                log.Error($"[Reviews Controller] [Edit] Exception thrown: {e.Message}");
                return RedirectToAction("Index");
            }
        }

        // POST: Reviews/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(Review review)
        {
            try
            {
                //ReviewsEditVM vm = new ReviewsEditVM(id, RestaurantID);
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
            catch (Exception e)
            {
                log.Error($"[Reviews Controller] [Edit] Exception thrown: {e.Message}");
                return RedirectToAction("Index");
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
            catch (Exception e)
            {
                log.Error($"[Reviews Controller] [Delete] Exception thrown: {e.Message}");
                return RedirectToAction("Index");
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
            catch (Exception e)
            {
                log.Error($"[Reviews Controller] [Delete] Exception thrown: {e.Message}");
                return View(review);
            }
        }
    }
}
