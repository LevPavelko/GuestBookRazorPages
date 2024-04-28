using GuestBookRazorPages.Models;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GuestBookRazorPages.Repository;
using System.Security.Cryptography;

namespace GuestBookRazorPages.Pages.GuestBook
{
    public class loginModel : PageModel
    {

        private readonly IRepository repo;
        public loginModel(IRepository r)
        {
            repo = r;
        }
        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public LoginModel login { get; set; } = default!;
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {

                var users = await repo.Login(login.Login);
                if (users == null)
                {
                    ModelState.AddModelError("", "Wrong login or password!");
                    return Page();
                }

                string? salt = users.Salt;


                byte[] password = Encoding.Unicode.GetBytes(salt + login.Password);


                byte[] byteHash = SHA256.HashData(password);

                StringBuilder hash = new StringBuilder(byteHash.Length);
                for (int i = 0; i < byteHash.Length; i++)
                    hash.Append(string.Format("{0:X2}", byteHash[i]));

                if (users.Password != hash.ToString())
                {
                    ModelState.AddModelError("", "Wrong login or password!");
                    return Page();
                }
                HttpContext.Session.SetString("FirstName", users.FirstName);
                HttpContext.Session.SetString("LastName", users.LastName);
                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}
