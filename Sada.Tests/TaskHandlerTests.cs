using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Sada.Api.Data;
using Sada.Api.HandlersApi;
using Sada.Core.Enums;
using Sada.Core.Requests.Task;
using Sada.Core.Requests.TaskRequest;


namespace Sada.Tests
{
    [TestClass]
    public class TaskHandlerTests
    {
        private AppDbContext? _context;
        private Mock<ILogger<TaskHandler>>? _logger;
        private TaskHandler? _handler;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDB")
                .Options;

            _context = new AppDbContext(options);
            _logger = new Mock<ILogger<TaskHandler>>();
            _handler = new TaskHandler(_context, _logger.Object);

        }

        [TestMethod]
        public async Task Task_Lifecycle_ShouldWork_AsExpected()
        {
            // --- CREATE ---
            var createRequest = new CreateTaskRequest
            {
                Title = "Teste Título",
                Description = "Descrição para teste",
                ExpirationDate = DateTime.UtcNow.AddDays(1),
                Status = EStatus.Pendente
            };

            var createResponse = await _handler.CreateAsync(createRequest);

            Assert.AreEqual(201, createResponse.StatusCode);
            Assert.IsNotNull(createResponse.Data);
            var createdTaskId = createResponse.Data!.Id;

            // --- UPDATE ---
            var updateRequest = new UpdateTaskRequest
            {
                Id = createdTaskId,
                Title = "Tarefa Atualizada",
                Description = "Descrição atualizada",
                ExpirationDate = DateTime.UtcNow.AddDays(2),
                Status = EStatus.Concluida
            };

            var updateResponse = await _handler.UpdateAsync(updateRequest);

            Assert.AreEqual(200, updateResponse.StatusCode);
            Assert.IsNotNull(updateResponse.Data);
            Assert.AreEqual("Tarefa Atualizada", updateResponse.Data!.Title);
            Assert.AreEqual(EStatus.Concluida, updateResponse.Data.Status);

            // --- GET BY ID ---
            var getByIdRequest = new GetTaskByIdRequest { Id = createdTaskId };
            var getByIdResponse = await _handler.GetByIdAsync(getByIdRequest);

            Assert.AreEqual(200, getByIdResponse.StatusCode);
            Assert.IsNotNull(getByIdResponse.Data);
            Assert.AreEqual(createdTaskId, getByIdResponse.Data!.Id);
            Assert.AreEqual("Tarefa Atualizada", getByIdResponse.Data.Title);

            // --- GET BY PARAMETERS ---
            var getByParamsRequest = new GetTaskByParametersRequest { Status = EStatus.Concluida };
            var getByParamsResponse = await _handler.GetByParametersAsync(getByParamsRequest);

            Assert.AreEqual(200, getByParamsResponse.StatusCode);
            Assert.IsNotNull(getByParamsResponse.Data);
            Assert.IsTrue(getByParamsResponse.Data!.Any(x => x.Id == createdTaskId));

            // --- GET ALL ---
            var getAllResponse = await _handler.GetAllAsync(new GetAllTaskRequest());

            Assert.AreEqual(200, getAllResponse.StatusCode);
            Assert.IsNotNull(getAllResponse.Data);
            Assert.IsTrue(getAllResponse.Data!.Any(x => x.Id == createdTaskId));

            // --- DELETE ---
            var deleteRequest = new DeleteTaskRequest { Id = createdTaskId };
            var deleteResponse = await _handler.DeleteAsync(deleteRequest);

            Assert.AreEqual(200, deleteResponse.StatusCode);
            Assert.IsNull(deleteResponse.Data);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context?.Database.EnsureDeleted();
            _context?.Dispose();
        }
    }
}
