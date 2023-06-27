using AutoMapper;
using DAL.Base;
using DAL.DTO;

namespace Public.DTO.Mappers;

public class ItemMapper: BaseMapper<BLL.DTO.Item, v1.Item>
{
    public ItemMapper(IMapper mapper) : base(mapper)
    {
    }

    public v1.Item MapWithCount(ItemWithItemComponents entity)
    {
        return Mapper.Map<v1.Item>(entity);
    }
}