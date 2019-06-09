using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Books;
using BusinessObjects.Books;
using BusinessObjects.DTO;
using System.Web.Http.OData;
using JsonPatch;

namespace BusinessLayer.Books
{
    public class BooksBL : BooksDA
    {
        public new List<BooksDTO> GetBooks()
        {
            var Data = base.GetBooks();
            List<BooksDTO> Books = Data.Select(x=> new BooksDTO(x)).ToList();
            return Books;
        }
        public dynamic GetBook(string Id)
        {
            Guid guidOutput = new Guid();
            bool isValid = Guid.TryParse(Id, out guidOutput);
            if (isValid)
            {
                var Data = base.GetBook(guidOutput);
                if (Data == null)
                {
                    return "Invalid ID";
                }
                else
                {
                    return new BooksDTO(Data);
                }
            }
            else
            {
                return "Please provide proper ID";
            }
            
        }
        public dynamic RemoveBook(string Id)
        {
            Guid guidOutput = new Guid();
            bool isValid = Guid.TryParse(Id, out guidOutput);
            if (isValid)
            {
                return base.RemoveBook(guidOutput);
            }
            else
            {
                return "Please provide proper ID";
            }

        }
        public dynamic AddBook(BooksDTO NewBook)
        {
            try
            {
                var Book = new BooksBO();
                Book.Name = NewBook.Name;
                Book.NumPages = NewBook.NumPages;
                Book.PublicDate = NewBook.PublicDate.ToUniversalTime();
                Book.Authors = new List<BusinessObjects.Authors.AuthorsBO>();
                Book.Authors = NewBook.Authors.Select(x => new BusinessObjects.Authors.AuthorsBO() { AuthorName = x.AuthorName }).ToList();
                var Data = base.AddBook(Book);
                if (Data != null)
                {
                    return Data;
                }
                else
                {
                    return "Invalid Data";
                }
            }
            catch (Exception ex)
            {

                return "Invalid";
            }
            
            
        }
        public new dynamic EditBook(string id, Delta<BooksBO> NewBook)
        {
            var Data = base.EditBook(id,NewBook);
            if (Data != null)
            {
                return Data;
            }
            else
            {
                return "Invalid Data";
            }

        }
    }
}
