using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mission9.Models
{
    public class Basket
    {
        public List<BasketLineItem> Items { get; set; } = new List<BasketLineItem>();
        // to be able to add an item to the basket (know which project we're donating to, and how much the donation is
        public virtual void AddItem(Books book, int qty)
        {
            // go find the project associated with that ID passed in
            BasketLineItem line = Items.Where(p => p.Book.BookId == book.BookId)
                .FirstOrDefault();

            // if there's not one already associated with it,
            if (line == null)
            {
                // add a new item with the information we were passed 
                Items.Add(new BasketLineItem
                {
                    Book = book,
                    Quantity = qty
                });
            }
            else
            {
                // add the new donation amount to whatever was already in there from the last donation
                line.Quantity += qty;
            }
        }
        public virtual void RemoveItem(Books book)
        {
            Items.RemoveAll(x => x.Book.BookId == book.BookId);
        }
        public virtual void ClearBasket ()
        {
            Items.Clear();
        }
        public double CalculateTotal()
        {
            // for this project, we're just saying that every donation is 25 dollars
            double sum = Items.Sum(x => x.Quantity * x.Book.Price);
            return sum;
        }

    }

    public class BasketLineItem
    {
        [Key]
        public int LineID { get; set; }
        public Books Book { get; set; }
        public int Quantity { get; set; }
    }
}