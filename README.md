特性	垂直切片架构	清洁架构
核心理念	按功能组织代码	按技术关注点分层
代码组织方式	功能切片（每个功能一个文件/文件夹）	同心圆分层（领域->应用->基础设施等）
依赖方向	无固定方向	向内依赖（外层依赖内层）
主要优势	高内聚、易理解、快速开发	技术无关性、可测试性、长期可维护性
学习曲线	低（直观）	中到高（需要理解分层规则）
适用项目规模	中小型项目	中大型复杂项目
典型技术栈	Minimal API + EF Core	MediatR + FluentValidation + AutoMapper
变更影响范围	局部（限制在切片内）	可能跨层（但受依赖规则约束）
新功能开发速度	快（所有相关代码在一起）	中（需要跨层实现）
技术替换成本	中（功能内聚但可能分散）	低（基础设施层可替换）
详细对比
1. 设计理念
维度	垂直切片架构	清洁架构
核心思想	功能优先 - 围绕业务功能组织代码	关注点分离 - 按技术职责分层
比喻	披萨切片 - 每片包含所有层（奶酪、酱料、配料）	洋葱圈 - 每层包裹内层，核心不受外层影响
主要目标	减少认知负荷，加速功能开发	创建技术无关的核心，提高长期可维护性
架构驱动因素	业务功能需求	技术解耦需求
2. 代码结构
垂直切片结构示例：

text
Features/
├── Products/
│   ├── CreateProduct.cs        // 端点+命令+处理程序
│   ├── GetProductById.cs
│   └── UpdateProduct.cs
├── Orders/
│   ├── PlaceOrder.cs
│   └── CancelOrder.cs
清洁架构结构示例：

text
src/
├── Domain/                     // 核心业务逻辑
│   ├── Entities/
│   └── Repositories/
├── Application/                // 用例实现
│   ├── Products/
│   │   ├── Commands/
│   │   └── Queries/
│   └── Orders/
│       ├── Commands/
│       └── Queries/
├── Infrastructure/             // 技术实现
│   ├── Persistence/
│   └── ExternalServices/
└── Web/                        // 交付机制
    ├── Controllers/
    └── Middleware/
3. 依赖管理
依赖方向	垂直切片架构	清洁架构
核心原则	无强制规则，但切片内自包含	依赖规则：外层依赖内层，内层不依赖外层
领域知识位置	分散在各切片中	集中在领域层
数据库访问	切片可直接访问数据库	通过接口抽象，基础设施层实现
框架依赖	可能直接依赖框架	领域层完全框架无关
4. 优势对比
垂直切片优势:

✅ 功能内聚：所有相关代码在一个文件中

✅ 快速开发：添加新功能无需跨多个文件

✅ 简化理解：开发者只需关注当前功能

✅ 减少过度设计：避免不必要的抽象

✅ 渐进式演进：可轻松重构为更复杂架构

清洁架构优势:

✅ 技术无关性：核心业务不依赖具体技术

✅ 高度可测试：领域逻辑可独立测试

✅ 长期可维护：清晰的架构边界

✅ 团队协作：不同团队可并行处理不同层

✅ 替换技术栈：轻松更换数据库/框架

5. 劣势对比
垂直切片劣势:

⚠️ 可能重复代码：不同切片可能有相似基础设施代码

⚠️ 抽象不足：复杂系统可能缺乏统一抽象

⚠️ 技术耦合：业务逻辑可能直接依赖具体技术

⚠️ 规模限制：在超大型项目中可能变得混乱

清洁架构劣势:

⚠️ 开发速度慢：简单功能需要跨多层实现

⚠️ 认知负荷高：需要理解多层交互

⚠️ 过度抽象风险：可能导致不必要的复杂性

⚠️ 文件跳转多：单个功能涉及多个文件/层

6. 适用场景
场景	推荐架构	原因
初创项目/原型开发	垂直切片	快速迭代，最小化前期设计
领域复杂的中大型系统	清洁架构	需要清晰的领域模型和技术解耦
微服务中的单个服务	两者皆可	垂直切片适合简单服务，清洁架构适合核心服务
需要频繁更换技术的系统	清洁架构	基础设施层可替换
短期活动项目（如营销活动）	垂直切片	快速开发，项目结束后可能废弃
长期企业级应用	清洁架构	投资在可维护性和可扩展性上
7. 技术栈实现对比
典型垂直切片实现:

csharp
// Features/Products/CreateProduct.cs
app.MapPost("/products", async (CreateProductRequest request, AppDbContext db) => 
{
    var product = new Product { Name = request.Name };
    db.Products.Add(product);
    await db.SaveChangesAsync();
    return Results.Created($"/products/{product.Id}", product);
});
典型清洁架构实现:

csharp
// Application/Products/Commands/CreateProductCommand.cs
public record CreateProductCommand(string Name) : IRequest<int>;

// Application/Products/Commands/CreateProductHandler.cs
public class CreateProductHandler : IRequestHandler<CreateProductCommand, int>
{
    public async Task<int> Handle(CreateProductCommand request, IRepository repo)
    {
        var product = new Product(request.Name);
        await repo.AddAsync(product);
        return product.Id;
    }
}

// Infrastructure/Persistence/ProductRepository.cs
public class ProductRepository : IRepository
{
    // EF Core 实现
}

// Web/Controllers/ProductsController.cs
[HttpPost]
public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
{
    var id = await _mediator.Send(command);
    return CreatedAtAction(nameof(Get), new { id });
}
8. 演进路径
图表
代码










混合架构示例（清洁架构+垂直切片）:

text
src/
├── Domain/
├── Application/
│   ├── Features/
│   │   ├── Products/         # 产品功能切片
│   │   │   ├── Commands      # 垂直切片内分层
│   │   │   └── Queries
│   │   └── Orders/           # 订单功能切片
├── Infrastructure/
└── Web/
9. 如何选择？
考虑以下问题帮助决策：

项目预期寿命？

短期 (<2年)：垂直切片

长期 (>3年)：清洁架构

领域复杂度？

简单CRUD：垂直切片

复杂业务规则：清洁架构

团队规模？

小团队(1-3人)：垂直切片

大团队(5+人)：清洁架构

技术不确定性？

技术栈固定：垂直切片

可能更换数据库/框架：清洁架构

测试要求？

基础测试：垂直切片

高测试覆盖率：清洁架构

10. 最佳实践建议
垂直切片优化：

创建共享工具类避免重复代码

为复杂切片添加内部分层

使用MediatR管理切片内交互

定期重构提取通用组件

清洁架构优化：

在应用层使用垂直切片组织功能

避免领域层贫血模型

使用依赖注入解耦

基础设施层实现应轻量

通用建议：

"从简单开始，按需演进 - 不要为明天可能不需要的复杂性买单"

结论
架构	何时选择
垂直切片	需要快速交付、项目规模较小、业务逻辑相对简单、团队偏好直观开发模式
清洁架构	长期复杂系统、领域逻辑复杂、需要技术独立性、高测试覆盖率要求、大型团队协作
混合架构	大多数现实项目的理想选择 - 在清洁架构的分层基础上，按功能垂直组织代码
最终选择应基于：

项目特定需求

团队专业知识和偏好

业务目标和约束

长期维护预期

两种架构各有优势，成熟团队通常会融合两者优点，在清洁架构的分层基础上采用垂直切片的功能组织方式，实现既灵活又可维护的系统设计。

开启新对话
