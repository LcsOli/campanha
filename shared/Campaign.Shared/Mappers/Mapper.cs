namespace Campaign.Shared.Mappers
{
    public abstract class Mapper<T, TModel> where T : class
                                            where TModel : class
    {
        public abstract T Parse(MapperParam<TModel> param);
    }
}
