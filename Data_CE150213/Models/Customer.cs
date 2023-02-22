using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data_CE150213.Models;

public class Customer
{
    [Key]
    [Column(TypeName = "VARCHAR")]
    [StringLength(50)]
    public string Username { get; set; }

    [Required]
    [Column(TypeName = "VARCHAR")]
    [StringLength(500)]
    public string Password { get; set; }

    [Required]
    [Column(TypeName = "NVARCHAR")]
    [StringLength(50)]
    public string Fullname { get; set; }

    [Required]
    [Column(TypeName = "VARCHAR")]
    [StringLength(50)]
    public string Gender { get; set; }

    [Required]
    [Column(TypeName = "DATE")]
    [NotFutureDate(ErrorMessage = "Birthday cannot be in the future.")]
    public DateTime Birthday { get; set; }

    [Required]
    [Column(TypeName = "TEXT")]
    public string Address { get; set; }

}
public class NotFutureDateAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        DateTime birthday = (DateTime)value;

        if (birthday > DateTime.Today)
        {
            return new ValidationResult(ErrorMessage);
        }

        return ValidationResult.Success;
    }
}