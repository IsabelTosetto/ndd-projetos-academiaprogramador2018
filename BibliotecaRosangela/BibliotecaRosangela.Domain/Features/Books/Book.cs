using BibliotecaRosangela.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaRosangela.Domain.Features.Books
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Theme { get; set; }
        public string Author { get; set; }
        public int Volume { get; set; }
        public DateTime PublicationDate { get; set; }
        public bool Disponibility { get; set; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(Title) || string.IsNullOrEmpty(Theme) || string.IsNullOrEmpty(Author))
                throw new BookFieldNullOrEmptyException();

            if (Title.Length < 4)
                throw new BookTitleLessThan4CharactersException();

            if (Theme.Length < 4)
                throw new BookThemeLessThan4CharactersException();

            if (Author.Length < 4)
                throw new BookAuthorLessThan4CharactersException();

            if (Volume < 1)
                throw new BookInvalidVolumeException();

            if (!PublicationDate.CompareDateSmallerCurrent())
                throw new BookDateOverFlowException();
        }
        
    }
}
