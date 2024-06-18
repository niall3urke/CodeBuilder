using CodeBuilder.Data;
using CodeBuilder.Models;
using CodeBuilder.Operations.Commands;
using Microsoft.EntityFrameworkCore;

namespace CodeBuilder.Pages.Checks.Helpers
{
    public class BlockDatabaseService
    {

        // Fields

        private readonly IDbContextFactory<ApplicationDbContext> _factory;

        private readonly bool _update = true;

        // Constructors

        public BlockDatabaseService(IDbContextFactory<ApplicationDbContext> factory)
        {
            _factory = factory;
        }

        public BlockDatabaseService() 
        {
            _update = false;
        }

        // Methods

        public async Task<bool> Create(ConditionModel condition)
        {
            if (!_update)
                return false;

            return await new CreateCommand<ConditionModel>(_factory, condition).Execute();
        }

        public async Task<bool> Update(List<BlockModel> blocks)
        {
            if (!_update)
                return false;

            bool success = true;

            foreach (var block in blocks)
            {
                if (!await new UpdateCommand<BlockModel>(_factory, block).Execute())
                {
                    success = false;
                }
            }

            return success;
        }

        public async Task<bool> Update (BlockModel block)
        {
            if (!_update)
                return false;

            return await new UpdateCommand<BlockModel>(_factory, block).Execute();
        }

        public async Task<bool> Update(ConditionModel block)
        {
            if (!_update)
                return false;

            return await new UpdateCommand<ConditionModel>(_factory, block).Execute();
        }

        public async Task<bool> Delete(BlockModel block)
        {
            if (!_update)
                return false;

            return await new DeleteCommand<BlockModel>(_factory, block.Id).Execute();
        }

        public async Task<bool> Delete(ConditionModel condition)
        {
            if (!_update)
                return false;

            return await new DeleteCommand<ConditionModel>(_factory, condition.Id).Execute();
        }


    }
}
