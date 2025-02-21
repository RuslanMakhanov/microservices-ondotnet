using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Queries
{
    public class GetProductByBrandQuery : IRequest<List<ProductResponse>>
    {
        public string BrandName { get; set; }
        public GetProductByBrandQuery(string brandName)
        {
            BrandName = brandName;
        }

    }
}
