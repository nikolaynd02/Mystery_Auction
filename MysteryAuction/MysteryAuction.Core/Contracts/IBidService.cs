using MysteryAuction.Core.Models.Bid;

namespace MysteryAuction.Core.Contracts
{
    public interface IBidService
    {
        public Task AddBidAsync(AddBidViewModel model);


        public Task PickWinnerAsync();
    }
}
