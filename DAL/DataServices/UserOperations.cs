using DAL.Contracts;
using AutoMapper;
using Db.Context.Models;
using System.Collections.Generic;
using System.Linq;
using ViewModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace DAL
{
    public class UserOperations : ICrudOperation<UserViewModel>
    {
        private readonly AtsTestContext _context;
        private readonly IMapper _mapper;

        public UserOperations(AtsTestContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IEnumerable<UserViewModel> Get()
        {
            foreach(var user in _context.User.Include(x=>x.Blogs))
            { 
                var item = _mapper.Map<UserViewModel>(user);  
                yield return item;
            }
        }

        public IEnumerable<UserViewModel> Get(int id)
        {
            var user = _context.User.FirstOrDefault(x => x.Id == id);
            yield return _mapper.Map<UserViewModel>(user);
        }

        public UserViewModel GetSingle(int id)
        {
            return Get(id).FirstOrDefault();
        }

        public void Update(UserViewModel item)
        {
            var user = _context.User.FirstOrDefault(x => x.Id == item.Id);
            if(user != null)
            {
                _mapper.Map(item, user);
                _context.SaveChanges();
            } 
        }

        public void Delete(int id)
        {
            try
            {
                var user = _context.User.Include(x=>x.Blogs)
                    .Include(x=>x.Comments)
                    .Include(x=>x.UserBlogs)
                    .FirstOrDefault(x => x.Id == id);

                if (user != null)
                { 
                    var blogIds = user.Blogs.Select(x => x.Id).ToList();
                    var posts = _context.Posts.Include(x=> x.PostTags)
                        .Where(x => blogIds.Contains(x.BlogId))
                        .ToList();
                    var postTags = posts.Select(x => x.PostTags);

                    _context.Comments.RemoveRange(user.Comments);
                    foreach(var items in postTags)
                        _context.PostTags.RemoveRange(items);

                    _context.Posts.RemoveRange(posts); 
                    _context.UserBlogs.RemoveRange(user.UserBlogs);  
                    _context.Blogs.RemoveRange(user.Blogs); 
                    _context.User.Remove(user); 
                    _context.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public void Insert(UserViewModel item)
        {

            var user = _mapper.Map<User>(item);
            _context.User.Add(user);
            _context.SaveChanges();
        }
    }
}
