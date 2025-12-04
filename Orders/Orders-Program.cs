using System;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace Orders
{
    class Program
    {
        static void Main()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            var mp = new dataMapper();
            var Cate = mp.getAllCategories();
            var Pro = mp.getAllProducts();
            var Ord1 = mp.getAllOrders();

            // Names of the 5 most expensive products
            var first = Pro
                .OrderByDescending(p => p.unit_price)
                .Take(5)
                .Select(p => p.nome);
            Console.WriteLine(string.Join(Environment.NewLine, first));

            Console.WriteLine(new string('-', 10));

            // Number of products in each category
            var second = Pro
                .GroupBy(p => p.catId)
                .Select(grp => new { Category = Cate.First(c => c.Id == grp.Key).NAME, Count = grp.Count() })
                .ToList();
            foreach (var item in second)
            {
                Console.WriteLine("{0}: {1}", item.Category, item.Count);
            }

            Console.WriteLine(new string('-', 10));

            // The 5 top products (by order quantity)
            var third = Ord1
                .GroupBy(o => o.product_id)
                .Select(grp => new { Product = Pro.First(p => p.id == grp.Key).nome, Quantities = grp.Sum(grpgrp => grpgrp.quant) })
                .OrderByDescending(q => q.Quantities)
                .Take(5);
            foreach (var item in third)
            {
                Console.WriteLine("{0}: {1}", item.Product, item.Quantities);
            }

            Console.WriteLine(new string('-', 10));

            // The most profitable category
            var category = Ord1
                .GroupBy(o => o.product_id)
                .Select(g => new { catId = Pro.First(p => p.id == g.Key).catId, price = Pro.First(p => p.id == g.Key).unit_price, quantity = g.Sum(p => p.quant) })
                .GroupBy(gg => gg.catId)
                .Select(grp => new { category_name = Cate.First(c => c.Id == grp.Key).NAME, total_quantity = grp.Sum(g => g.quantity * g.price) })
                .OrderByDescending(g => g.total_quantity)
                .First();
            Console.WriteLine("{0}: {1}", category.category_name, category.total_quantity);
        }
    }
}
