using Microsoft.AspNetCore.Mvc;
using Mission9.Models;
using Mission9.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission9.Controllers
{
    public class HomeController : Controller
    {
        private IBookRepository repo;
        public HomeController (IBookRepository temp)
        {
            repo = temp;
        }
        public IActionResult Index (string bookType, int pageNum = 1)
        {
            int pageSize = 10;

            var x = new BooksViewModel
            {
                Books = repo.Books
                .Where(x => x.Category == bookType || bookType == null)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),

                PageInfo = new PageInfo
                {
                    TotalNumBooks = (bookType == null ? repo.Books.Count() : repo.Books.Where(x => x.Category == bookType).Count()),
                    BooksPerPage = pageSize,
                    CurrentPage = pageNum
                }
            };
            return View(x);
        }
    }
}
