using AutoMapper;
using DAL.Base;
using Domain.App;

namespace Public.DTO.Mappers;

public class ItemComponentMapper: BaseMapper<BLL.DTO.ItemComponent, v1.ItemComponent>
{
    public ItemComponentMapper(IMapper mapper) : base(mapper)
    {
    }
}