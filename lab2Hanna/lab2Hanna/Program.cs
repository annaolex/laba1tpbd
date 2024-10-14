using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop
{
    // Моделі
    public class Client
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }

        public ICollection<Order> Orders { get; set; }
    }

    public class Drink
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class Employee
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }

        public ICollection<Order> Orders { get; set; }
    }

    public class Supplier
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
    }

    public class Order
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }

        public int ClientID { get; set; }
        public Client Client { get; set; }

        public int EmployeeID { get; set; }
        public Employee Employee { get; set; }
    }

    // Контекст бази даних
    public class CoffeeShopContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Drink> Drinks { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=CoffeeShop;Trusted_Connection=True;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Client)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.ClientID);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Employee)
                .WithMany(e => e.Orders)
                .HasForeignKey(o => o.EmployeeID);
        }
    }

    // Головна логіка програми
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new CoffeeShopContext())
            {
                // Додавання даних до бази
                if (!context.Clients.Any())
                {
                    AddInitialData(context);
                }

                // Меню для вибору дій
                bool exit = false;
                while (!exit)
                {
                    Console.WriteLine("1. Вивести клієнтів");
                    Console.WriteLine("2. Вивести напої");
                    Console.WriteLine("3. Вивести співробітників");
                    Console.WriteLine("4. Вивести постачальників");
                    Console.WriteLine("5. Вивести замовлення");
                    Console.WriteLine("6. Вийти");

                    var choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            DisplayClients(context);
                            break;
                        case "2":
                            DisplayDrinks(context);
                            break;
                        case "3":
                            DisplayEmployees(context);
                            break;
                        case "4":
                            DisplaySuppliers(context);
                            break;
                        case "5":
                            DisplayOrders(context);
                            break;
                        case "6":
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
                            break;
                    }
                }
            }
        }

        // Метод для наповнення бази даних початковими даними
        static void AddInitialData(CoffeeShopContext context)
        {
            var clients = new Client[]
            {
                new Client { FirstName = "Іван", LastName = "Петренко", Phone = "123456789" },
                new Client { FirstName = "Марія", LastName = "Іваненко", Phone = "111222333" },
                new Client { FirstName = "Олег", LastName = "Сидоренко", Phone = "222333444" }
            };
            context.Clients.AddRange(clients);

            var drinks = new Drink[]
            {
                new Drink { Name = "Кава", Price = 50.00M },
                new Drink { Name = "Чай", Price = 30.00M },
                new Drink { Name = "Сік", Price = 40.00M }
            };
            context.Drinks.AddRange(drinks);

            var employees = new Employee[]
            {
                new Employee { FirstName = "Олексій", LastName = "Коваленко", Position = "Бариста" },
                new Employee { FirstName = "Светлана", LastName = "Григоренко", Position = "Менеджер" }
            };
            context.Employees.AddRange(employees);

            var suppliers = new Supplier[]
            {
                new Supplier { Name = "Кава Постачальник", Phone = "987654321" },
                new Supplier { Name = "Чай Постачальник", Phone = "654321987" }
            };
            context.Suppliers.AddRange(suppliers);

            var orders = new Order[]
            {
                new Order { Date = DateTime.Now, ClientID = 1, EmployeeID = 1 },
                new Order { Date = DateTime.Now, ClientID = 2, EmployeeID = 1 },
                new Order { Date = DateTime.Now, ClientID = 3, EmployeeID = 2 }
            };
            context.Orders.AddRange(orders);

            context.SaveChanges();
        }

        // Методи для виведення даних з бази
        static void DisplayClients(CoffeeShopContext context)
        {
            var clients = context.Clients.ToList();
            foreach (var client in clients)
            {
                Console.WriteLine($"ID: {client.ID}, Name: {client.FirstName} {client.LastName}, Phone: {client.Phone}");
            }
        }

        static void DisplayDrinks(CoffeeShopContext context)
        {
            var drinks = context.Drinks.ToList();
            foreach (var drink in drinks)
            {
                Console.WriteLine($"ID: {drink.ID}, Name: {drink.Name}, Price: {drink.Price}");
            }
        }

        static void DisplayEmployees(CoffeeShopContext context)
        {
            var employees = context.Employees.ToList();
            foreach (var employee in employees)
            {
                Console.WriteLine($"ID: {employee.ID}, Name: {employee.FirstName} {employee.LastName}, Position: {employee.Position}");
            }
        }

        static void DisplaySuppliers(CoffeeShopContext context)
        {
            var suppliers = context.Suppliers.ToList();
            foreach (var supplier in suppliers)
            {
                Console.WriteLine($"ID: {supplier.ID}, Name: {supplier.Name}, Phone: {supplier.Phone}");
            }
        }

        static void DisplayOrders(CoffeeShopContext context)
        {
            var orders = context.Orders
                .Include(o => o.Client)
                .Include(o => o.Employee)
                .ToList();

            foreach (var order in orders)
            {
                Console.WriteLine($"Order ID: {order.ID}, Date: {order.Date}, Client: {order.Client.FirstName} {order.Client.LastName}, Employee: {order.Employee.FirstName} {order.Employee.LastName}");
            }
        }
    }
}
