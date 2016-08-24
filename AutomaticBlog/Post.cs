namespace AutomaticBlog
{
    public class Post
    {
        public string Title { get; set; }
        public string Abstract { get; set; }
        public string Content { get; set; }
        public string ReadMore { get; internal set; }
    }
}