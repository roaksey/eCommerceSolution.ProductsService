

namespace BussinessLogicLayer.DTO;

public record ProductResponse(Guid ProductID,string? ProductName, CategoryOptions Category, double? UnitPrice, int? QuantiryInStock)
{
    public ProductResponse() : this(default,default, default, default, default)
    {

    }
}

