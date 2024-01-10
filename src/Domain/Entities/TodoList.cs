using Sample1.Domain.Common;

namespace Sample1.Domain.Entities;

public class TodoList : BaseAuditableEntity
{
    public string? Title { get; set; }

    public IList<TodoItem> Items { get; private set; } = new List<TodoItem>();
}
