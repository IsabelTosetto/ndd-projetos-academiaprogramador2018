using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerceiroReforco.Domain.Exceptions;
using TerceiroReforco.Domain.Features.Rooms;

namespace TerceiroReforco.Application.Features.Rooms
{
    public class RoomService : IRoomService
    {
        private IRoomRepository _repository;

        public RoomService(IRoomRepository repository)
        {
            _repository = repository;
        }

        public Room Add(Room room)
        {
            room.Validate();

            return _repository.Save(room);
        }

        public Room Update(Room room)
        {
            if (room.Id < 1)
                throw new IdentifierUndefinedException();

            room.Validate();

            return _repository.Update(room);
        }

        public Room Get(long id)
        {
            if (id < 1)
                throw new IdentifierUndefinedException();

            return _repository.Get(id);
        }

        public IEnumerable<Room> GetAll()
        {
            return _repository.GetAll();
        }

        public void Delete(Room room)
        {
            if (room.Id < 1)
                throw new IdentifierUndefinedException();

            _repository.Delete(room);
        }
    }
}
