using AutoMapper;
using DAL.Base;
using Domain.App;

namespace BLL.App.Mappers;

public class ItemComponentMapper: BaseMapper<BLL.DTO.ItemComponent, DAL.DTO.ItemComponent>
{
    public ItemComponentMapper(IMapper mapper) : base(mapper)
    {
    }
}