namespace Campaign.Shared.Mappers
{
    public abstract class Mapper<T, TModel> where T : class
                                            where TModel : class
    {
        public abstract T Parse(MapperParam<TModel> param);
    }

    public abstract class Mapper<T, TModel1, TModel2> where T : class
                                            where TModel1 : class
                                            where TModel2 : class
    {
        public abstract T Parse(MapperParam<TModel1> param1, MapperParam<TModel2> param2);
    }

    public abstract class Mapper<T, TModel1, TModel2, TModel3> where T : class
                                            where TModel1 : class
                                            where TModel2 : class
                                            where TModel3 : class
    {
        public abstract T Parse(MapperParam<TModel1> param1, MapperParam<TModel2> param2, MapperParam<TModel3> param3);
    }
}
