using AutoMapper;
using DAL.Base;
using Domain.App;

namespace DAL.EF.App.Mappers;

public class StoreMapper: BaseMapper<DAL.DTO.Store, Store>
{
    public StoreMapper(IMapper mapper) : base(mapper)
    {
    }
}