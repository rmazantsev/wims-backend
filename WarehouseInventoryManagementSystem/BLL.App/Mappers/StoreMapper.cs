using AutoMapper;
using DAL.Base;
using Store = DAL.DTO.Store;

namespace BLL.App.Mappers;

public class StoreMapper: BaseMapper<BLL.DTO.Store, DAL.DTO.Store>
{
    public StoreMapper(IMapper mapper) : base(mapper)
    {
    }
}