using BibliotecaRosangela.Domain.Features.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaRosangela.Common.Tests.Features.Books
{
    public static partial class ObjectMother
    {
        public static Book GetBook()
        {
            return new Book()
            {
                Title = "Novo livro",
                Theme = "Tema",
                Author = "Autor",
                Volume = 1,
                PublicationDate = DateTime.Now.AddYears(-3),
                Disponibility = true
            };
        }

        public static Book GetBookInvalidTitle()
        {
            return new Book()
            {
                Title = "",
                Theme = "Tema",
                Author = "Autor",
                Volume = 1,
                PublicationDate = DateTime.Now.AddYears(-3),
                Disponibility = true
            };
        }
    }
}
