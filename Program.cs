using System;
using Lab2.Documents;

namespace Lab2.Part2App
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Простые названия организаций (коротко)
            var orgA = new Organization("Ромашка");
            var orgB = new Organization("Синицин");

            // Накладная (номер простой)
            var inv = new Invoice("1", DateTime.Today, orgA, orgB);
            inv.Items.Add(new LineItem("Товар A", 2, 199.99m));
            inv.Items.Add(new LineItem("Товар B", 5, 49.50m));

            // Квитанция
            var receipt = new Receipt("2", DateTime.Today.AddDays(-1), 499.95m, orgB, orgA);

            // Чек (sealed)
            var check = new Check("3", DateTime.Today, 1234.50m, "Синицин", orgA, orgB);

            // Массив документов (разнотипные объекты)
            Document[] docs = { inv, receipt, check };

            Console.WriteLine("Документы (тип, номер, итог):\n");

            foreach (var d in docs)
            {
                // Печатаем тип (на русском), номер и итог — минимально и понятно
                string typeRu = GetTypeNameRu(d);
                Console.WriteLine($"{typeRu} №{d.Number} — итог: {d.GetTotal():0.00}");

                // Дополнительно вызываем Print() — демонстрация полиморфизма (каждый тип печатает свои детали)
                d.Print();

                Console.WriteLine(); // пустая строка между документами
            }

            Console.ReadKey();
        }

        // Небольшая вспомогательная функция для русских названий типов
        private static string GetTypeNameRu(Document d)
        {
            if (d is Invoice) return "Накладная";
            if (d is Receipt) return "Квитанция";
            if (d is Check) return "Чек";
            return "Документ";
        }
    }
}
