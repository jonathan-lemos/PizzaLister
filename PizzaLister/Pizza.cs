namespace PizzaLister;

public class Pizza : IEquatable<Pizza>
{
    public ICollection<string> Toppings { get; set; } = new List<string>();

    public bool Equals(Pizza? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;

        return Toppings.Count == other.Toppings.Count &&
               Toppings.OrderBy(x => x)
                   .Zip(other.Toppings.OrderBy(x => x))
                   .All(pair => pair.First == pair.Second);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Pizza)obj);
    }

    public override int GetHashCode()
    {
        return Toppings.OrderBy(x => x)
            .Aggregate(0, (hash, topping) => (hash, topping).GetHashCode());
    }

    public static bool operator ==(Pizza? left, Pizza? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Pizza? left, Pizza? right)
    {
        return !Equals(left, right);
    }
}