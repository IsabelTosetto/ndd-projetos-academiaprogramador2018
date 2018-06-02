using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerceiroReforco.Domain.Features.Rooms;

namespace TerceiroReforco.Application.Features.Rooms
{
    public interface IRoomService
    {
        Room Add(Room room);
        Room Update(Room room);
        Room Get(long id);
        IEnumerable<Room> GetAll();
        void Delete(Room room);
    }
}
