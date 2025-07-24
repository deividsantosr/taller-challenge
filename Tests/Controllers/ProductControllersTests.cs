using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductAPI.Controllers;
using ProductAPI.Models;
using ProductAPI.Repository;
using Xunit;

namespace Tests.Controllers
{
    public class ProductsControllerTests
    {
        private readonly Mock<IProductRepository> _mockRepo;
        private readonly ProductsController _controller;

        public ProductsControllerTests()
        {
            _mockRepo = new Mock<IProductRepository>();
            _controller = new ProductsController(_mockRepo.Object);
        }

        [Fact]
        public void GetAll_ReturnsOkWithProducts()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = Guid.NewGuid(), Name = "Product 1", Price = 10 },
                new Product { Id = Guid.NewGuid(), Name = "Product 2", Price = 20 }
            };
            _mockRepo.Setup(r => r.GetAll()).Returns(products);

            // Act
            var result = _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnProducts = Assert.IsAssignableFrom<IEnumerable<Product>>(okResult.Value);
            Assert.Equal(2, returnProducts.Count());
        }

        [Fact]
        public void Get_ReturnsOk_WhenProductExists()
        {
            // Arrange
            var id = Guid.NewGuid();
            var product = new Product { Id = id, Name = "Product", Price = 15 };
            _mockRepo.Setup(r => r.GetById(id)).Returns(product);

            // Act
            var result = _controller.Get(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnProduct = Assert.IsType<Product>(okResult.Value);
            Assert.Equal(id, returnProduct.Id);
        }

        [Fact]
        public void Get_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mockRepo.Setup(r => r.GetById(id)).Returns((Product?)null);

            // Act
            var result = _controller.Get(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Create_SetsIdIfEmpty_AndReturnsCreatedAtAction()
        {
            // Arrange
            var product = new Product { Id = Guid.Empty, Name = "New Product", Price = 30 };

            _mockRepo.Setup(r => r.Add(It.IsAny<Product>()));
            _mockRepo.Setup(r => r.SaveChanges());

            // Act
            var result = _controller.Create(product);

            // Assert
            var createdAtResult = Assert.IsType<CreatedAtActionResult>(result);
            var createdProduct = Assert.IsType<Product>(createdAtResult.Value);
            Assert.NotEqual(Guid.Empty, createdProduct.Id);

            _mockRepo.Verify(r => r.Add(It.Is<Product>(p => p.Id == createdProduct.Id)), Times.Once);
            _mockRepo.Verify(r => r.SaveChanges(), Times.Once);
        }

        [Fact]
        public void Update_ReturnsNoContent_WhenProductExists()
        {
            // Arrange
            var id = Guid.NewGuid();
            var existingProduct = new Product { Id = id, Name = "Old", Price = 10 };
            var updatedProduct = new Product { Id = id, Name = "Updated", Price = 20, Description = "desc" };

            _mockRepo.Setup(r => r.GetById(id)).Returns(existingProduct);
            _mockRepo.Setup(r => r.Update(existingProduct));
            _mockRepo.Setup(r => r.SaveChanges());

            // Act
            var result = _controller.Update(id, updatedProduct);

            // Assert
            Assert.IsType<NoContentResult>(result);
            Assert.Equal("Updated", existingProduct.Name);
            Assert.Equal("desc", existingProduct.Description);
            Assert.Equal(20, existingProduct.Price);

            _mockRepo.Verify(r => r.Update(existingProduct), Times.Once);
            _mockRepo.Verify(r => r.SaveChanges(), Times.Once);
        }

        [Fact]
        public void Update_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mockRepo.Setup(r => r.GetById(id)).Returns((Product?)null);

            // Act
            var result = _controller.Update(id, new Product());

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Delete_ReturnsNoContent_WhenProductExists()
        {
            // Arrange
            var id = Guid.NewGuid();
            var product = new Product { Id = id };

            _mockRepo.Setup(r => r.GetById(id)).Returns(product);
            _mockRepo.Setup(r => r.Delete(product));
            _mockRepo.Setup(r => r.SaveChanges());

            // Act
            var result = _controller.Delete(id);

            // Assert
            Assert.IsType<NoContentResult>(result);

            _mockRepo.Verify(r => r.Delete(product), Times.Once);
            _mockRepo.Verify(r => r.SaveChanges(), Times.Once);
        }

        [Fact]
        public void Delete_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mockRepo.Setup(r => r.GetById(id)).Returns((Product?)null);

            // Act
            var result = _controller.Delete(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
