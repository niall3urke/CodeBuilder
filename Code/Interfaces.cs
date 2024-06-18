using CodeBuilder.Models;

public interface IEntity
{
    Guid Id { get; set; }
}

public interface IMeta : IEntity 
{
    DateTime Created { get; set; }
    DateTime Updated { get; set; }
}

public interface IModelBase : IMeta
{
    StateType State { get; set; }
    string? Name { get; set; }
    string? Desc { get; set; }

    string GetName(int limit = 100);
    string GetDesc(int limit = 100);
}

public interface IStandardized : IModelBase
{
    StandardModel? Standard { get; set; }
    string? Reference { get; set; }

    string GetReference(int limit = 100);
    string GetStandard(int limit = 100);
}

public interface ICodable : IStandardized
{
    string? Code { get; set; }
}

public interface IMeasurable : ICodable
{
    StorageType Storage { get; set; }
    UnitType Unit { get; set; }
}

public interface IVariable : IMeasurable
{
    string? Value { get; set; }
    string? Math { get; set; }
}

public interface IBucket<T>
{
    List<T> Children { get; set; }
}

public interface IBaseProperties : IEntity
{
    StateType State { get; set; }
    DateTime Created { get; set; }
    DateTime Updated { get; set; }
    string? Name { get; set; }
    string? Desc { get; set; }
    string GetStandardType();
    string GetName(int limit = 100);
}

public interface IViewManager
{
    void SetSelectedItem(BlockModel block);

    Task ReorderDrop(BlockModel target);

    Task NestingDrop(BlockModel target);

    Task DeleteBlock(BlockModel block);

    Task ConditionLhsDrop(ConditionModel target);

    Task ConditionRhsDrop(ConditionModel target);

    Task RemoveConditionLhs(ConditionModel condition);

    Task RemoveConditionRhs(ConditionModel condition);
}

public interface IIndexable
{
    int Index { get; set; }
}

public interface ISummarizable
{
    bool IsSummarizable { get; set; }
}