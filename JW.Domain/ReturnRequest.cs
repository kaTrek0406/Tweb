using System;

namespace JW.Domain
{
    public class ReturnRequest
    {
        public int Id { get; set; }
        public string OrderId { get; set; }
        public string  CustomerId { get; set; }
        public string Reason { get; set; }
        public DateTime ReturnDate { get; set; }
        public virtual Order Order { get; set; }
        public virtual Customer Customer { get; set; }
    }
}