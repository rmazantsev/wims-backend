using AutoMapper;
using DAL.Base;
using Domain.App;
using Store = DAL.DTO.Store;

namespace Public.DTO.Mappers;

public class StoreMapper: BaseMapper<BLL.DTO.Store, v1.Store>
{
    public StoreMapper(IMapper mapper) : base(mapper)
    {
    }
}