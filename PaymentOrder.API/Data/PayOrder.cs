using System.ComponentModel.DataAnnotations;

namespace PaymentOrder.API.Data
{
    public class PayOrder
    {
        [Key]
        public int Id { get; set; }

        public string Identifier { get; set; }

        public string Cpf { get; set; }

        public decimal Value { get; set; }
    }
}
