using DAL.Contracts;
using AutoMapper;
using Db.Context.Models; 
using System.Collections.Generic;
using System.Linq; 
using ViewModels;
using DAL.DataServices.Contracts;
using Microsoft.EntityFrameworkCore;
using System;

namespace DAL
{
    public class BlogOperations : ICrudOperation<BlogViewModel>,
        IBlogOperations
    {
        private readonly AtsTestContext _context;
        private readonly IMapper _mapper;
        IQueryable<Blogs> BlogsQuery => _context.Blogs.
            Include(x=>x.Posts).
            Include(x => x.OwnerUser);

        public BlogOperations(AtsTestContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
         
        public IEnumerable<BlogViewModel> GetByUserId(int? userId) 
        {
            if (!userId.HasValue)
                Enumerable.Empty<BlogViewModel>();

            foreach (var Blog in BlogsQuery.Where(x=>x.OwnerUserId == userId))
            {
                var item = _mapper.Map<BlogViewModel>(Blog);
                yield return item;
            }
        }

        public IEnumerable<BlogViewModel> Get()
        {
            foreach (var Blog in BlogsQuery)
            {
                var item = _mapper.Map<BlogViewModel>(Blog);
                yield return item;
            }
        }

        public IEnumerable<BlogViewModel> Get(int id)
        {
            var Blog = _context.Blogs.FirstOrDefault(x => x.Id == id);
            yield return _mapper.Map<BlogViewModel>(Blog);
        }

        public BlogViewModel GetSingle(int id)
        {
            return Get(id).FirstOrDefault();
        }

        public void Update(BlogViewModel item)
        {
            var Blog = BlogsQuery.FirstOrDefault(x => x.Id == item.Id);
            if(Blog != null)
            {
                _mapper.Map(item, Blog);
                _context.SaveChanges();
            } 
        }

        public void Delete(int id)
        {
            try
            {
                var Blog = _context.Blogs.FirstOrDefault(x => x.Id == id);
                if (Blog != null)
                {
                    var userBlogs = _context.UserBlogs.Where(x => Blog.Id == x.Id).ToList();
                    var posts = _context.Posts.Where(x => Blog.Id == x.BlogId).ToList();
                    var postIds = posts.Select(x => x.Id);
                    var comments = _context.Comments.Where(x => postIds.Contains(x.PostId)).ToList();

                    _context.Comments.RemoveRange(comments);
                    _context.Posts.RemoveRange(posts);
                    _context.UserBlogs.RemoveRange(userBlogs);

                    _context.Blogs.Remove(Blog);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Insert(BlogViewModel item)
        {
            var Blog = _mapper.Map<Blogs>(item);
            _context.Blogs.Add(Blog);
            _context.SaveChanges();
        }
    }
}
