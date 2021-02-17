using System;
using System.Collections.Generic;
using System.Threading;

namespace ThreadsHomeWork
{

    class Client
    {
        public int Id {set; get;} = 0;
        public string Name {get; set;}
        public int Age {get; set;}
        public decimal Balance {get; set;}
    }    
        
    
    class Program
    {
        public static List<Client> clients = new List<Client>();
        public static List<Client> balances = new List<Client>();
        static void Main(string[] args)
        {
            int id = 0;
            balances.AddRange(clients);
            Client client = new Client();
            int command = 1;
            TimerCallback callback = new TimerCallback(ShowChangeInBalance);
            Timer timer = new Timer(callback, clients, 0, 1000);
            while (command != 0)
            {
               
                Console.WriteLine("Выберите команду:\n1---> Insert,\n2---> Update,\n3---> Select By Id\n4---> Select All\n5---> Delete By Id\n0---> Exit");
                int.TryParse(Console.ReadLine(), out int chooseCommand);
                switch (chooseCommand)
                {
                    case 1:
                    {
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.WriteLine("Insert");
                        Console.ForegroundColor = ConsoleColor.White;
                        id++;
                        Console.Write("Введите Имя: "); string name = Console.ReadLine();
                        Console.Write("Введите возраст: "); int.TryParse(Console.ReadLine(), out int age);
                        Console.Write("Введите баланс: "); decimal.TryParse(Console.ReadLine(), out decimal balance);
                        Thread newInsertThread = new Thread(new ThreadStart(() => {Insert(id, name, age, balance); }));
                        newInsertThread.Start();
                        Thread.Sleep(1000);
                    }
                    break;
                    case 2: 
                    {
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.WriteLine("Update");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("Введите Id "); int Id = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Введите баланс: ");  decimal balance = Convert.ToDecimal(Console.ReadLine());
                        Thread newUpdateThread = new Thread(new ThreadStart(() => {Update(Id, balance); }));
                        newUpdateThread.Start();
                        Thread.Sleep(1000);
                    }
                    break;
                    case 3:
                    {
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.WriteLine("Select By Id: ");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("Введите Id: "); int Id = Convert.ToInt32(Console.ReadLine());
                        Thread newSelectByIdThread = new Thread(new ThreadStart(() => {SelectById(Id);}));
                        newSelectByIdThread.Start();
                        Thread.Sleep(1000);
                    }
                    break;
                    case 4:
                    {
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.WriteLine("Select All: ");
                        Console.ForegroundColor = ConsoleColor.White;
                        Thread newSelectThread = new Thread(new ThreadStart(() => {Select();}));
                        newSelectThread.Start();
                        Thread.Sleep(1000);
                    }
                    break;
                    case 5:
                    {
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.WriteLine("Delete: ");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("Введите Id: "); int Id = Convert.ToInt32(Console.ReadLine());
                        Thread newDeleteThread = new Thread(new ThreadStart(()=>{Delete(Id);}));
                        newDeleteThread.Start();
                        Thread.Sleep(1000);
                    }
                    break;
                    case 0:
                    {
                        Console.ForegroundColor = ConsoleColor.Red;   
                        Console.WriteLine("По вашей команде будет осуществлен выход из программы!");
                        Console.ForegroundColor = ConsoleColor.White;
                        command = 0;
                    }
                    break;
                    default:
                    {
                        Console.ForegroundColor = ConsoleColor.Red;   
                        Console.WriteLine("Вы ввели неправильный символ, повторите команду!");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    break;
                }
            }
        }
        public static void ShowChangeInBalance(object obj)
        {
            for (int i = 0; i < clients.Count; i++)
            {
                if (clients[i].Balance != balances[i].Balance)
                {
                    if (clients[i].Balance <= balances[i].Balance) 
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Баланс до уменьшения: {balances[i].Balance}");
                        Console.WriteLine($"Баланс после уменьшения: {clients[i].Balance}");
                        Console.WriteLine($"Разница: - {clients[i].Balance - balances[i].Balance}");
                    }
                    else 
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Баланс до увеличения: {balances[i].Balance}");
                        Console.WriteLine($"Баланс после увеличения: {clients[i].Balance}");
                        Console.WriteLine($"Разница: + {clients[i].Balance - balances[i].Balance}");
                    }   
                    balances[i].Balance = clients[i].Balance;
                    Console.ForegroundColor = ConsoleColor.White;
                    
                }
            }
        }
        public static void Insert(int id, string name, int age, decimal balance)
        {
            clients.Add(new Client(){Id = id, Name = name, Age = age, Balance = balance});
            balances.Add(new Client(){Id = id, Name = name, Age = age, Balance = balance});
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Успешно добавлен клиент: {id}\t {name}\t {age}\t {balance}");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void Update(int id, decimal balance)
        {
            for (int i = 0; i < clients.Count; i++) if (id == clients[i].Id) clients[i].Balance = balance; 
        }
        public static void SelectById(int id)
        {
            for (int i = 0; i < clients.Count; i++) if (id == clients[i].Id) Console.WriteLine($"{clients[i].Id}\t {clients[i].Name}\t {clients[i].Age}\t {clients[i].Balance}");
        }
        public static void Select()
        {
            for (int i = 0; i < clients.Count; i++) Console.WriteLine($"{clients[i].Id},\t {clients[i].Name},\t {clients[i].Age},\t {clients[i].Balance}");
                
        }
        public static void Delete(int id)
        {
            for(int i =0; i < clients.Count;i++) if(id == clients[i].Id) clients.RemoveAt(i);
            Console.ForegroundColor = ConsoleColor.Red;   
            Console.WriteLine($"Успешно удален клиент {id}");
            Console.ForegroundColor = ConsoleColor.White;
        }
        
    }
}
