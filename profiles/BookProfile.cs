using AutoMapper;
using ju.Dto;
using ju.entity;

namespace ju.profiles
{
    public class BookProfile :Profile
    {
       
            public BookProfile()
            {
                CreateMap<Book, BookDTO>();
                CreateMap<BookDTO, Book>();
            }
        
    }
}
