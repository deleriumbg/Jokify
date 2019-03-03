using System.Collections.Generic;
using Jokify.Data.Common;

namespace Jokify.Data.Models
{
    public class Category : BaseModel<int>
    {
        public Category()
        {
            this.Jokes = new HashSet<Joke>();
        }

        public string Name { get; set; }

        public virtual ICollection<Joke> Jokes { get; set; }
    }
}
