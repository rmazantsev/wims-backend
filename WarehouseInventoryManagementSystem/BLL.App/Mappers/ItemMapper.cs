using AutoMapper;
using DAL.Base;
using Domain.App;

namespace BLL.App.Mappers;

public class ItemMapper: BaseMapper<BLL.DTO.Item, DAL.DTO.Item>
{
    public ItemMapper(IMapper mapper) : base(mapper)
    {
    }

    // public Item MapWithCount(ItemWithItemComponents entity)
    // {
    //     return Mapper.Map<Item>(entity);
    // }
}