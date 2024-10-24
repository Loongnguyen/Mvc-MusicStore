using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MusicStore.Models
{
    public class Order
    {
        [ScaffoldColumn(false)]
        public int OrderId { get; set; }
        [ScaffoldColumn(false)]
        public System.DateTime OrderDate { get; set; }
        [ScaffoldColumn(false)]
        public string? Username { get; set; }


        [Required(ErrorMessage = "Vui lòng điền thông tin")]
        [DisplayName("Họ")]
        [StringLength(160)]
        public string? FirstName { get; set; }


        [Required(ErrorMessage = "Vui lòng điền thông tin")]
        [DisplayName("Tên")]
        [StringLength(160)]
        public string? LastName { get; set; }


        [Required(ErrorMessage = "Vui lòng điền thông tin")]
        [StringLength(70)]
        public string? Address { get; set; }


        [Required(ErrorMessage = "Vui lòng điền thông tin")]
        [StringLength(40)]
        public string? City { get; set; }


        [Required(ErrorMessage = "Vui lòng điền thông tin")]
        [StringLength(40)]
        public string? State { get; set; }


        [Required(ErrorMessage = "Vui lòng điền thông tin")]
        [DisplayName("Postal Code")]
        [StringLength(10)]
        public string? PostalCode { get; set; }


        [Required(ErrorMessage = "Vui lòng điền thông tin")]
        [StringLength(40)]
        public string? Country { get; set; }


        [Required(ErrorMessage = "Vui lòng điền thông tin")]
        [StringLength(24)]
        public string? Phone { get; set; }


        [Required(ErrorMessage = "Vui lòng điền thông tin")]
        [DisplayName("Email Address")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
            ErrorMessage = "Email không hợp lệ")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [ScaffoldColumn(false)]

        public decimal Total { get; set; }
        public List<OrderDetail>? OrderDetails { get; set; }
    }
}
