using DAL.Contracts;
using AutoMapper;
using Db.Context.Models;
using System.Collections.Generic;
using System.Linq;
using ViewModels;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class CommentOperations : ICrudOperation<CommentViewModel>
    {
        private readonly AtsTestContext _context;
        private readonly IMapper _mapper;
        IQueryable<Comments> CommentQuery => _context.Comments.Include(x => x.User);

        public CommentOperations(AtsTestContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
         
        public IEnumerable<CommentViewModel> Get()
        {
            foreach(var Comment in CommentQuery)
            {
                var item = _mapper.Map<CommentViewModel>(Comment);
                yield return item;
            }
        }

        public IEnumerable<CommentViewModel> Get(int id)
        {
            var Comment = CommentQuery.FirstOrDefault(x => x.Id == id);
            yield return _mapper.Map<CommentViewModel>(Comment);
        }

        public CommentViewModel GetSingle(int id)
        {
            return Get(id).FirstOrDefault();
        }

        public void Update(CommentViewModel item)
        {
            var Comment = _context.Comments.FirstOrDefault(x => x.Id == item.Id);
            if(Comment != null)
            {
                _mapper.Map(item, Comment);
                _context.SaveChanges();
            } 
        }

        public void Delete(int id)
        {
            var Comment = _context.Comments.FirstOrDefault(x => x.Id == id);
            if (Comment != null)
            {
                _context.Comments.Remove(Comment);
                _context.SaveChanges();
            } 
        }

        public void Insert(CommentViewModel item)
        {
            var Comment = _mapper.Map<Comments>(item);
            _context.Comments.Add(Comment);
            _context.SaveChanges();
        }
    }
}
