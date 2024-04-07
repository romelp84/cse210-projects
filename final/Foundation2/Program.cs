using System;
using System.Collections.Generic;

class Address
{
    private string street;
    private string city;
    private string state;
    private string country;

    public Address(string street, string city, string state, string country)
    {
        this.street = street;
        this.city = city;
        this.state = state;
        this.country = country;
    }

    public bool IsInUSA()
    {
        return country == "USA";
    }

    public string GetAddressInfo()
    {
        return $"{street}, {city}, {state}, {country}";
    }
}

class Customer
{
    private string name;
    private Address address;

    public Customer(string name, Address address)
    {
        this.name = name;
        this.address = address;
    }

    public bool IsInUSA()
    {
        return address.IsInUSA();
    }

    public string GetName()
    {
        return name;
    }

    public Address GetAddress()
    {
        return address;
    }
}

class Product
{
    private string name;
    private int productId;
    private double price;
    private int quantity;

    public Product(string name, int productId, double price, int quantity)
    {
        this.name = name;
        this.productId = productId;
        this.price = price;
        this.quantity = quantity;
    }
      public string GetName()
    {
        return name;
    }

    public double CalculateTotalCost()
    {
        return price * quantity;
    }

    public string GetProductInfo()
    {
        return $"{name} (ID: {productId}) - Price: ${price:F2} (Quantity: {quantity})";
    }

    public int GetProductId()
    {
        return productId;
    }

    public double GetPrice()
    {
        return price;
    }
}

class Order
{
    private List<Product> products;
    private Customer customer;
    private const double ShippingCostUSA = 5.0;
    private const double ShippingCostInternational = 35.0;

    public Order(List<Product> products, Customer customer)
    {
        this.products = products;
        this.customer = customer;
    }

    public double CalculateTotalPrice()
    {
        double totalPrice = 0;
        foreach (var product in products)
        {
            totalPrice += product.CalculateTotalCost();
        }

        if (customer.IsInUSA())
        {
            totalPrice += ShippingCostUSA;
        }
        else
        {
            totalPrice += ShippingCostInternational;
        }

        return totalPrice;
    }

    public string GetPackingLabel()
    {
        string packingLabel = "Packing Label:\n";
        foreach (var product in products)
        {
            packingLabel += $"{product.GetProductInfo()}\n";
        }
        packingLabel += $"Shipping Cost: ${CalculateTotalPrice() - GetTotalProductCost():F2}\n";
        return packingLabel;
    }

    public string GetShippingLabel()
    {
        Address address = customer.GetAddress();
        string shippingLabel = "Shipping Label:\n";
        shippingLabel += $"{customer.GetName()}\n";
        shippingLabel += $"{address.GetAddressInfo()}";
        return shippingLabel;
    }

    private double GetTotalProductCost()
    {
        double totalProductCost = 0;
        foreach (var product in products)
        {
            totalProductCost += product.CalculateTotalCost();
        }
        return totalProductCost;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Address address1 = new Address("1865 East St", "Bluffton", "SC", "USA");
        Address address2 = new Address("4843 River St", "Townsville", "NY", "Canada");
        Address address3 = new Address("6 Euclid Circle", "Sanford", "NC", "USA");
        Address address4 = new Address("45 Arrowhead Ave", "Winter Haven", "FL", "USA");
        Address address5 = new Address("215 West Columbia Lane", "Ruralville", "WA", "Canada");

        Customer customer1 = new Customer("Hassan Caldwell", address1);
        Customer customer2 = new Customer("Demi Floyd", address2);
        Customer customer3 = new Customer("Bennett Moyer", address3);
        Customer customer4 = new Customer("Ashley Serrano", address4);
        Customer customer5 = new Customer("Lachlan Montgomery", address5);

        List<Product> products = new List<Product>
        {
            new Product("Laptop", 101, 999.99, 2),
            new Product("Phone", 102, 499.99, 3),
            new Product("Tablet", 103, 299.99, 1),
            new Product("Headphones", 104, 149.99, 2),
            new Product("Smartwatch", 105, 199.99, 1),
            new Product("Camera", 106, 599.99, 1),
            new Product("Printer", 107, 299.99, 1),
            new Product("Monitor", 108, 399.99, 2),
            new Product("Keyboard", 109, 79.99, 1),
            new Product("Mouse", 110, 39.99, 3)
        };

        Random rand = new Random();
        for (int i = 0; i < 2; i++)
        {
            List<Product> orderProducts = new List<Product>();
            for (int j = 0; j < rand.Next(1, 4); j++)
            {
                Product product = products[rand.Next(products.Count)];
                int quantity = rand.Next(2, 5);
                orderProducts.Add(new Product(product.GetName(), product.GetProductId(), product.GetPrice(), quantity));
            }
            Order order = new Order(orderProducts, customer1);
            Console.WriteLine($"\nOrder {i + 1} - Packing Label:");
            Console.WriteLine(order.GetPackingLabel());
            Console.WriteLine($"Order {i + 1} - Shipping Label:");
            Console.WriteLine(order.GetShippingLabel());
            Console.WriteLine($"Order {i + 1} - Total Price: ${order.CalculateTotalPrice():F2}");
        }
    }
}
