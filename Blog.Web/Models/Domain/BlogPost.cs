using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Blog.Web.Models.Domain
{
    public class BlogPost
    {
        public Guid Id { get; set; }
        public string Heading { get; set; }

        [Display(Name ="Page Title")]
        public string PageTitle { get; set; }
        public string Content { get; set; }

        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }

        [Display(Name = "Featured Image Url")]
        public string FeaturedImageUrl { get; set; }

        [Display(Name = "Url Handle")]
        public string UrlHandle { get; set; }

        [Display(Name = "Published Date")]
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
