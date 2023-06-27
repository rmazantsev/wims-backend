using BLL.Base;
using BLL.Contracts.App;
using Contracts.Base;
using DAL.Contracts.App;
using DAL.DTO;

namespace BLL.App.Services;

public class InventoryTransactionService :
    BaseEntityService<BLL.DTO.InventoryTransaction, DAL.DTO.InventoryTransaction, IInventoryTransactionRepository>,
    IInventoryTransactionService
{
    protected IAppUOW Uow;

    public InventoryTransactionService(IAppUOW uow, IMapper<BLL.DTO.InventoryTransaction, InventoryTransaction> mapper)
        : base(uow.InventoryTransactionRepository, mapper)
    {
        Uow = uow;
    }


    public async Task<IEnumerable<DTO.InventoryTransaction?>> AllAsyncById(Guid id)
    {
        return (await Uow.InventoryTransactionRepository.AllAsyncById(id)).Select(e => Mapper.Map(e));
    }

    public async Task<DTO.InventoryTransaction?> FindAsync(Guid id, Guid userId)
    {
        return Mapper.Map(await Uow.InventoryTransactionRepository.FindAsync(id, userId));
    }

    public async Task<DTO.InventoryTransaction?> RemoveAsync(Guid id, Guid userId)
    {
        return Mapper.Map(await Uow.InventoryTransactionRepository.RemoveAsync(id, userId));
    }

    public async Task<bool> IsOwnedByUserAsync(Guid id, Guid userId)
    {
        return await Uow.InventoryTransactionRepository.IsOwnedByUserAsync(id, userId);
    }
}