using System.ComponentModel.DataAnnotations;

namespace DattingApp.API.DTOs
{
    public class UserForRegisterDto
    {
        [Required]
       public string Username {get; set;} 

       [Required]
       [StringLength(8,MinimumLength=4,ErrorMessage="you must specify password between 4 to 8 characters")]
       public string Password {get; set;} 
    }
}