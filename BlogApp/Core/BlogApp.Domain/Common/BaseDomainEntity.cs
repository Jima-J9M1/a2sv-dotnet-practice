namespace BlogApp.Domain;
public class BaseDomainEntity
{
    public int PostId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
