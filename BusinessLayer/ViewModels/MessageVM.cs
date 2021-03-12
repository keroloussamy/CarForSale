using DAL;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessLayer.ViewModels
{
    public class MessageVM
    {
        public int Id { get; set; }

        [Required]
        [MinLength(50)]
        public string body { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        [ForeignKey("Dealer")]
        public string DealerId { get; set; }
        public virtual Dealer Dealer { get; set; }
    }
}
