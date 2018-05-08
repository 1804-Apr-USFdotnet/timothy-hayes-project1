using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using LocalGourmet.BLL.Models;
using LocalGourmet.PL.ViewModels;
using NLog;
using LocalGourmet.BLL.Repositories;
using LocalGourmet.BLL.Services;

namespace LocalGourmet.PL.Controllers
{
    public class ReviewsController : Controller
    {
        private Logger log;
        private RestaurantRepository restaurantRepository;
        private ReviewRepository reviewRepository;

        public ReviewsController()
        {
            log = LogManager.GetLogger("file");
            reviewRepository = new ReviewRepository();
            restaurantRepository = new RestaurantRepository();
        }

        public ReviewsController(RestaurantRepository newRestaurantRepository,
            ReviewRepository newReviewRepository)
        {
            log = LogManager.GetLogger("file");
            restaurantRepository = newRestaurantRepository;
            reviewRepository = newReviewRepository;
        }

        // GET: Reviews
        public ActionResult Index(string sort)
        {
            try
            {
                var rrViewModel = new ReviewsIndexVM(reviewRepository, restaurantRepository);
                if(sort=="byRevName")
                {
                    rrViewModel.Reviews = ReviewService.SortByReviewerNameAsc(rrViewModel.Reviews);
                } else if(sort=="byRating")
                {
                    rrViewModel.Reviews = ReviewService.SortByOverallRatingDesc(rrViewModel.Reviews);
                } else if (sort=="byComment")
                {
                    rrViewModel.Reviews = ReviewService.SortByCommentAsc(rrViewModel.Reviews);
                }

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
                Review r = reviewRepository.GetById(id);
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
        public ActionResult Create(int? ID)
        {
            try
            {
                ReviewsCreateVM vm;
                if (ID == null)
                {
                    vm = new ReviewsCreateVM(restaurantRepository);
                }
                else
                {
                    vm = new ReviewsCreateVM((int) ID, restaurantRepository);
                }
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
        public ActionResult Create(ReviewsCreateVM vm)
        {
            try
            {
                if(ModelState.IsValid) // server-side validation
                {
                    reviewRepository.Add(vm.Review);
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
                ReviewsEditVM vm = new ReviewsEditVM(id, reviewRepository, restaurantRepository);
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
        public ActionResult Edit(Review review)
        {
            try
            {
                //ReviewsEditVM vm = new ReviewsEditVM(id, RestaurantID);
                if(ModelState.IsValid) // server-side validation
                {
                    reviewRepository.Update(review);
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
                Review r = reviewRepository.GetById(id);
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
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Review r = reviewRepository.GetById(id);
                if (r == null) { throw new ArgumentNullException("id"); }
                reviewRepository.Delete(r);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                log.Error($"[Reviews Controller] [Delete] Exception thrown: {e.Message}");
                return RedirectToAction("Index");
            }
        }
    }
}
