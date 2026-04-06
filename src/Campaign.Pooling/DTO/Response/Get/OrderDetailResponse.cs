namespace Campaign.Pooling.DTO.Response.Get
{
    //TODO - Os resultados de pesquisa no banco devem ter seus proprios objetos de resposta
    //Para evitar acoplamento de objetos de banco e objetos de resposta para a view (DTOs)
    public record OrderDetailResponse(int SellerId,
                                      decimal Price,
                                      int ProductId,
                                      int ConsumerId,
                                      decimal Quantity,
                                      DateTime DateOfSale,
                                      decimal? ProductPromotionPoints);
}
