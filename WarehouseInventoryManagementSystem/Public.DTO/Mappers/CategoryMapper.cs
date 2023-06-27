using AutoMapper;
using DAL.Base;

namespace Public.DTO.Mappers;

public class CategoryMapper: BaseMapper<BLL.DTO.Category, v1.Category>
{
    public CategoryMapper(IMapper mapper) : base(mapper)
    {
    }
}