using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppObject
{
    class Pos
    {
        List<Products> productList = new List<Products> { };
        List<Purchase> userCart = new List<Purchase> { };
        Dictionary<int, int> productQuantity = new Dictionary<int, int>();
        Dictionary<int, int> cartQuantity = new Dictionary<int, int>();

        public Pos()
        {
            productList.Add(new Products() { ProductName = "Pen", Price = 5, productId = 1 });
            productList.Add(new Products() { ProductName = "Shart", Price = 100, productId = 2 });
            productList.Add(new Products() { ProductName = "Cap", Price = 50, productId = 3 });

            productQuantity[1] = 20;
            productQuantity[3] = 20;
            productQuantity[2] = 20;
        }

        public void Start()
        {
            Console.WriteLine("\nSelect your product \n");
            Console.WriteLine("No \t Item \t Price \t Stock");

            var counter = 1;
            foreach (var item in productList)
            {
                Console.WriteLine(counter + " \t " + item.ProductName + " \t " + item.Price + " \t " + productQuantity[item.productId]);
                counter++;
            }
            Console.WriteLine("\n0. \t to checkout");
            UserInput();
        }

        public void UserInput()
        {
            var itemInput = GetInput("\nSelect your option");
            if (itemInput == 0)
            {
                CheckOut();
            }

            var quantityInput = GetInput("\nInput quantity");
            UpdateCart(productList[itemInput - 1], quantityInput);
        }

        public void UpdateCart(Products item, int quantity)
        {
            var cart = new Purchase();
            cart.productInfo = item;

            if (cartQuantity.ContainsKey(item.productId))
            {
                cartQuantity[item.productId] += quantity;
            }
            else
            {
                userCart.Add(cart);
                cartQuantity[item.productId] = quantity;
            }
            
            productQuantity[item.productId] -= quantity;
            Start();
        }

        public void CheckOut()
        {
            Console.WriteLine("\nYou chose the following products: ");
            Console.WriteLine("\nNo \t Item \t Price \t Quantity \t Total");

            int count = 1;
            double total = 0; 
            foreach (var item in userCart)
            {
                Console.WriteLine(count + 1 + ". \t " + item.productInfo.ProductName + " \t " + item.productInfo.Price + " \t " + cartQuantity[item.productInfo.productId] + " \t\t " + item.productInfo.Price * cartQuantity[item.productInfo.productId]);
                total += item.productInfo.Price * cartQuantity[item.productInfo.productId];
                count++;
            }
            Start();
        }

        public int GetInput(string message)
        {
            Console.WriteLine(message);
            return Convert.ToInt32(Console.ReadLine());
        }
    }
}
