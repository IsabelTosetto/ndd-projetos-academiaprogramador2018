using FluentAssertions;
using Moq;
using NUnit.Framework;
using SalaDeReuniao.Common.Tests.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerceiroReforco.Application.Features.Schedulings;
using TerceiroReforco.Domain.Exceptions;
using TerceiroReforco.Domain.Features.Employees;
using TerceiroReforco.Domain.Features.Rooms;
using TerceiroReforco.Domain.Features.Schedulings;

namespace SalaDeReuniao.Application.Tests.Features.Schedulings
{
    [TestFixture]
    public class SchedulingServiceTest
    {
        private ISchedulingService _service;
        private Mock<ISchedulingRepository> _mockRepository;

        private Mock<Employee> _mockEmployee;
        private Mock<Room> _mockRoom;

        [SetUp]
        public void Initialize()
        {
            _mockRepository = new Mock<ISchedulingRepository>();
            _service = new SchedulingService(_mockRepository.Object);

            _mockEmployee = new Mock<Employee>();
            _mockRoom = new Mock<Room>();
        }

        [Test]
        public void SchedulingService_Add_ShouldBeOk()
        {
            // Cenário
            Scheduling scheduling = ObjectMother.GetScheduling();
            scheduling.Employee = _mockEmployee.Object;
            scheduling.Room = _mockRoom.Object;

            _mockRepository
                .Setup(m => m.Save(scheduling))
                .Returns(new Scheduling { Id = 1 });

            Scheduling retorno = new Scheduling()
            {
                StartTime = new DateTime(2018, 6, 23, 7, 0, 0),
                EndTime = new DateTime(2018, 6, 23, 10, 0, 0)
            };

        _mockRepository
               .Setup(m => m.GetAll())
               .Returns(new List<Scheduling>
               {
                   retorno
               });

            // Ação
            Scheduling result = _service.Add(scheduling);

            // Verifica
            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
            _mockRepository.Verify(m => m.Save(scheduling));
        }

        [Test]
        public void SchedulingService_Add_UnavailableRoom_ShouldBeFail()
        {
            // Cenário
            Scheduling scheduling = new Scheduling()
            {
                StartTime = new DateTime(2018, 10, 23, 21, 0, 0),
                EndTime = new DateTime(2018, 10, 23, 22, 0, 0)
            };
            scheduling.Employee = _mockEmployee.Object;
            scheduling.Room = _mockRoom.Object;

            _mockRepository
               .Setup(m => m.GetAll())
               .Returns(new List<Scheduling>
               {
                   scheduling
               });

            // Ação
            Action action = () => _service.Add(scheduling);

            // Verifica
            action.Should().Throw<SchedulingUnavailableRoomException>();
        }

