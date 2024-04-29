using GuestBookRazorPages.Models;
using GuestBookRazorPages.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GuestBookRazorPages.Pages
{
    public class createModel : PageModel
    {
        private readonly IRepository repo;
        public createModel(IRepository r)
        {
            repo = r;
        }
        public IActionResult OnGet()
        {

            if (HttpContext.Session.GetString("LastName") != null
                && HttpContext.Session.GetString("FirstName") != null)
            {

                return Page();
            }
            else
                return RedirectToPage("./login");
        }

        [BindProperty]
        public Messages mess { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                string firstName = HttpContext.Session.GetString("FirstName");
                string lastName = HttpContext.Session.GetString("LastName");


                var user = await repo.InOrOut(firstName, lastName);


                if (user != null)
                {
                    var message = new Messages
                    {
                        Id_User = user.Id,
                        Message = mess.Message,
                        MessageDate = DateTime.Now
                    };

                    await repo.Create(message);
                    await repo.Save();

                    return RedirectToPage("./Index");
                }
                else
                {

                    ModelState.AddModelError(string.Empty, "User not found.");
                    return RedirectToAction("Index");
                }
            }
            return Page();

        }
    }
}
