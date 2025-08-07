namespace InventoryAPI.Models
{
    public class UpdateUserDto
    {
        public required string Password { get; set; }
        public required int Level { get; set; }
    }
}
