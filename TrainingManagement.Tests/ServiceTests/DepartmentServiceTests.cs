using Microsoft.Extensions.Logging;
using Moq;
using SqlSugar;
using System.Linq.Expressions;
using TrainingManagement.Model;
using TrainingManagement.Model.Dtos;
using TrainingManagement.Service.Implementations;
using Xunit;

namespace TrainingManagement.Tests.ServiceTests
{
    public class DepartmentServiceTests
    {
        private readonly Mock<ISqlSugarClient> _mockDb;
        private readonly Mock<ILogger<DepartmentService>> _mockLogger;
        private readonly DepartmentService _departmentService;

        public DepartmentServiceTests()
        {
            _mockDb = new Mock<ISqlSugarClient>();
            _mockLogger = new Mock<ILogger<DepartmentService>>();
            _departmentService = new DepartmentService(_mockDb.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task CreateAsync_ValidDepartment_ReturnsDepartmentDto()
        {
            // Arrange
            var createDto = new DepartmentCreateDto
            {
                Name = "测试部门",
                Code = "TEST",
                ParentId = null
            };

            var department = new Department
            {
                Id = 1,
                Name = "测试部门",
                Code = "TEST",
                ParentId = null,
                CreateTime = DateTime.Now
            };

            var mockInsertable = new Mock<IInsertable<Department>>();
            mockInsertable.Setup(m => m.ExecuteReturnIdentityAsync()).ReturnsAsync(1);
            _mockDb.Setup(db => db.Insertable(It.IsAny<Department>())).Returns(mockInsertable.Object);

            // Act
            var result = await _departmentService.CreateAsync(createDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("测试部门", result.Name);
            Assert.Null(result.ParentId);
        }

        [Fact]
        public async Task GetByIdAsync_ExistingId_ReturnsDepartmentDto()
        {
            // Arrange
            var department = new Department
            {
                Id = 1,
                Name = "测试部门",
                Code = "TEST",
                ParentId = null,
                CreateTime = DateTime.Now
            };

            _mockDb.Setup(db => db.Queryable<Department>())
                   .Returns(new Mock<ISugarQueryable<Department>>().Object);
            _mockDb.Setup(db => db.Queryable<Department>().Where(It.IsAny<Expression<Func<Department, bool>>>()))
                   .Returns(new Mock<ISugarQueryable<Department>>().Object);
            _mockDb.Setup(db => db.Queryable<Department>().FirstAsync(It.IsAny<Expression<Func<Department, bool>>>()))
                   .ReturnsAsync(department);

            // Act
            var result = await _departmentService.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("测试部门", result.Name);
        }

        [Fact]
        public async Task UpdateAsync_ValidDepartment_ReturnsTrue()
        {
            // Arrange
            var updateDto = new DepartmentDto
            {
                Id = 1,
                Name = "更新后的部门",
                Code = "UPDATED",
                ParentId = null
            };

            var existingDepartment = new Department
            {
                Id = 1,
                Name = "原部门",
                Code = "ORIGINAL",
                ParentId = null,
                CreateTime = DateTime.Now
            };

            _mockDb.Setup(db => db.Queryable<Department>().Where(It.IsAny<Expression<Func<Department, bool>>>()))
                   .Returns(new Mock<ISugarQueryable<Department>>().Object);
            _mockDb.Setup(db => db.Queryable<Department>().FirstAsync(It.IsAny<Expression<Func<Department, bool>>>()))
                   .ReturnsAsync(existingDepartment);
            _mockDb.Setup(db => db.Updateable(It.IsAny<Department>())).Returns(new Mock<IUpdateable<Department>>().Object);
            _mockDb.Setup(db => db.Updateable(It.IsAny<Department>()).ExecuteCommandAsync()).ReturnsAsync(1);

            // Act
            var result = await _departmentService.UpdateAsync(updateDto);

            // Assert
            Assert.True(result);
        }
    }
}