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
        private readonly ISetup<IRepositoryManager, bool> categorySetup;

        public CategoryServiceShould()
        {
            repositoryManager = new Mock<IRepositoryManager>();
            var mapper = new Mock<IMapper>();
            categorySetup = repositoryManager.Setup(s =>
                s.CategoryRepository.CheckByNameCategoryExists(It.IsAny<string>(), It.IsAny<bool>()));
            sut = new CategoryService(repositoryManager.Object, mapper.Object);
        }
        
        [Fact]
        public void ThrowCategoryExistException_WhenCategoryExistInDatabase()
        {
            categorySetup.Returns(true); 
            var actual = sut.Invoking(s => s.CreateCategory(new CategoryForCreationDto("Hello", 
                new [] {new ProductForCreationDto(name:"skin", 24, "veryGood", 12)}))); 
            actual.Should().Throw<CategoryExistException>(); 

        }

        [Fact]
        public void ThrowNotCategoryException_WhenCategoryIsntExist()
        {
            var categoryId = Guid.Parse("de1c89ba-34e2-4599-a8f0-3092c03cdb20"); 
            var actual = sut.Invoking(s => s.GetCategoryById(categoryId, false)); 
            actual.Should().Throw<CategoryNotFoundException>(); 
        }
    }
}