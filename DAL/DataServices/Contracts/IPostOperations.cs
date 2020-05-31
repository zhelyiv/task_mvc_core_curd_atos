using System;
using System.Collections.Generic;
using System.Text;
using ViewModels;

namespace DAL.DataServices.Contracts
{
    public interface IPostOperations
    {
        IEnumerable<PostViewModel> GetByBlogId(int? blogId);
        void AddTag(PostTagViewModel tag);
        void RemoveTag(int PostId, int TagId);
        IEnumerable<PostTagViewModel> GetTags(int postId);
    }
}

