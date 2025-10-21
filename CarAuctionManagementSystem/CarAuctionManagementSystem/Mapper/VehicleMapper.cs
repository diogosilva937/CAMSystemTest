using CarAuctionManagementSystem.Controllers.DTOs;
using CarAuctionManagementSystem.Domain;

namespace CarAuctionManagementSystem.Mapper
{
    public class VehicleMapper
    {
        public static VehicleDTO DomainToDTO(Vehicle v)
        {
            return new VehicleDTO
            (
                v.RegistrationNumber,
                new VehicleModelDTO
                (
                    v.Model.Name,
                    new VehicleManufacturerDTO
                    (
                        v.Model.Manufacturer.Name
                    )
                ),
                v.Year,
                v.StartingBid,
                v.GetType().Name
            );

        }
    }
}
