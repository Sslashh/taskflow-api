using Microsoft.EntityFrameworkCore;
using TaskFlow.Infrastructure;

namespace TaskFlow.Tests;

public class TaskFlowDbContextTests
{
    [Fact]
    public void DbContext_ExposesExpectedDbSets()
    {
        var options = new DbContextOptionsBuilder<TaskFlowDbContext>()
            .UseInMemoryDatabase("taskflow-dbcontext")
            .Options;

        using var context = new TaskFlowDbContext(options);

        Assert.NotNull(context.Users);
        Assert.NotNull(context.Projects);
        Assert.NotNull(context.TaskItems);
        Assert.NotNull(context.Comments);
    }
}
