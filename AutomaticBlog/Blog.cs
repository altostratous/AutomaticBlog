using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticBlog
{
    public class Blog
    {
        public Blog()
        {
            Url = "New blog.";
            Username = "";
            Password = "";
            LoginScript = "";
            PostScript = "";
        }
        public string Url { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string LoginScript { get; set; }
        public string PostScript { get; set; }
    }
}
