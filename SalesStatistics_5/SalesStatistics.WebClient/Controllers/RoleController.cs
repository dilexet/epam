using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SalesStatistics.WebClient.Identity;
using SalesStatistics.WebClient.IdentityConfig;
using SalesStatistics.WebClient.Infrastructure;
using SalesStatistics.WebClient.Models.RoleModel;
using Serilog;

namespace SalesStatistics.WebClient.Controllers
{
    public class RoleController : Controller
    {
        private ApplicationUserManager UserManager => HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

        private AppRoleManager RoleManager => HttpContext.GetOwinContext().GetUserManager<AppRoleManager>();

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(RoleManager.Roles.ToList());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create([Required] string name)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result
                    = await RoleManager.CreateAsync(new AppRole(name));

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                AddErrorsFromResult(result);
            }

            return View(name);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(string id)
        {
            AppRole role = await RoleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await RoleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                return View("Error");
            }
            return View("Error");
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(string id)
        {
            AppRole role = await RoleManager.FindByIdAsync(id);
            string[] memberIDs = role.Users.Select(x => x.UserId).ToArray();

            IEnumerable<ApplicationUser> members
                = UserManager.Users.Where(x => memberIDs.Any(y => y == x.Id));

            IEnumerable<ApplicationUser> nonMembers = UserManager.Users.Except(members);

            return View(new RoleEditModel
            {
                Role = role,
                Members = members,
                NonMembers = nonMembers
            });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(RoleModificationModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result;
                foreach (string userId in model.IdsToAdd ?? new string[] { })
                {
                    try
                    {
                        result = await UserManager.AddToRoleAsync(userId, model.RoleName);
                        if (!result.Succeeded)
                        {
                            return View("Error");
                        }
                    }
                    catch (DbEntityValidationException dbEx)
                    {
                        foreach (var validationErrors in dbEx.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                Log.Error("{Property}: {Error}", validationError.PropertyName, validationError.ErrorMessage);
                            }
                        }
                    }
                    
                }

                foreach (string userId in model.IdsToDelete ?? new string[] { })
                {
                    result = await UserManager.RemoveFromRoleAsync(userId,
                        model.RoleName);

                    if (!result.Succeeded)
                    {
                        return View("Error");
                    }
                }

                return RedirectToAction("Index");
            }

            return View("Error");
        }
    }
}