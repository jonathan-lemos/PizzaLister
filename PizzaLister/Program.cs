// See https://aka.ms/new-console-template for more information

using PizzaLister;

const string url = "https://www.brightway.com/CodeTests/pizzas.json";
const int numberToDisplay = 20;

var pizzas = (await Http.Get<Pizza[]>(url))
    .Select(pizza => new Pizza
    {
        Toppings = pizza.Toppings.Select(s => s.ToLower()).ToHashSet()
    })
    .ToList();

var pizzaCounts = pizzas.GroupBy(pizza => pizza)
    .ToDictionary(x => x.Key, x => x.Count());

var topN = pizzaCounts.OrderByDescending(x => x.Value).Take(numberToDisplay);

foreach (var ((pizza, count), i) in topN.Zip(Enumerable.Range(1, numberToDisplay)))
{
    Console.WriteLine($"{i}: {string.Join(", ", pizza.Toppings.OrderBy(x => x))} (ordered {count} times)");
}
