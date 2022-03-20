using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebClient
{
    static class Program
    {
        static async Task Main()
        {
            var isRunning = true;
            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("Введите цифру для выбора опции:");
                Console.WriteLine("1 - Выбрать клиента по Id");
                Console.WriteLine("2 - Создать клиента с рандомными данными");
                Console.WriteLine("Другое - Выход");
                var key = Console.ReadKey();
                string id;
                switch (key.KeyChar)
                {
                    case '1':
                        Console.Clear();
                        id = Console.ReadLine() ?? "0";
                        try
                        {
                            PrintCustomer(await GetCustomerAsync(long.Parse(id)), long.Parse(id));
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }

                        break;
                    case '2':
                        Console.Clear();
                        var result = await PostCustomerAsync(CreateRandomCustomer());
                        if(result is not null)
                        {
                            PrintCustomer(await GetCustomerAsync(result.id), result.id);
                        }
                        else
                        {
                            Console.WriteLine($"Произошла ошибка при создании клиента");
                        }
                        Console.ReadKey();
                        break;
                    default:
                        isRunning = false;
                        break;
                }
            }
        }

        private static async Task<Client> GetCustomerAsync(long id)
        {
            var response = await new HttpClient().GetAsync($"https://localhost:5001/clients/{id}");

            return JsonConvert.DeserializeObject<Client>(await response.Content.ReadAsStringAsync());
        }

        private static async Task<Client> PostCustomerAsync(CustomerCreateRequest client)
        {
            var response = await new HttpClient().PostAsync("https://localhost:5001/clients",
                new StringContent(JsonConvert.SerializeObject(new Client
                { 
                first_name = client.first_name,
                middle_name = client.middle_name,
                last_name = client.last_name,
                email = client.email}),
                    Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<Client>(await response.Content.ReadAsStringAsync());
        }

        private static void PrintCustomer(Client client, long id)
        {
            if (client == null)
            {
                Console.WriteLine($"Клиент с id {id} не найден");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"#{client.id} - {client.first_name} {client.middle_name} {client.last_name} {client.email}");
            Console.ReadKey();
        }

        private static CustomerCreateRequest CreateRandomCustomer()
        {
            var random = new Random();
            return new CustomerCreateRequest
            {
                first_name = $"RandomFirstName{random.Next(int.MaxValue)}",
                middle_name = $"RandomFirstName{random.Next(int.MaxValue)}",
                last_name = $"RandomLastName{random.Next(int.MaxValue)}",
                email = $"RandomMail{random.Next(int.MaxValue)}" + "@gmail.com",
            };
        }
    }
}