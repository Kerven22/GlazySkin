using FluentAssertions;
using Moq;
using Moq.Language.Flow;
using RepositoryContracts;
using Servicies;
using Servicies.Exceptions;
using Shared;

namespace GlazySkin.Tests
{
    public class CategoryServiceShould
    {
        private readonly CategoryService sut;
        private readonly ISetup<IRepositoryManager, CategoryDto> categorySetup;
        private readonly Mock<IRepositoryManager> repositoryManager;

        public CategoryServiceShould()
        {
            repositoryManager = new Mock<IRepositoryManager>();
            categorySetup = repositoryManager.Setup(s =>
                s.CategoryRepository.GetCategoryById(It.IsAny<Guid>(), It.IsAny<bool>()));
            sut = new CategoryService(repositoryManager.Object);
        }
        
        [Fact]
        public void ThrowCategoryExistException_WhenCategoryEsistInDatabase()
        {
            var categoryId = Guid.Parse("8e13141f-4786-454f-89f5-06f855d58d3b");
            var expected = new CategoryDto(categoryId, "Hello");
            categorySetup.Returns(expected); 
            var actual = sut.Invoking(s => s.CreateCategory(Guid.Parse(""), "Hello")); 
            actual.Should().Throw<CategoryExistException>(); 

        }

        [Fact]
        public void ThrowNotCategoryException_WhenCategoryIsntExist()
        {
            //var actual = sut.Invoking(s => s.GetCategoryByName("Hello", false));
            //actual.Should().BeNull();
        }
    }
}