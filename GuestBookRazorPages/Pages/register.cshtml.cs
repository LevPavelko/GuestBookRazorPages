using System.Text;
using GuestBookRazorPages.Models;
using GuestBookRazorPages.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography;


namespace GuestBookRazorPages.Pages.GuestBook
{
    public class registerModel : PageModel
    {
        private readonly IRepository repo;
        public registerModel(IRepository r)
        {
            repo = r;
        }
        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public RegisterModel register { get; set; } = default!;
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                User user = new User();
                user.FirstName = register.FirstName;
                user.LastName = register.LastName;
                user.Login = register.Login;

                byte[] saltbuf = new byte[16];

                RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
                randomNumberGenerator.GetBytes(saltbuf);

                StringBuilder sb = new StringBuilder(16);
                for (int i = 0; i < 16; i++)
                    sb.Append(string.Format("{0:X2}", saltbuf[i]));
                string salt = sb.ToString();


                byte[] password = Encoding.Unicode.GetBytes(salt + register.Password);


                byte[] byteHash = SHA256.HashData(password);

                StringBuilder hash = new StringBuilder(byteHash.Length);
                for (int i = 0; i < byteHash.Length; i++)
                    hash.Append(string.Format("{0:X2}", byteHash[i]));

                user.Password = hash.ToString();
                user.Salt = salt;
                repo.CreateUser(user);
                repo.Save();
                HttpContext.Session.SetString("FirstName", user.FirstName);
                HttpContext.Session.SetString("LastName", user.LastName);
                return RedirectToPage("./Index");
            }

            return Page();
        }

    }
}
