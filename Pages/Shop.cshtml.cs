using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Mission9.Infrastructure;
using Mission9.Models;

namespace Mission9.Pages
{
    public class ShopModel : PageModel
    {
        private IBookRepository repo { get; set; }
        public ShopModel (IBookRepository temp)
        {
            repo = temp;
        }
        public Basket basket { get; set; }
        public string ReturnUrl { get; set; }
        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
            basket = HttpContext.Session.GetJson<Basket>("basket") ?? new Basket();
        }
        public IActionResult OnPost(int bookId, string returnUrl)
        {
            // find project associated with the passed in ID
            Books b = repo.Books.FirstOrDefault(x => x.BookId == bookId);

            // if session already exists, use it! otherwise/if null, make a new basket
            basket = HttpContext.Session.GetJson<Basket>("basket") ?? new Basket();
            // add it 
            basket.AddItem(b, 1);

            // set JSON file based on the new basket
            HttpContext.Session.SetJson("basket", basket);

            // this will redirect us to the cshtml page associated with this model
            return RedirectToPage(new { ReturnUrl = returnUrl });
        }
    }
}
