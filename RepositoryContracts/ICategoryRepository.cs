﻿  using Entity.Models;
  using Shared;

  namespace RepositoryContracts
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAllCategories(bool trackChanges);

        void CreateCategory(CategoryDto category);

        CategoryDto GetCategoryById(Guid id, bool trackChanges);
    }
}

