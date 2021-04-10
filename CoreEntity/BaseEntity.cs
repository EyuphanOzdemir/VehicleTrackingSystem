using System;
using System.Collections.Generic;
using System.Text;

namespace CoreEntity
{
    /// <summary>
    ///  All entities have Id, dont they?
    ///  Also most entities have a descriptor mostly with the name of "name".
    ///  BaseNamedEntity is very useful for for instance name duplication checks.
    /// </summary>
    public class BaseEntity
    {
        public int Id { get; set; }
    }

    public class BaseNamedEntity : BaseEntity
    {
        public string Name { get; set; }
    }
}