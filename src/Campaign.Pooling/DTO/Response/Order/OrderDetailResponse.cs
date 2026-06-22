namespace Campaign.Pooling.DTO.Response.Order
{
    //TODO - Os resultados de pesquisa no banco devem ter seus proprios objetos de resposta
    //Para evitar acoplamento de objetos de banco e objetos de resposta para a view (DTOs)
    public record OrderDetailResponse(int OrderId,
                                      int SellerId,
                                      int ProductId,
                                      int ConsumerId,
                                      decimal Quantity,
                                      string ClientName,
                                      DateTime DateOfSale,
                                      string ProductDescription,
                                      decimal? ProductPromotionPoints);
}
