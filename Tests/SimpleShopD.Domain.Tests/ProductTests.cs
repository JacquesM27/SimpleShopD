using SimpleShopD.Domain.Enum;
using SimpleShopD.Domain.Products;
using SimpleShopD.Domain.Products.Exceptions;
using FluentAssertions;

namespace SimpleShopD.Domain.Tests
{
    public class ProductTests
    {
        [Fact]
        public void NewProduct_ForEmptyTitle_ShouldThrowEmptyTitleExeption()
        {
            // Arrange
            // Act
            var exception = Record.Exception(() => new Product(Guid.NewGuid(), string.Empty, string.Empty, ProductType.Product, 0));
            
            // Assert
            exception.Should().BeOfType<EmptyTitleExeption>();
            exception.Message.Should().Be("Title cannot be empty.");
        }

        [Fact]
        public void NewProduct_ForEmptyDescription_ShouldThrowEmptyDescriptionExeption()
        {
            // Arrange
            string title = "Some not empty title";
            // Act
            var exception = Record.Exception(() => new Product(Guid.NewGuid(), title, string.Empty, ProductType.Product, 0));

            // Assert
            exception.Should().BeOfType<EmptyDescriptionException>();
            exception.Message.Should().Be("Description cannot be empty.");
        }

        [Fact]
        public void NewProduct_ForInvalidPrice_ShouldThrowInvalidPriceValueException()
        {
            // Arrange
            string title = "Some not empty title";
            string description = "Some not empty description";
            // Act
            var exception = Record.Exception(() => new Product(Guid.NewGuid(), title, description, ProductType.Product, -1));

            // Assert
            exception.Should().BeOfType<IncorrectPriceValueException>();
            exception.Message.Should().Be("Price cannot be negative.");
        }

        [Fact]
        public void NewProduct_ForValidData_ShouldNotThrowAnyException()
        {
            // Arrange
            string title = "Some not empty title";
            string description = "Some not empty description";
            // Act
            var exception = Record.Exception(() => new Product(Guid.NewGuid(), title, description, ProductType.Product, 0));

            // Assert
            exception.Should().Be(null);
        }

        [Fact]
        public void NewProduct_ForValidData_ShouldCreateANewProduct()
        {
            // Arrange
            string title = "Some not empty title";
            string description = "Some not empty description";
            decimal price = 61;
            Guid id = Guid.NewGuid();

            // Act
            var product = new Product(id, title, description, ProductType.Product, price);

            // Assert
            product.TypeOfProduct.ProductType.Should().Be(ProductType.Product);
            product.Description.Value.Should().Be(description);
            product.Title.Value.Should().Be(title);
            product.Price.Value.Should().Be(price);
            product.Id.Should().Be(id);
        }
    }
}
