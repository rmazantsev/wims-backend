using AutoMapper;
using DAL.Base;
using Domain.App;

namespace DAL.EF.App.Mappers;

public class ItemMapper: BaseMapper<DAL.DTO.Item, Item>
{
    public ItemMapper(IMapper mapper) : base(mapper)
    {
    }

    // public Item MapWithCount(ItemWithItemComponents entity)
    // {
    //     return Mapper.Map<Item>(entity);
    // }
}