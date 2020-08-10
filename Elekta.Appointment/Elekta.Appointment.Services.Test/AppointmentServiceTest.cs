using AutoFixture;
using Elekta.Appointment.Data;
using Elekta.Appointment.Services.Requests;
using Elekta.Appointment.Services.Validation;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Elekta.Appointment.Services.Test
{
    [TestFixture]
    public class AppointmentServiceTest
    {
        private MockRepository _mockRepository;
        private IFixture _fixture;

        private AppointmentDbContext _context;
        private Mock<IAppointmentRequestValidator> _validator;

        private AppointmentService _appointmentService;

        [SetUp]
        public void SetUp()
        {
            // Boilerplate
            _mockRepository = new MockRepository(MockBehavior.Strict);
            _fixture = new Fixture();

            //Prevent fixture from generating from entity circular references
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));

            // Mock setup
            _context = new AppointmentDbContext(new DbContextOptionsBuilder<AppointmentDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);

            _validator = _mockRepository.Create<IAppointmentRequestValidator>();

            // Mock default
            SetupMockDefaults();

            // Sut instantiation
            _appointmentService = new AppointmentService(
                _context,
                _validator.Object
            );
        }

        private void SetupMockDefaults()
        {
            _validator.Setup(x => x.ValidateMakeAppointmentRequestAsync(It.IsAny<AppointmentRequest>()))
                .ReturnsAsync(new ValidationResult(true));
        }

        //[Test]
        public async Task MakeAppointment_ValidatesRequestAsync()
        {
            //arrange
            var request = _fixture.Create<AppointmentRequest>();

            //act
            await _appointmentService.MakeAppointmentAsync(request);

            //assert
            _validator.Verify(x => x.ValidateMakeAppointmentRequestAsync(request), Times.Once);
        }

        //[Test]
        public void MakeAppointment_ValidatorFails_ThrowsArgumentException()
        {
            //arrange
            var failedValidationResult = new ValidationResult(false, _fixture.Create<string>());

            _validator.Setup(x => x.ValidateMakeAppointmentRequestAsync(It.IsAny<AppointmentRequest>())).ReturnsAsync(failedValidationResult);

            //act
            var exception = Assert.Throws<ArgumentException>(async () => await _appointmentService.MakeAppointmentAsync(_fixture.Create<AppointmentRequest>()));
             
            //assert
            exception.Message.Should().Be(failedValidationResult.Errors.First());
        }

    }
}
