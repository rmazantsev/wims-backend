using AutoMapper;
using DAL.Base;
using Domain.App;

namespace DAL.EF.App.Mappers;

public class CategoryMapper: BaseMapper<DAL.DTO.Category, Category>
{
    public CategoryMapper(IMapper mapper) : base(mapper)
    {
    }
}