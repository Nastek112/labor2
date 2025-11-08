using System;

namespace Lab2.Documents
{
    /// <summary>
    /// Простая модель организации: название, идентификатор и адрес.
    /// </summary>
    public class Organization
    {
        public string Name { get; set; }
        public string INN { get; set; }    // любой идентификатор
        public string Address { get; set; }

        public Organization(string name, string inn = "", string address = "")
        {
            Name = string.IsNullOrWhiteSpace(name) ? "Unknown" : name;
            INN = inn ?? string.Empty;
            Address = address ?? string.Empty;
        }

        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(INN))
                return $"{Name}";
            return $"{Name} (INN: {INN})";
        }
    }
}
