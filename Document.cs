using System;

namespace Lab2.Documents
{
    /// <summary>
    /// Абстрактный базовый класс Document.
    /// Содержит общие реквизиты (номер, дата, отправитель/получатель)
    /// и объявляет абстрактный метод GetTotal().
    /// Также переопределяет Equals / GetHashCode / ToString.
    /// </summary>
    public abstract class Document : IEquatable<Document>
    {
        public string Number { get; set; }
        public DateTime IssueDate { get; set; }
        public Organization From { get; set; }
        public Organization To { get; set; }

        protected Document(string number, DateTime issueDate, Organization from = null, Organization to = null)
        {
            Number = string.IsNullOrWhiteSpace(number) ? "N/A" : number;
            IssueDate = issueDate;
            From = from ?? new Organization("Unknown");
            To = to ?? new Organization("Unknown");
        }

        /// <summary>
        /// Общий контракт: возвращает суммарную стоимость/сумму документа.
        /// Реализуется в наследниках.
        /// </summary>
        public abstract decimal GetTotal();

        /// <summary>
        /// Виртуальная печать — наследники могут расширять.
        /// </summary>
        public virtual void Print()
        {
            Console.WriteLine(ToString());
        }

        /// <summary>
        /// Базовое ToString выводит тип, номер и дату; потомки дополняют.
        /// </summary>
        public override string ToString()
        {
            return $"Номер: {Number}, Дата: {IssueDate:yyyy-MM-dd}, От: {From?.Name ?? "?"}, Кому: {To?.Name ?? "?"}";
        }


        /// <summary>
        /// Равенство по номеру и дате (упрощённо).
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as Document);
        }

        public bool Equals(Document other)
        {
            if (other == null) return false;
            return string.Equals(Number, other.Number, StringComparison.OrdinalIgnoreCase)
                   && IssueDate.Date == other.IssueDate.Date;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 31 + (Number?.ToLowerInvariant().GetHashCode() ?? 0);
                hash = hash * 31 + IssueDate.Date.GetHashCode();
                return hash;
            }
        }
    }
}
