using CodeBuilder.Data;
using Microsoft.EntityFrameworkCore;

namespace CodeBuilder.Operations
{
    public abstract class ExecutableBase
    {

        protected IDbContextFactory<ApplicationDbContext> _factory;

        public ExecutableBase(IDbContextFactory<ApplicationDbContext> factory)
        {
            _factory = factory;
        }   


    }
}
