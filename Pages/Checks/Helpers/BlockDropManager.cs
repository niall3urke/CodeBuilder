using CodeBuilder.Data;
using CodeBuilder.Models;
using CodeBuilder.Operations.Commands;
using Microsoft.EntityFrameworkCore;

namespace CodeBuilder.Pages.Checks.Helpers
{
    public class BlockDropManager
    {

        // Properties

        public Action Update { get; set; }

        // Fields

        private readonly IDbContextFactory<ApplicationDbContext> _factory;

        private readonly BlockDatabaseService _db;

        private BlockModel _dragged;

        // Constructors

        public BlockDropManager(IDbContextFactory<ApplicationDbContext> factory)
        {
            _db = new BlockDatabaseService(factory);
            _factory = factory;
        }

        public BlockDropManager()
        {
            _db = new BlockDatabaseService();
        }

        // Methods - public

        public void SetDraggedItem(BlockModel block)
        {
            _dragged = block;
        }

        public async Task HandleDropNesting(BlockModel target)
        {
            await NestBlock(target);
            Update.Invoke();
        }

        public async Task HandleDropReorder(BlockModel target)
        {
            await ReorderBlocks(target);
            Update.Invoke();
        }

        public async Task HandleDelete(BlockModel block)
        {
            await DeleteBlock(block);
            Update.Invoke();
        }

        // =======================
        // ===== Methods - blocks
        // =======================

        private async Task NestBlock(BlockModel target)
        {
            if (target == null || _dragged == null || target == _dragged)
                return;

            if (_dragged.Parent == null || _dragged.Parent == target)
                return;

            if (TargetIsChildOfDragged(target, _dragged))
                return;

            await CreateTargetConditionIfNull(target);

            await RemoveParentConditionIfNoChildren();

            _dragged.Parent.Children.Remove(_dragged);

            ReduceIndexes(_dragged.Parent.Children, _dragged.Index);

            await _db.Update(_dragged.Parent.Children);
            //foreach (var child in _dragged.Parent.Children)
            //{
            //    await new UpdateCommand<BlockModel>(_factory, child).Execute();
            //}

            _dragged.Parent = target;

            _dragged.Index = target.Children.Count;

            target.Children.Add(_dragged);

            await _db.Update(_dragged);

            await _db.Update(target);
            //await new UpdateCommand<BlockModel>(_factory, _dragged).Execute();

            //await new UpdateCommand<BlockModel>(_factory, target).Execute();
        }

        private bool TargetIsChildOfDragged(BlockModel target, BlockModel block)
        {
            // Is the target an immediate child of the block?
            if (block.Children.Contains(target))
            {
                return true;
            }

            // Is the target a subsequent child?
            foreach (var child in block.Children)
            {
                if (TargetIsChildOfDragged(target, child))
                {
                    return true;
                }
            }
            return false;
        }

        private async Task ReorderBlocks(BlockModel target)
        {
            if (target == null || _dragged == null || _dragged == target)
                return;

            if (target.Parent == null || _dragged.Parent == null)
                return;

            if (TargetIsChildOfDragged(target, _dragged))
                return;

            await RemoveParentConditionIfNoChildren();

            _dragged.Parent.Children.Remove(_dragged);

            ReduceIndexes(_dragged.Parent.Children, _dragged.Index);

            _dragged.Index = target.Index;

            for (int i = 0; i < target.Parent.Children.Count; i++)
            {
                if (target.Parent.Children[i].Index >= target.Index)
                {
                    target.Parent.Children[i].Index++;
                }
            }

            target.Parent.Children.Insert(_dragged.Index, _dragged);

            _dragged.Parent = target.Parent;

            await _db.Update(target.Parent.Children);

            await _db.Update(target.Parent);
            
            //foreach (var child in target.Parent.Children)
            //{
            //    await new UpdateCommand<BlockModel>(_factory, child).Execute();
            //}

            //await new UpdateCommand<BlockModel>(_factory, target.Parent).Execute();
        }

