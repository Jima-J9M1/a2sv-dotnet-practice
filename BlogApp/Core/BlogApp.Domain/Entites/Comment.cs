namespace BlogApp.Domain;
public class Comment : BaseDomainEntity
{
    public int CommentId { get; set; }
    public string Text { get; set; } = "";
}