        [Test]
        public void SchedulingService_Save_InvalidEmptyOrNullName_ShouldBeFail()
        {
            // Cenário
            Scheduling scheduling = ObjectMother.GetSchedulingInvalidStartTime();
            scheduling.Employee = _mockEmployee.Object;
            scheduling.Room = _mockRoom.Object;

            // Ação
            Action executeAction = () => _service.Add(scheduling);

            // Verifica
            executeAction.Should().Throw<SchedulingStartTimeOverFlowException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void SchedulingService_Update_ShouldBeOk()
        {
            // Cenário
            Scheduling scheduling = ObjectMother.GetScheduling();
            scheduling.Id = 1;
            scheduling.Employee = _mockEmployee.Object;
            scheduling.Room = _mockRoom.Object;

            _mockRepository
                .Setup(m => m.Update(scheduling))
                .Returns(new Scheduling { Id = 1 });

            Scheduling retorno = new Scheduling()
            {
                StartTime = new DateTime(2018, 6, 23, 7, 0, 0),
                EndTime = new DateTime(2018, 6, 23, 10, 0, 0)
            };

            _mockRepository
                   .Setup(m => m.GetAll())
                   .Returns(new List<Scheduling>
                   {
                   retorno
                   });

            // Ação
            Scheduling result = _service.Update(scheduling);

            // Verifica
            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
            _mockRepository.Verify(m => m.Update(scheduling));
        }

        [Test]
        public void SchedulingService_Update_UnavailableRoom_ShouldBeOk()
        {
            // Cenário
            Scheduling scheduling = ObjectMother.GetScheduling();
            scheduling.Id = 1;
            scheduling.Employee = _mockEmployee.Object;
            scheduling.Room = _mockRoom.Object;

            _mockRepository
                .Setup(m => m.Update(scheduling))
                .Returns(new Scheduling { Id = 1 });

            _mockRepository
                   .Setup(m => m.GetAll())
                   .Returns(new List<Scheduling>
                   {
                   scheduling
                   });

            // Ação
            Action action = () => _service.Update(scheduling);

            // Verifica
            action.Should().Throw<SchedulingUnavailableRoomException>();
        }

        [Test]
        public void SchedulingService_Update_InvalidEndTimeLessThanStartTime_ShouldBeFail()
        {
            // Cenário
            Scheduling scheduling = ObjectMother.GetSchedulingInvalidEndTime();
            scheduling.Id = 1;
            scheduling.Employee = _mockEmployee.Object;
            scheduling.Room = _mockRoom.Object;

            // Ação
            Action executeAction = () => _service.Update(scheduling);

            // Verifica
            executeAction.Should().Throw<SchedulingEndTimeLessThanStartTimeException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void SchedulingService_Update_InvalidId_ShouldBeFail()
        {
            // Cenário
            Scheduling scheduling = ObjectMother.GetScheduling();
            scheduling.Employee = _mockEmployee.Object;
            scheduling.Room = _mockRoom.Object;

            // Ação
            Action executeAction = () => _service.Update(scheduling);

            // Verifica
            executeAction.Should().Throw<IdentifierUndefinedException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void SchedulingService_Get_ShouldBeOk()
        {
            // Cenário
            int id = 1;

            _mockRepository
                .Setup(m => m.Get(id))
                .Returns(ObjectMother.GetScheduling());

            // Ação
            Scheduling result = _service.Get(id);

            // Verifica
            result.Should().NotBeNull();
            _mockRepository.Verify(m => m.Get(id));
        }

        [Test]
        public void SchedulingService_Get_InvalidId_ShouldBeFail()
        {
            // Ação
            Action executeAction = () => _service.Get(0);

            // Verifica
            executeAction.Should().Throw<IdentifierUndefinedException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void SchedulingService_GetAll_ShouldBeOk()
        {
            // Cenário
            _mockRepository
                .Setup(m => m.GetAll())
                .Returns(new List<Scheduling>()
                        {
                            new Scheduling { Id = 1 },
                            new Scheduling { Id = 2 },
                            new Scheduling { Id = 3 }
                        });

            // Ação
            IEnumerable<Scheduling> schedulings = _service.GetAll();

            // Verifica
            schedulings.Count().Should().Equals(3);
            _mockRepository.Verify(m => m.GetAll());
        }

        [Test]
        public void SchedulingService_Delete_ShouldBeOk()
        {
            // Cenário
            Scheduling scheduling = ObjectMother.GetScheduling();
            scheduling.Id = 1;

            _mockRepository
                .Setup(m => m.Delete(scheduling));

            // Ação
            _service.Delete(scheduling);

            // Verifica
            _mockRepository.Verify(m => m.Delete(scheduling));
        }

        [Test]
        public void SchedulingService_Delete_InvalidId_ShouldBeFail()
        {
            // Cenário
            Scheduling scheduling = ObjectMother.GetScheduling();

            _mockRepository
                .Setup(m => m.Delete(scheduling));

            // Ação
            Action executeAction = () => _service.Delete(scheduling);

            // Verifica
            executeAction.Should().Throw<IdentifierUndefinedException>();
            _mockRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void SchedulingService_CheckAvailableRoom_ShouldBeFail()
        {
            //Cenário 
            Scheduling scheduling = ObjectMother.GetScheduling();
            scheduling.Id = 1;
            scheduling.Employee = _mockEmployee.Object;
            scheduling.Room = _mockRoom.Object;

            _mockRepository
                .Setup(m => m.GetAll())
                .Returns(new List<Scheduling>()
                        {
                            scheduling
                        });

            //Ação
            Action action = () => _service.CheckAvailableRoom(scheduling);

            //Verificar
            action.Should().Throw<SchedulingUnavailableRoomException>();
            _mockRepository.Verify(m => m.GetAll());
        }
    }
}