        private void ReduceIndexes<T>(List<T> items, int limit) where T : IIndexable
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Index >= limit)
                {
                    items[i].Index--;
                }
            }
        }

        private async Task DeleteBlock(BlockModel block)
        {
            if (block == null || block.Parent == null)
                return;

            await RemoveParentConditionIfNoChildren();

            for (int i = block.Children.Count - 1; i > -1; i--)
            {
                await DeleteBlock(block.Children[i]);
            }

            if (block.Condition != null)
            {
                await _db.Delete(block.Condition);
                //await new DeleteCommand<ConditionModel>(_factory, block.Condition.Id).Execute();
            }

            await _db.Delete(block);
            //await new DeleteCommand<BlockModel>(_factory, block.Id).Execute();

            block.Parent.Children.Remove(block);

            await _db.Update(block.Parent);
            // await new UpdateCommand<BlockModel>(_factory, block.Parent).Execute();
        }

        private async Task CreateTargetConditionIfNull(BlockModel target)
        {
            if (target == null)
                return;

            if (target.Condition != null)
                return;

            // No condition exists yet for the block - create one

            var condition = new ConditionModel();

            if (target.Calculation != null)
            {
                condition.LhsId = target.Calculation.Id;
            }

            if (await _db.Create(condition))
            {
                target.ConditionId = condition.Id;
                await _db.Update(target);
            }

            //if (await new CreateCommand<ConditionModel>(_factory, condition).Execute())
            //{
            //    target.ConditionId = condition.Id;

            //    await new UpdateCommand<BlockModel>(_factory, target).Execute();
            //}

            // Update the model in the UI
            if (target.Calculation != null)
            {
                condition.Lhs = target.Calculation;
            }

            // Update the model in the UI
            target.Condition = condition;
        }

        private async Task RemoveParentConditionIfNoChildren()
        {
            if (_dragged == null || _dragged.Parent == null)
                return;
             
            if (_dragged.Parent.Children.Count > 1 && _dragged.Parent.Children.Contains(_dragged)) // i.e. only block is the _dragged block 
                return;

            if (_dragged.Parent.Condition == null)
                return;

            // The parent doesn't have any children after the drag
            // and drop operation - remove the condition

            _dragged.Parent.ConditionId = null;

            _dragged.Parent.Condition = null;

            await _db.Update(_dragged.Parent);

            //await new UpdateCommand<BlockModel>(_factory, _dragged.Parent).Execute();
        }

        // ===========================
        // ===== Methods - conditions
        // ===========================

        public async Task HandleLhsConditionDrop(ConditionModel target)
        {
            if (_dragged == null || _dragged.Calculation == null)
                return;

            target.LhsId = _dragged.Calculation.Id;

            target.Lhs = _dragged.Calculation;

            target.LhsType = ConditionItemType.IsVariable;

            await _db.Update(target);
            //await new UpdateCommand<ConditionModel>(_factory, target).Execute();

            Update.Invoke();
        }

        public async Task HandleRhsConditionDrop(ConditionModel target)
        {
            if (_dragged == null || _dragged.Calculation == null)
                return;

            target.RhsId = _dragged.Calculation.Id;

            target.Rhs = _dragged.Calculation;

            target.RhsType = ConditionItemType.IsVariable;

            await _db.Update(target);
            //await new UpdateCommand<ConditionModel>(_factory, target).Execute();

            Update.Invoke();
        }

        public async Task HandleRemoveLhsCondition(ConditionModel condition)
        {
            if (condition == null)
                return;

            condition.LhsType = ConditionItemType.IsNumber;

            condition.LhsId = null;

            condition.Lhs = null;

            await _db.Update(condition);
            //await new UpdateCommand<ConditionModel>(_factory, condition).Execute();
            
            Update.Invoke();
        }

        public async Task HandleRemoveRhsCondition(ConditionModel condition)
        {
            if (condition == null)
                return;

            condition.RhsType = ConditionItemType.IsNumber;

            condition.RhsId = null;

            condition.Rhs = null;

            await _db.Update(condition);

            //await new UpdateCommand<ConditionModel>(_factory, condition).Execute();

            Update.Invoke();
        }

    }
}
