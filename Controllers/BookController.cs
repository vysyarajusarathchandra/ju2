using AutoMapper;
using ju.Dto;
using ju.entity;
using ju.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Reflection.Metadata.BlobBuilder;

namespace ju.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService BookService;
        private readonly IMapper _mapper;
        private readonly IConfiguration configuration;
        public BookController(IuserService userService, IMapper mapper, IConfiguration configuration)
        {
            this.BookService = BookService;
            _mapper = mapper;
            this.configuration = configuration;

        }
        [HttpGet, Route("GetBooks")]
        [Authorize]
        public IActionResult GetAll()
        {
            try
            {
                List<Book> books = BookService.GetBooks();
                List<BookDTO> booksDTO = _mapper.Map<List<BookDTO>>(books);
                return StatusCode(200, booksDTO);
            }
            catch (Exception ex)
            {
              
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet, Route("GetBookById/{Bookid}")]
        [Authorize]


        public IActionResult Get(int bookId)
        {
            try
            {
                Book book = BookService.GetBookById(bookId);
                BookDTO bookDTO = _mapper.Map<BookDTO>(book);
                if (book != null)
                    return StatusCode(200, bookDTO);
                else
                    return StatusCode(404, new JsonResult("Invalid Id"));
            }
            catch (Exception ex)
            {
              
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost, Route("AddBook")]
        [Authorize(Roles = "Admin")]
        public IActionResult Add(BookDTO BookDTO)
        {
            try
            {
                Book book = _mapper.Map<Book>(BookDTO);
                BookService.AddBook(book);
                return StatusCode(200, book);
            }
            catch (Exception ex)
            {
              
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut, Route("EditBook")]
        [Authorize(Roles = "Admin")]

        public IActionResult Edit(BookDTO BookDTO)
        {
            try
            {
                Book book = _mapper.Map<Book>(BookDTO);
                BookService.updatebook(book);
                return StatusCode(200, book);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete, Route("Deletebook/{Bookid}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int Bookid)
        {
            try
            {
                BookService.deletebook(Bookid);
                return StatusCode(200, new JsonResult($"Book with Id {Bookid} is Deleted"));
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, ex.Message);

            }
        }
    }
}
