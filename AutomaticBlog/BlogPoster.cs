using System;
using FrontEndAutomation;

namespace AutomaticBlog
{
    internal class BlogPoster
    {
        private Blog blog;
        private Executor executor;

        public BlogPoster(Blog blog, FrontEndAutomation.Executor executor)
        {
            this.executor = executor;
            this.blog = blog;
        }

        public void Login()
        {
            executor.Statement = Compiler.Compile(blog.LoginScript);
            executor.Scope.Set("username", blog.Username);
            executor.Scope.Set("password", blog.Password);
            executor.Execute();
        }

        public void Post(Post post)
        {
            executor.Statement = Compiler.Compile(blog.PostScript);
            executor.Scope.Set("title", post.Title);
            executor.Scope.Set("abstract", post.Abstract);
            executor.Scope.Set("content", post.Content);
            executor.Execute();
        }
    }
}