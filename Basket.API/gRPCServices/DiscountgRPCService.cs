using Discount.gRPC.Protos;

namespace Basket.API.gRPCServices
{
    public class DiscountgRPCService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _service;

        public DiscountgRPCService(DiscountProtoService.DiscountProtoServiceClient client)
        {
            _service = client;
        }

        public async Task<CouponModel> GetDiscount(string productName)
        {
            var discountRequest = new GetDiscountRequest { ProductName = productName };
            return await _service.GetDiscountAsync(discountRequest);
        }
    }
}
