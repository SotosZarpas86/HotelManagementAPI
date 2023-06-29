using Anixe.Business;
using AutoMapper;

namespace Anixe.Tests
{
    public static class MapperHelper
    {
        public static IMapper GetMapper()
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            return mapperConfig.CreateMapper();
        }
    }
}
