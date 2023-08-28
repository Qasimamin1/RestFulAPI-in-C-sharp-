using System.ComponentModel.DataAnnotations;

namespace ContactsAPI.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; } 

        public string Name { get; set; }    

        public int price { get; set; }    
    }
}
