using AutoFixture;
using Elekta.Appointment.Data;
using Elekta.Appointment.Data.Modles;
using Elekta.Appointment.Services.Requests;
using Elekta.Appointment.Services.Validation;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Elekta.Appointment.Services.Test.Validation
{
    [TestFixture]
    public class AppointmentRequestValidatorTest
    {
        private MockRepository _mockRepository;

        private IFixture _fixture;

        private AppointmentDbContext _context;

        private AppointmentRequestValidator _validator;

        //do not remove
        private static DateTime _testDataPass = new DateTime(2020, 11, 8, 9, 0, 0);
        private static DateTime _testDataFail1 = new DateTime(2020, 11, 8, 7, 0, 0);
        private static DateTime _testDataFail2 = new DateTime(2020, 08, 12, 9, 0, 0);

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

            // Sut instantiation
            _validator = new AppointmentRequestValidator(
                _context
            );
        }

        [Test]
        public async Task MakeAppointment_AppointmentDateWithCorrectDateAndTime_ReturnsSuccessValidationResult()
        {
            //arrange
            var request = GetValidRequest();
            request.AppointmentDate = new DateTime(2020, 10, 8, 10, 10, 10);

            //act
            var res = await _validator.ValidateMakeAppointmentRequestAsync(request);

            //assert
            res.PassedValidation.Should().BeTrue();
        }

        [Test]
        public async Task MakeAppointment_AppointmentDateWithWrongTime_ReturnsFailedValidationResultAsync()
        {
            //arrange
            var request = GetValidRequest();
            request.AppointmentDate = new DateTime(2020, 10, 8, 7, 10, 10);

            //act
            var res = await _validator.ValidateMakeAppointmentRequestAsync(request);

            //assert
            res.PassedValidation.Should().BeFalse();
            res.Errors.Should().Contain("Appointments can be made between 08:00 and 16:00!");
        }

        [Test]
        public async Task MakeAppointment_AppointmentDateWithWrongDate_ReturnsFailedValidationResult()
        {
            //arrange
            var request = GetValidRequest();
            request.AppointmentDate = new DateTime(2020, 08, 15, 10, 10, 10);

            //act
            var res = await _validator.ValidateMakeAppointmentRequestAsync(request);

            //assert
            res.PassedValidation.Should().BeFalse();
            res.Errors.Should().Contain("Appointments can only be made for 2 weeks later at most!");
            //
        }

        [Test]
        public void Cancel_Appointment_AppointmentDoesNotExist_ReturnsFailedValidationResult()
        {
            //arrange
            var request = GetValidRequest();
            request.AppointmentDate = new DateTime(2020, 10, 8, 10, 10, 10);

            //act
            var res = _validator.ValidateCancelAppointmentRequest(request);

            //assert
            res.PassedValidation.Should().BeFalse();
        }

        [Test]
        public async Task Cancel_Appointment_CannotBeCancelledBefore3Days_ReturnsFailedValidationResult()
        {
            //arrange
            var request = GetValidRequest();
            request.AppointmentDate = new DateTime(2020, 10, 8, 10, 10, 10);

            //act
            var res = await _validator.ValidateMakeAppointmentRequestAsync(request);

            //assert
            res.PassedValidation.Should().BeTrue();
        }

        private AppointmentRequest GetValidRequest()
        {
            var data = _fixture.Create<AppointmentModel>();
            _context.Appointments.Add(data);
            _context.SaveChanges();

            var request = _fixture.Build<AppointmentRequest>()
                .With(x => x.PatientId, data.Id)
                .With(x => x.AppointmentDate, data.AppointmentDate)
                //.With(x => x.NewAppointmentDate, data.NewAppointmentDate)
                .Create();
            return request;
        }
    }
}
