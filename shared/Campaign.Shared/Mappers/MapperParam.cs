namespace Campaign.Shared.Mappers
{
    public class MapperParam<TModel>
    {
        public TModel Model { get; } = default!;
        public MapperParam(TModel model) => Model = model;
    }
}
