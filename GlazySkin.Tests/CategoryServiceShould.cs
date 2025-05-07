using AutoMapper;
using Entity.Models;
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
        private readonly Mock<IRepositoryManager> repositoryManager;
        private readonly ISetup<IRepositoryManager, Category> categorySetup;

        public CategoryServiceShould()
        {
            repositoryManager = new Mock<IRepositoryManager>();
            var mapper = new Mock<IMapper>();
            categorySetup = repositoryManager.Setup(s =>
                s.CategoryRepository.GetCategoryById(It.IsAny<Guid>(), It.IsAny<bool>()));
            sut = new CategoryService(repositoryManager.Object, mapper.Object);
        }
        
        [Fact]
        public void ThrowCategoryExistException_WhenCategoryExistInDatabase()
        {
            var categoryId = Guid.Parse("8e13141f-4786-454f-89f5-06f855d58d3b");
            var expected = new Category(){CategoryId = categoryId, Name = "Hello"};
            categorySetup.Returns(expected); 
            var actual = sut.Invoking(s => s.CreateCategory(new CategoryDto { Id = Guid.Parse("8e13141f-4786-454f-89f5-06f855d58d3b"), Name = "Hello"})); 
            actual.Should().Throw<CategoryExistException>(); 

        }

        [Fact]
        public void ThrowNotCategoryException_WhenCategoryIsntExist()
        {
            
        }
    }
}