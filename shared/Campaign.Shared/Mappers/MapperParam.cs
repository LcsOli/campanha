namespace Campaign.Shared.Mappers
{
    public abstract class MapperParam<TModel>
    {
        public TModel Model { get; } = default!;
        protected MapperParam() { }
        protected MapperParam(TModel model) => Model = model;
    }
}
