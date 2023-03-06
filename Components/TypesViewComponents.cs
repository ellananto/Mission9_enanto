using Microsoft.AspNetCore.Mvc;
using Mission9.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission9.Components
{
    public class TypesViewComponent : ViewComponent
    {
        // load up the dataset
        private IBookRepository repo { get; set; }
        public TypesViewComponent (IBookRepository temp)
        {
            repo = temp;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedType = RouteData?.Values["bookType"];
            var types = repo.Books
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x);

            return View(types);
        }
    }
}
