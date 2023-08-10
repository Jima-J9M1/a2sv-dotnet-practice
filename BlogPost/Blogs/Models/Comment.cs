namespace Blog.Models
{
    public class Comment: BaseEntity
    {

        public int postId { get; set; }
        public string text { get; set; } = "";

        public virtual Post Post { get; set;} = null;
    }
}