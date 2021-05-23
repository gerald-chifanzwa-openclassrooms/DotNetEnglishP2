﻿using System.Collections.Generic;
using System.Linq;

namespace P2FixAnAppDotNetCode.Models
{
    /// <summary>
    /// The Cart class
    /// </summary>
    public class Cart : ICart
    {
        private readonly List<CartLine> _cartLines = new List<CartLine>();

        /// <summary>
        /// Read-only property for display only
        /// </summary>
        public IEnumerable<CartLine> Lines => GetCartLineList();

        /// <summary>
        /// Return the actual cartline list
        /// </summary>
        /// <returns></returns>
        private List<CartLine> GetCartLineList()
        {
            return _cartLines;
        }

        /// <summary>
        /// Adds a product in the cart or increment its quantity in the cart if already added
        /// </summary>//
        public void AddItem(Product product, int quantity)
        {
            CartLine cartLine = _cartLines.FirstOrDefault(c => c.Product.Id == product.Id);

            if (cartLine != null)
            {
                cartLine.Quantity = cartLine.Quantity + quantity;
                return;
            }

            cartLine = new CartLine
            {
                Product = product,
                Quantity = quantity
            };
            _cartLines.Add(cartLine);
        }

        /// <summary>
        /// Removes a product form the cart
        /// </summary>
        public void RemoveLine(Product product) => _cartLines.RemoveAll(l => l.Product.Id == product.Id);

        /// <summary>
        /// Get total value of a cart
        /// </summary>
        public double GetTotalValue()
        {
            double totalAmount = _cartLines.Sum(cartLine => cartLine.Product.Price * cartLine.Quantity);
            return totalAmount;
        }

        /// <summary>
        /// Get average value of a cart
        /// </summary>
        public double GetAverageValue()
        {
            double totalAmount = GetTotalValue();
            int numberOfItems = _cartLines.Sum(cartLine => cartLine.Quantity);
            return totalAmount / numberOfItems;
        }

        /// <summary>
        /// Looks after a given product in the cart and returns if it finds it
        /// </summary>
        public Product FindProductInCartLines(int productId)
        {
            // Find cart line whose product id matches productId. If found, return the product.
            CartLine cartLine = _cartLines.FirstOrDefault(line => line.Product.Id == productId);
            return cartLine?.Product;
        }

        /// <summary>
        /// Get a specifid cartline by its index
        /// </summary>
        public CartLine GetCartLineByIndex(int index)
        {
            return Lines.ToArray()[index];
        }

        /// <summary>
        /// Clears a the cart of all added products
        /// </summary>
        public void Clear()
        {
            List<CartLine> cartLines = GetCartLineList();
            cartLines.Clear();
        }
    }

    public class CartLine
    {
        public int OrderLineId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
