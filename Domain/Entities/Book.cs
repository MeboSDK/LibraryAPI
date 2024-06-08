using Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Book : Entity
    {
        public Book()
        {
            BookAuthors = new HashSet<BookAuthor>();
            BookUsers = new HashSet<UserBook>();
        }
        public string Title { get; set; }
        public string Descrption { get; set; }
        public double Rate { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int TotalCount { get; set; }
        public int CurrentCount { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public virtual ICollection<BookAuthor> BookAuthors { get; set; }
        public virtual ICollection<UserBook> BookUsers { get; set; }
        public virtual BookImage BookImage { get; set; }
    }
}
