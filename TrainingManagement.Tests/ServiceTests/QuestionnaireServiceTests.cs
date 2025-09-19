using Microsoft.Extensions.Logging;
using Moq;
using SqlSugar;
using TrainingManagement.Model;
using TrainingManagement.Model.Dtos;
using TrainingManagement.Service;
using Xunit;
using System.Linq.Expressions;

namespace TrainingManagement.Tests.ServiceTests
{
    public class QuestionnaireServiceTests
    {
        private readonly Mock<ISqlSugarClient> _mockDb;
        private readonly Mock<ILogger<QuestionnaireService>> _mockLogger;
        private readonly QuestionnaireService _questionnaireService;

        public QuestionnaireServiceTests()
        {
            _mockDb = new Mock<ISqlSugarClient>();
            _mockLogger = new Mock<ILogger<QuestionnaireService>>();
            _questionnaireService = new QuestionnaireService(_mockDb.Object);
        }

        [Fact]
        public async Task CreateQuestionnaireAsync_ValidQuestionnaire_ReturnsQuestionnaireDto()
        {
            // Arrange
            var createDto = new QuestionnaireCreateDto
            {
                Title = "测试问卷",
                Description = "测试描述",
                DepartmentId = 1
            };

            var questionnaire = new Questionnaire
            {
                Id = 1,
                Title = "测试问卷",
                Description = "测试描述",
                DepartmentId = 1,
                CreateTime = DateTime.Now
            };

            var mockInsertable = new Mock<IInsertable<Questionnaire>>();
            mockInsertable.Setup(m => m.ExecuteReturnIdentityAsync()).ReturnsAsync(1);
            _mockDb.Setup(db => db.Insertable(It.IsAny<Questionnaire>())).Returns(mockInsertable.Object);

            // Act
            var result = await _questionnaireService.CreateQuestionnaireAsync(createDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("测试问卷", result.Title);
            Assert.Equal(1, result.DepartmentId);
        }

        [Fact]
        public async Task GetQuestionnaireByIdAsync_ExistingId_ReturnsQuestionnaireDto()
        {
            // Arrange
            var questionnaire = new Questionnaire
            {
                Id = 1,
                Title = "测试问卷",
                Description = "测试描述",
                DepartmentId = 1,
                CreateTime = DateTime.Now
            };

            _mockDb.Setup(db => db.Queryable<Questionnaire>())
                   .Returns(new Mock<ISugarQueryable<Questionnaire>>().Object);
            _mockDb.Setup(db => db.Queryable<Questionnaire>().Where(It.IsAny<Expression<Func<Questionnaire, bool>>>()))
                   .Returns(new Mock<ISugarQueryable<Questionnaire>>().Object);
            _mockDb.Setup(db => db.Queryable<Questionnaire>().FirstAsync(It.IsAny<Expression<Func<Questionnaire, bool>>>()))
                   .ReturnsAsync(questionnaire);

            // Act
            var result = await _questionnaireService.GetQuestionnaireByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("测试问卷", result.Title);
        }

        [Fact]
        public async Task GetAllQuestionnairesAsync_ReturnsQuestionnaireDtos()
        {
            // Arrange
            var questionnaires = new List<Questionnaire>
            {
                new Questionnaire
                {
                    Id = 1,
                    Title = "问卷1",
                    Description = "描述1",
                    DepartmentId = 1,
                    CreateTime = DateTime.Now
                },
                new Questionnaire
                {
                    Id = 2,
                    Title = "问卷2",
                    Description = "描述2",
                    DepartmentId = 2,
                    CreateTime = DateTime.Now
                }
            };

            _mockDb.Setup(db => db.Queryable<Questionnaire>())
                   .Returns(new Mock<ISugarQueryable<Questionnaire>>().Object);
            _mockDb.Setup(db => db.Queryable<Questionnaire>().ToListAsync())
                   .ReturnsAsync(questionnaires);

            // Act
            var result = await _questionnaireService.GetAllQuestionnairesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, q => q.Title == "问卷1");
            Assert.Contains(result, q => q.Title == "问卷2");
        }

        [Fact]
        public async Task UpdateQuestionnaireAsync_ValidQuestionnaire_ReturnsTrue()
        {
            // Arrange
            var updateDto = new QuestionnaireDto
            {
                Id = 1,
                Title = "更新后的问卷",
                Description = "更新后的描述",
                DepartmentId = 1
            };

            var existingQuestionnaire = new Questionnaire
            {
                Id = 1,
                Title = "原问卷",
                Description = "原描述",
                DepartmentId = 1,
                CreateTime = DateTime.Now
            };

            _mockDb.Setup(db => db.Queryable<Questionnaire>().Where(It.IsAny<Expression<Func<Questionnaire, bool>>>()))
                   .Returns(new Mock<ISugarQueryable<Questionnaire>>().Object);
            _mockDb.Setup(db => db.Queryable<Questionnaire>().FirstAsync(It.IsAny<Expression<Func<Questionnaire, bool>>>()))
                   .ReturnsAsync(existingQuestionnaire);
            _mockDb.Setup(db => db.Updateable(It.IsAny<Questionnaire>())).Returns(new Mock<IUpdateable<Questionnaire>>().Object);
            _mockDb.Setup(db => db.Updateable(It.IsAny<Questionnaire>()).ExecuteCommandAsync()).ReturnsAsync(1);

            // Act
            var result = await _questionnaireService.UpdateQuestionnaireAsync(updateDto);

            // Assert
            Assert.True(result);
        }
    }
}