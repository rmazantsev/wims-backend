using AutoMapper;
using DAL.Base;
using Domain.App;

namespace DAL.EF.App.Mappers;

public class ItemComponentMapper: BaseMapper<DAL.DTO.ItemComponent, ItemComponent>
{
    public ItemComponentMapper(IMapper mapper) : base(mapper)
    {
    }
}