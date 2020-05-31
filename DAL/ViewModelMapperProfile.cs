using AutoMapper;
using Db.Context.Models;
using ViewModels;
using System.Linq;

namespace DAL
{
    public class ViewModelMapperProfile : Profile
    {
        public ViewModelMapperProfile()
        {
            CreateMap<User, UserViewModel>(MemberList.Source)
                .ForMember(dest => dest.OwnedBlogsCount, opts => opts.MapFrom(src => src.Blogs.Count))
                ;
            CreateMap<UserViewModel, User>(MemberList.Source);

            CreateMap<Blogs, BlogViewModel>(MemberList.Source) 
                 .ForMember(dest => dest.OwnerUserName, opts => opts.MapFrom(src => src.OwnerUser.Login))
                 .ForMember(dest => dest.PostsCount, opts => opts.MapFrom(src => src.Posts.Count))
                 ; 
            CreateMap<BlogViewModel, Blogs>(MemberList.Source)
                .ForMember(x => x.Id, o => o.Ignore()) 
                ;

            CreateMap<Posts, PostViewModel>(MemberList.Source)
                .ForMember(dest => dest.Tags, 
                    opts => opts.MapFrom(src => 
                        src.PostTags.Select(x=>x.Tag.Name).ToList()))
                ;
            CreateMap<PostViewModel, Posts>(MemberList.Source)
                .ForMember(x => x.Id, o => o.Ignore()) 
                ;

            CreateMap<PostTags, PostTagViewModel>(MemberList.Source)
                .ForMember(dest => dest.TagName, opts => opts.MapFrom(src => src.Tag.Name))
                ;
            CreateMap<PostTagViewModel, PostTags>(MemberList.Source)
                .ForMember(x => x.Id, o => o.Ignore()) 
                ;

            CreateMap<Tags, TagViewModel>(MemberList.Source);
            CreateMap<TagViewModel, Tags>(MemberList.Source);

            CreateMap<Comments, CommentViewModel>(MemberList.Source)
                .ForMember(dest => dest.UserLogin, opts => opts.MapFrom(src => src.User.Login))
                ;
            CreateMap<CommentViewModel, Comments>(MemberList.Source)
                .ForMember(x => x.Id, o => o.Ignore())  
                ;
        }
    }
}
