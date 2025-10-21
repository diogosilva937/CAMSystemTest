using CarAuctionManagementSystem.Controllers.DTOs;
using CarAuctionManagementSystem.Controllers.Requests.Query;

namespace CarAuctionManagementSystem.Handlers.Query.Interfaces
{
    public interface IVehicleQuery
    {
        List<VehicleDTO> Handle(GetVehicleQuery vehicleRequest);
    }
}
