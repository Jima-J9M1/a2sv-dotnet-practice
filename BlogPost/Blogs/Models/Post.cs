using System.Collections;
namespace Blog.Models
{
    public class Post: BaseEntity
    {

        public Post()
        {
            Comments = new HashSet<Comment>();
        }

        public string title { get; set; } = "";
        public string content { get; set; } = "";

        public virtual ICollection<Comment> Comments {get; set;}
        
    }
}