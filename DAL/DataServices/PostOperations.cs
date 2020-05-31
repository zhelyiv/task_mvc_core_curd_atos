using DAL.Contracts;
using AutoMapper;
using Db.Context.Models;
using System.Collections.Generic;
using System.Linq;
using ViewModels;
using DAL.DataServices.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class PostOperations : ICrudOperation<PostViewModel>, IPostOperations
    {
        private readonly AtsTestContext _context;
        private readonly IMapper _mapper;

        public PostOperations(AtsTestContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        IQueryable<Posts> PostsQuery => _context.Posts.
            Include(x => x.PostTags);
         
        public IEnumerable<PostViewModel> GetByBlogId(int? blogId)
        {
            if (!blogId.HasValue)
                Enumerable.Empty<BlogViewModel>();

            foreach (var post in PostsQuery.Where(x => x.BlogId == blogId))
            {
                var item = _mapper.Map<PostViewModel>(post);
                yield return item;
            }
        }

        public IEnumerable<PostTagViewModel> GetTags(int postId)
        {
            foreach (var postTag in _context.PostTags.Include(x=>x.Tag)
                .Where(x=>x.PostId == postId))
            {
                var item = _mapper.Map<PostTagViewModel>(postTag);
                yield return item;
            }
        }

        public IEnumerable<PostViewModel> Get()
        {
            foreach (var Post in PostsQuery)
            {
                var item = _mapper.Map<PostViewModel>(Post);
                item.Tags = _context.PostTags.Include(x => x.Tag).
                    Where(x => x.PostId == item.Id).
                    Select(x => x.Tag.Name).
                    ToList();
                yield return item;
            }
        }
         
        public IEnumerable<PostViewModel> Get(int id)
        {
            var Post = PostsQuery.FirstOrDefault(x => x.Id == id);
            yield return _mapper.Map<PostViewModel>(Post);
        }

        public PostViewModel GetSingle(int id)
        {
            return Get(id).FirstOrDefault();
        }

        public void Update(PostViewModel item)
        {
            var Post = _context.Posts.FirstOrDefault(x => x.Id == item.Id);
            if(Post != null)
            {
                _mapper.Map(item, Post);
                _context.SaveChanges();
            } 
        }

        public void Delete(int id)
        {
            var Post = _context.Posts.FirstOrDefault(x => x.Id == id);
            if (Post != null)
            {
                var postTags = _context.PostTags.Where(x => x.PostId == Post.Id);
                _context.PostTags.RemoveRange(postTags);
                _context.Posts.Remove(Post);
                _context.SaveChanges();
            } 
        }

        public void Insert(PostViewModel item)
        {
            var Post = _mapper.Map<Posts>(item);
            _context.Posts.Add(Post);
            _context.SaveChanges();
        }

        public void AddTag(PostTagViewModel tag)
        { 
            if (!_context.PostTags.Any(x => x.PostId == tag.PostId && x.TagId == tag.TagId))
            {
                _context.PostTags.Add(_mapper.Map<PostTags>(tag));
                _context.SaveChanges();
            }
        }

        public void RemoveTag(int PostId, int TagId)
        {
            var item = _context.PostTags.FirstOrDefault(x => x.PostId == PostId && x.TagId == TagId);
            if (item != null)
            {
                _context.PostTags.Remove(item);
                _context.SaveChanges();
            }
        }
    }
}
