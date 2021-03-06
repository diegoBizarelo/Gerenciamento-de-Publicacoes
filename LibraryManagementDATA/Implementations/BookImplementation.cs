﻿using LibraryManagement.Models;
using LibraryManagement.Repositories;
using LibraryManagement.ViewModel;
using LibraryManagementDATA.DataContect;
using LibraryManagementDATA.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementDATA.Implementations
{
    public class BookImplementation : BaseRepository<Guid, Book>, IBookRepository
    {
        public BookImplementation(LibraryManagementContext db) : base(db)
        {
            
        }
        public override async Task<Book> SelectAsync(Guid id)
        {
            var entity = await base.SelectAsync(id);
            await _db.Set<Author>().Include(a => a.BooksAuthors).ThenInclude(b => b.Author).ToListAsync();
            //entity.BooksAuthors = authors;
            return entity ?? null;
        }
        public override async Task<IEnumerable<Book>> SelectAsync()
        {
            try
            {
                return await _dbSet.ToListAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public override async Task<Book> UpdateAsnyc(Book item)
        {
            try
            {
                var result = await _dbSet.SingleOrDefaultAsync(e => e.Id.Equals(item.Id));
                await _db.Set<Author>().Include(a => a.BooksAuthors).ThenInclude(b => b.Author).ToListAsync();
                if (result == null)
                {
                    return null;
                }
                
                    
                //result.Include(a => a.BooksAuthors).ThenInclude(b => b.Author)
                item.UpdateAt = DateTime.UtcNow;
                item.CreateAt = result.CreateAt;
                result.BooksAuthors = item.BooksAuthors;
                //_dbSet.Update(item);
                _db.Entry(result).CurrentValues.SetValues(item);
                //await _db.Set<BookAuthor>().AddRangeAsync(item.BooksAuthors);
                await _db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }

            return item;
        }


    }
}
