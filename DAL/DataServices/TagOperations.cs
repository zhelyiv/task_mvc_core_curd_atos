using DAL.Contracts;
using AutoMapper;
using Db.Context.Models;
using System.Collections.Generic;
using System.Linq;
using ViewModels;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class TagOperations : ICrudOperation<TagViewModel>
    {
        private readonly AtsTestContext _context;
        private readonly IMapper _mapper;

        public TagOperations(AtsTestContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IEnumerable<TagViewModel> Get()
        {
            foreach(var Tag in _context.Tags)
            {
                var item = _mapper.Map<TagViewModel>(Tag);
                yield return item;
            }
        }

        public IEnumerable<TagViewModel> Get(int id)
        {
            var Tag = _context.Tags.FirstOrDefault(x => x.Id == id);
            yield return _mapper.Map<TagViewModel>(Tag);
        }

        public TagViewModel GetSingle(int id)
        {
            return Get(id).FirstOrDefault();
        }

        public void Update(TagViewModel item)
        {
            var Tag = _context.Tags.FirstOrDefault(x => x.Id == item.Id);
            if(Tag != null)
            {
                _mapper.Map(item, Tag);
                _context.SaveChanges();
            } 
        }

        public void Delete(int id)
        {
            var Tag = _context.Tags.FirstOrDefault(x => x.Id == id);
            if (Tag != null)
            {
                var postIds = string.Join(",",
                    _context.PostTags.Where(x => x.TagId == Tag.Id).Select(x=>x.PostId).Distinct());

                if (!string.IsNullOrEmpty(postIds))
                    throw new System.Exception($"Tag '{Tag.Name}' is used by posts: {postIds}"); 

                _context.Tags.Remove(Tag);
                _context.SaveChanges();
            } 
        }

        public void Insert(TagViewModel item)
        {
            var Tag = _mapper.Map<Tags>(item);
            _context.Tags.Add(Tag);
            _context.SaveChanges();
        }
    }
}
