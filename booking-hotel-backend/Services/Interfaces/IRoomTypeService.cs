using booking_hotel_backend.Models.DTOs.RoomType;

namespace booking_hotel_backend.Services.Interfaces;

public interface IRoomTypeService
{
    Task<IEnumerable<Models.Entities.RoomType>> GetAll();

    Task<Models.Entities.RoomType?> GetById(long id);

    Task<Models.Entities.RoomType> Create(CreateRoomTypeRequest request);

    Task<Models.Entities.RoomType> Update(long id, UpdateRoomTypeRequest request);

    Task Delete(long id);
}