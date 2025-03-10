using Grpc.Core;
using ProductService;

namespace Application.GrpcServices;

public class ProductGetter : ProductService.ProductService.ProductServiceClient
{
	public override ProductReply GetProduct(ProductRequest request, CallOptions options) => base.GetProduct(request, options);
}
