using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Db.Context.Models
{
    static class AtsTestModelCreator
    {
        public static void Setup(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blogs>(entity =>
            {
                entity.HasOne(d => d.OwnerUser)
                    .WithMany(p => p.Blogs)
                    .HasForeignKey(d => d.OwnerUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Blogs__OwnerUser__173876EA");
            });

            modelBuilder.Entity<Comments>(entity =>
            {
                entity.Property(e => e.Contents).IsUnicode(false);

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Comments__PostId__267ABA7A");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Comments__UserId__25869641");
            });

            modelBuilder.Entity<PostTags>(entity =>
            {
                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostTags)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PostTags__PostId__21B6055D");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.PostTags)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PostTags__TagId__22AA2996");
            });

            modelBuilder.Entity<Posts>(entity =>
            {
                entity.Property(e => e.Contents).IsUnicode(false);

                entity.HasOne(d => d.Blog)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.BlogId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Posts__BlogId__1ED998B2");
            });

            modelBuilder.Entity<Tags>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(512)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Login)
                    .HasMaxLength(256)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserBlogs>(entity =>
            {
                entity.HasIndex(e => new { e.UserId, e.BlogId })
                    .HasName("UQ_UserBlog")
                    .IsUnique();

                entity.HasOne(d => d.Blog)
                    .WithMany(p => p.UserBlogs)
                    .HasForeignKey(d => d.BlogId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserBlogs__BlogI__1BFD2C07");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserBlogs)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserBlogs__UserI__1B0907CE");
            });
        }
    }
}
