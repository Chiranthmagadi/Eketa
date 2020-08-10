using AutoFixture;
using Elekta.Appointment.Data;
using Elekta.Appointment.Data.Modles;
using Elekta.Appointment.Services.Helper;
using Elekta.Appointment.Services.Requests;
using Elekta.Appointment.Services.Validation;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Elekta.Appointment.Services.Test.Validation
{
    [TestFixture]
    public class AppointmentRequestValidatorTest
    {
        private MockRepository _mockRepository;

        private IFixture _fixture;

        private AppointmentDbContext _context;
        private Mock<IHttpHandler> _httpHandlerMock;
        private AppointmentRequestValidator _validator;

        private DateTime date1 = new DateTime(2020, 08, 20, 9, 0, 0);
        private DateTime date2 = new DateTime(2020, 08, 20, 10, 0, 0);
        private DateTime date3 = new DateTime(2020, 08, 20, 11, 0, 0);
        private DateTime date4 = new DateTime(2020, 08, 20, 12, 0, 0);

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
            _httpHandlerMock = _mockRepository.Create<IHttpHandler>();

            // Mock default
            SetupMockDefaults();


            // Sut instantiation
            _validator = new AppointmentRequestValidator(
                _context, _httpHandlerMock.Object
            );
        }

        private void SetupMockDefaults()
        {
            var msg = new HttpResponseMessage();
            msg.Content = new StringContent("true");
            _httpHandlerMock.Setup(x => x.PostAsync(It.IsAny<string>(), It.IsAny<HttpContent>())).Returns(Task.FromResult(msg));
        }

        [Test]
        public async Task MakeAppointment_AppointmentDateWithCorrectDateAndTime_ReturnsSuccessValidationResult()
        {
            //arrange
            var request = GetValidRequest();

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
            res.Errors.Should().Contain("The appointment does not exist!");

        }

        private AppointmentRequest GetValidRequest()
        {
            var request = new AppointmentRequest
            {
                PatientId = 1,
                AppointmentDate = date1.AddDays(25),
                ChangeAppointmentDate = date1.AddDays(30)
            };

            return request;
        }
    }
}
