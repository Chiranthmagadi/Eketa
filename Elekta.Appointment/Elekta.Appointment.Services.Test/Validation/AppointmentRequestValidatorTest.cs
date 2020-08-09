using AutoFixture;
using Elekta.Appointment.Data;
using Elekta.Appointment.Data.Modles;
using Elekta.Appointment.Services.Requests;
using Elekta.Appointment.Services.Validation;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;

namespace Elekta.Appointment.Services.Test.Validation
{
    [TestFixture]
    public class AppointmentRequestValidatorTest
    {
        private IFixture _fixture;

        private AppointmentDbContext _context;

        private AppointmentRequestValidator _validator;

        //do not remove
        private static DateTime _testData1 = new DateTime(2020, 11, 8, 9, 0, 0);
        private static DateTime _testData2 = new DateTime(2020, 11, 8, 7, 0, 0);
        private static DateTime _testData3 = new DateTime(2020, 08, 12, 9, 0, 0);

        [SetUp]
        public void SetUp()
        {
            // Boilerplate
            _fixture = new Fixture();

            //Prevent fixture from generating from entity circular references 
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));

            // Mock setup
            _context = new AppointmentDbContext(new DbContextOptionsBuilder<AppointmentDbContext>().Options);

            // Mock default
            SetupMockDefaults();

            // Sut instantiation
            _validator = new AppointmentRequestValidator(
                _context
            );
        }

        private void SetupMockDefaults()
        {

        }

        [Test]
        public void MAkeAppointment_AppointmentDateWithCorrectDateAndTime_ReturnsSuccessValidationResult([ValueSource("_testDataPass")] DateTime bookingDate)
        {
            //arrange
            var request = GetValidRequest();
            request.AppointmentDate = bookingDate;

            //act
            var res = _validator.ValidateMakeAppointmentRequest(request);

            //assert
            res.PassedValidation.Should().BeTrue();
        }

        [Test]
        public void MakeAppointment_AppointmentDateWithWrongTime_ReturnsFailedValidationResult([ValueSource("_testDataFail1")] DateTime bookingDate)
        {
            //arrange
            var request = GetValidRequest();
            request.AppointmentDate = bookingDate;

            //act
            var res = _validator.ValidateMakeAppointmentRequest(request);

            //assert
            res.PassedValidation.Should().BeFalse();
            res.Errors.Should().Contain("Appointments can be made between 08:00 and 16:00!");
        }

        [Test]
        public void MakeAppointment_AppointmentDateWithWrongDate_ReturnsFailedValidationResult([ValueSource("_testDataFail2")] DateTime bookingDate)
        {
            //arrange
            var request = GetValidRequest();
            request.AppointmentDate = bookingDate;

            //act
            var res = _validator.ValidateMakeAppointmentRequest(request);

            //assert
            res.PassedValidation.Should().BeTrue();
            res.Errors.Should().Contain("Appointments can only be made for 2 weeks later at most!");
            //
        }

        [Test]
        public void Cancel_Appointment_AppointmentDoesNotExist_ReturnsFailedValidationResult([ValueSource("_testDataFail")] DateTime bookingDate)
        {
            //arrange
            var request = GetValidRequest();
            request.AppointmentDate = bookingDate;

            //act
            var res = _validator.ValidateCancelAppointmentRequest(request);

            //assert
            res.PassedValidation.Should().BeTrue();
        }

        [Test]
        public void Cancel_Appointment_CannotBeCancelledBefore3Days_ReturnsFailedValidationResult([ValueSource("_testDataFail1")] DateTime bookingDate)
        {
            //arrange
            var request = GetValidRequest();
            request.AppointmentDate = bookingDate;

            //act
            var res = _validator.ValidateMakeAppointmentRequest(request);

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
