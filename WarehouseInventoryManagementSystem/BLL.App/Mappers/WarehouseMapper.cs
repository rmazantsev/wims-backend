using AutoMapper;
using DAL.Base;

namespace BLL.App.Mappers;

public class WarehouseMapper: BaseMapper<BLL.DTO.Warehouse, DAL.DTO.Warehouse>
{
    public WarehouseMapper(IMapper mapper) : base(mapper)
    {
    }
}