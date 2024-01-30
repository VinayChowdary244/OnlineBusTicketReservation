using BusModelLibrary;
using BusTicketingWebApplication.Exceptions;
using BusTicketingWebApplication.Interfaces;
using BusTicketingWebApplication.Models.DTOs;

namespace BusTicketingWebApplication.Services
{
    /// <summary>
    /// Service class for managing bus-related operations.
    /// </summary>
    public class BusService : IBusService
    {
        private readonly IBusRepository _busRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="BusService"/> class.
        /// </summary>
        /// <param name="repository">The bus repository.</param>
        public BusService(IBusRepository repository)
        {
            _busRepository = repository;
        }

        /// <summary>
        /// Adds a new bus.
        /// </summary>
        /// <param name="bus">The bus object to be added.</param>
        /// <returns>The added bus.</returns>
        public Bus Add(Bus bus)
        {
            var result = _busRepository.Add(bus);
            return result;
        }

        /// <summary>
        /// Removes a bus by its ID.
        /// </summary>
        /// <param name="busIdDTO">DTO containing the bus ID to be removed.</param>
        /// <returns>The DTO of the removed bus.</returns>
        public BusIdDTO RemoveBus(BusIdDTO busIdDTO)
        {
            var BusToBeRemoved = _busRepository.GetById(busIdDTO.Id);
            if (BusToBeRemoved != null)
            {
                var result = _busRepository.Delete(busIdDTO.Id);
                if (result != null)
                {
                    return busIdDTO;
                }
            }
            return null;
        }

        /// <summary>
        /// Gets a list of all buses.
        /// </summary>
        /// <returns>The list of buses.</returns>
        public List<Bus> GetBuses()
        {
            var buses = _busRepository.GetAll();
            if (buses != null)
            {
                return buses.ToList();
            }
            throw new NoBusesAvailableException();
        }

        /// <summary>
        /// Updates bus information.
        /// </summary>
        /// <param name="busDTO">DTO containing the updated bus information.</param>
        /// <returns>The updated bus DTO.</returns>
        public BusDTO UpdateBus(BusDTO busDTO)
        {
            var busData = _busRepository.GetById(busDTO.Id);
            busData.Type = busDTO.Type;
            busData.Cost = busDTO.Cost;
            busData.Start = busDTO.Start;
            busData.End = busDTO.End;
            if (busData != null)
            {
                var result = _busRepository.Update(busData);
                if (result != null)
                {
                    return busDTO;
                }
            }
            return null;
        }

        /// <summary>
        /// Gets a bus by its ID.
        /// </summary>
        /// <param name="busIdDTO">DTO containing the bus ID.</param>
        /// <returns>The bus with the specified ID.</returns>
        public Bus GetBusById(BusIdDTO busIdDTO)
        {
            var result = _busRepository.GetById(busIdDTO.Id);
            return result;
        }
    }
}
