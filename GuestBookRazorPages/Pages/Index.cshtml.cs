using GuestBookRazorPages.Models;
using GuestBookRazorPages.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GuestBookRazorPages.Pages
{
    public class IndexModel : PageModel
    {
        //private readonly ILogger<IndexModel> _logger;
        IRepository repo;
        public IndexModel( IRepository r)
        {
            
            repo = r;
        }

       
        public List<Messages> Messages { get; private set; }

        public async Task OnGetAsync()
        {
            Messages = await repo.IncludeMessage();
        }
        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Index");
        }
    }
}
