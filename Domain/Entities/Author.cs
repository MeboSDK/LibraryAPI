using Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Author : Entity
{
    public Author()
    {
        AuthorBooks = new HashSet<BookAuthor>();
    }
    public string Name { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public DateTime InsertDate { get; set; }
    public DateTime? ModifyDate { get; set; }
    public DateTime? DeleteDate { get; set; }
    public virtual ICollection<BookAuthor> AuthorBooks { get; set; }
}
