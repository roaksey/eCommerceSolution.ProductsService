

namespace BussinessLogicLayer.DTO;

public record ProductUpdateRequest(Guid ProductID,string ProductName, CategoryOptions category, double? UnitPrice, int? QuantiryInStock)
{
    public ProductUpdateRequest() : this(default,default, default, default, default)
    {

    }
}

