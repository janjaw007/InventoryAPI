namespace InventoryAPI.Models
{
    public class UpdateUserDto
    {
        public required string Password { get; set; }
        public int? Level { get; set; }
    }
}
