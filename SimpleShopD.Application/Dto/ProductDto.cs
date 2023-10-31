namespace SimpleShopD.Application.Dto;

public record ProductDto(Guid Id, string Title, string Description, string TypeOfProduct, decimal Price);