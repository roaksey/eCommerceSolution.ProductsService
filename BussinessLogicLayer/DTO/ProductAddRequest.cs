

namespace BussinessLogicLayer.DTO;

public record ProductAddRequest(string ProductName, CategoryOptions category, double? UnitPrice, int? QuantiryInStock)
{
    public ProductAddRequest() : this(default, default, default, default)
    {

    }
}

