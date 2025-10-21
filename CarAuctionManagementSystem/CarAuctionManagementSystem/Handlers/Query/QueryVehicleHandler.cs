using CarAuctionManagementSystem.Controllers.DTOs;
using CarAuctionManagementSystem.Controllers.Requests.Query;
using CarAuctionManagementSystem.Domain;
using CarAuctionManagementSystem.Handlers.Query.Interfaces;
using CarAuctionManagementSystem.Mapper;
using CarAuctionManagementSystem.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CarAuctionManagementSystem.Handlers.Query
{
    public class QueryVehicleHandler : IVehicleQuery
    {
        private readonly ApplicationDbContext _dbContext;

        public QueryVehicleHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<VehicleDTO> Handle(GetVehicleQuery request)
        {
            IQueryable<Vehicle> allVehicles = _dbContext.Vehicles
                                                        .Include(v => v.Model)
                                                        .ThenInclude(m => m.Manufacturer);
            List<Vehicle> result = new List<Vehicle>();

            if (!string.IsNullOrEmpty(request.Type))
            {
                result.AddRange(GetVehiclesOfType(request.Type, allVehicles));
            }
            else
            {
                result.AddRange(allVehicles.ToList());
            }

            if (!string.IsNullOrEmpty(request.Manufacturer) && !string.IsNullOrEmpty(request.Model))
            {
                result = result.Where(v => v.Model.Manufacturer.Name.Equals(request.Manufacturer) && v.Model.Name.Equals(request.Model)).ToList();
            }
            else if (!string.IsNullOrEmpty(request.Manufacturer))
            {
                result = result.Where(v => v.Model.Manufacturer.Name.Equals(request.Manufacturer)).ToList();
            }

            if(request.Year.HasValue)
            {
                result = result.Where(v => v.Year == request.Year.Value).ToList();
            }

            return result.Select(v => VehicleMapper.DomainToDTO(v)).ToList();


        }

        private IEnumerable<Vehicle> GetVehiclesOfType(string type, IQueryable<Vehicle> query)
        {
            switch (type)
            {
                case "Sedan":
                    return _dbContext.Vehicles.OfType<Sedan>();
                case "Hatchback":
                    return _dbContext.Vehicles.OfType<Hatchback>();
                case "SUV":
                    return _dbContext.Vehicles.OfType<SUV>();
                case "Truck":
                    return _dbContext.Vehicles.OfType<Truck>();
                default:
                    return Enumerable.Empty<Vehicle>();
            }
        }
    }
}
