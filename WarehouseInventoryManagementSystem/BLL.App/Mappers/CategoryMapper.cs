using AutoMapper;
using DAL.Base;

namespace BLL.App.Mappers;

public class CategoryMapper: BaseMapper<BLL.DTO.Category, DAL.DTO.Category>
{
    public CategoryMapper(IMapper mapper) : base(mapper)
    {
    }
}