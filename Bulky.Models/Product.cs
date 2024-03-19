using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BulkyBook.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(30, ErrorMessage = "Maximum length of Title should be no greater than 30")]  
        public string Title { get; set; }
        
        
        public string Description { get; set; }

        [Required]
        public string ISBN { get; set; }
        
        [Required]
        [DisplayName("Author")]
        [MaxLength(30, ErrorMessage = "Maximum length of Author should be no greater than 30")]
        public string Author { get; set; }    
        
        [Required]    
        [Display(Name ="List Price")]
        [Range(1, 1000)]
        public double ListPrice { get; set; }
        
       [Required]
        [Display(Name = "Price for 1 to 50")]
        [Range(1, 1000)]
        public double Price { get; set; }
       
        [Required]
        [Display(Name = "Price for 50+")]
        [Range(1, 1000)]
        public double Price50 { get; set; }
        
        [Required]
        [Display(Name ="Price for 100+")]
        [Range(1,1000)]
        public double Price100 { get; set; }
        public int CategoryID { get; set; }
        [Required]
        [ForeignKey("CategoryID")]

        [ValidateNever]
        public Category? Category { get; set; }

        [ValidateNever]
        public string? ImageURL { get; set; }

    }
}
