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
        public ShopModel (IBookRepository temp, Basket b)
        {
            repo = temp;
            basket = b;
        }
        public Basket basket { get; set; }
        public string ReturnUrl { get; set; }
        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
        }
        public IActionResult OnPost(int bookId, string returnUrl)
        {
            // find project associated with the passed in ID
            Books b = repo.Books.FirstOrDefault(x => x.BookId == bookId);

            // add it 
            basket.AddItem(b, 1);

            // this will redirect us to the cshtml page associated with this model
            return RedirectToPage(new { ReturnUrl = returnUrl });
        }
        public IActionResult OnPostRemove (int bookId, string returnUrl)
        {
            basket.RemoveItem(basket.Items.First(x => x.Book.BookId == bookId).Book);
            return RedirectToPage(new { ReturnUrl = returnUrl });
        }
    }
}
