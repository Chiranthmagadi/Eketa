using AutoFixture;
using Elekta.Appointment.Data;
using Elekta.Appointment.Services.Requests;
using Elekta.Appointment.Services.Validation;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            _context = new AppointmentDbContext(new DbContextOptionsBuilder<AppointmentDbContext>().Options);
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
            _validator.Setup(x => x.ValidateMakeAppointmentRequest(It.IsAny<AppointmentRequest>()))
                .Returns(new ValidationResult(true));
        }

        [Test]
        public void MakeAppointment_ValidatesRequest()
        {
            //arrange
            var request = _fixture.Create<AppointmentRequest>();

            //act
            _appointmentService.MakeAppointment(request);

            //assert
            _validator.Verify(x => x.ValidateMakeAppointmentRequest(request), Times.Once);
        }

        [Test]
        public void MakeAppointment_ValidatorFails_ThrowsArgumentException()
        {
            //arrange
            var failedValidationResult = new ValidationResult(false, _fixture.Create<string>());

            _validator.Setup(x => x.ValidateMakeAppointmentRequest(It.IsAny<AppointmentRequest>())).Returns(failedValidationResult);

            //act
            var exception = Assert.Throws<ArgumentException>(() => _appointmentService.MakeAppointment(_fixture.Create<AppointmentRequest>()));
             
            //assert
            exception.Message.Should().Be(failedValidationResult.Errors.First());
        }

    }
}
