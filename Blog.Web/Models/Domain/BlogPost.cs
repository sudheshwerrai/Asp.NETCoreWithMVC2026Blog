using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Blog.Web.Models.Domain
{
    public class BlogPost
    {
        public Guid Id { get; set; }
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }= DateTime.Now;
        public string Author { get; set; }
        public bool Visible { get; set; }

        // Navigation property
        [ValidateNever]
        public ICollection<Tag> Tags { get; set; }

        [ValidateNever]
        public ICollection<BlogPostLike> Likes { get; set; }

        [ValidateNever]
        public ICollection<BlogPostComment> Comments { get; set; }
    }
}
