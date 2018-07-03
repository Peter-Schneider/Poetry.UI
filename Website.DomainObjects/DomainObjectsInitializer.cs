using Poetry.UI.ComponentSupport.InitializerSupport;
using System;
using System.Collections.Generic;
using System.Text;
using Website.CategorySupport;
using Website.ProductSupport;

namespace Website.DomainObjects
{
    [Initializer]
    public class DomainObjectsInitializer : IInitializer
    {
        ICategoryRepository CategoryRepository { get; }
        IProductRepository ProductRepository { get; }

        public DomainObjectsInitializer(ICategoryRepository categoryRepository, IProductRepository productRepository)
        {
            CategoryRepository = categoryRepository;
            ProductRepository = productRepository;
        }

        public void Initialize()
        {
            var scooterCategory = new Category
            {
                Id = "cade3bee-4165-40c5-89ed-6a1622642009",
                Name = "Scooters",
                UrlSegment = "scooters",
            };

            CategoryRepository.Save(scooterCategory);

            var bikesCategory = new Category
            {
                Id = "7cba0b8d-9849-4be2-b17c-5e3f749e8c19",
                Name = "Bikes",
                UrlSegment = "bikes",
            };

            CategoryRepository.Save(bikesCategory);

            ProductRepository.Save(new Product
            {
                Id = "86707008-dfa4-4c04-bf43-e7260236eead",
                ArticleNo = "10000-5",
                Name = "Electric scooter",
                CategoryId = scooterCategory.Id,
                Description = "Integer in magna at sem varius egestas ut eu ante. Morbi laoreet sit amet leo quis ultricies",
                Price = 500.0,
            });
            ProductRepository.Save(new Product
            {
                Id = "cade3bee-4165-40c5-89ed-6a1622642009",
                ArticleNo = "10000-6",
                Name = "Scoo' with GPS tracking",
                CategoryId = scooterCategory.Id,
                Description = "Praesent vitae ipsum dictum, consectetur arcu quis, ullamcorper dui. Duis auctor neque lorem",
                Price = 250.0,
            });
            ProductRepository.Save(new Product
            {
                Id = "c8a92d83-297f-45e7-9b0a-8b5a44487c01",
                ArticleNo = "23000-1",
                Name = "Mountain bike",
                CategoryId = bikesCategory.Id,
                Description = "Morbi mattis, mauris eu fringilla pretium, nisi arcu ornare lacus, vel consectetur libero ipsum sed sem",
                Price = 150.0,
            });
            ProductRepository.Save(new Product
            {
                Id = "9552bad3-fc48-4cfe-9dca-e6a50fefe946",
                ArticleNo = "20000-2",
                Name = "Foldable bike",
                CategoryId = bikesCategory.Id,
                Description = "Proin porta felis libero, vel malesuada nunc imperdiet sed. Donec in nibh velit. Phasellus nunc lacus, suscipit quis nisl facilisis, semper porta augue",
                Price = 179.99,
            });
            ProductRepository.Save(new Product
            {
                Id = "b5c02e71-15cc-4f59-b714-6cf1999f3a02",
                ArticleNo = "22000-2",
                Name = "Unicycle",
                CategoryId = bikesCategory.Id,
                Description = "Duis aliquam neque vitae massa placerat fringilla. Proin quis iaculis orci. In ac volutpat lorem. Sed at purus sit amet neque tincidunt cursus sit amet non metus",
                Price = 39.49,
            });
        }
    }
}
