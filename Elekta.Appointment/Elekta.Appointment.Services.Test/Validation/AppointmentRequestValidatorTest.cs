using AutoFixture;
using Elekta.Appointment.Data;
using Elekta.Appointment.Data.Modles;
using Elekta.Appointment.Services.Requests;
using Elekta.Appointment.Services.Validation;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Elekta.Appointment.Services.Test.Validation
{
    [TestFixture]
    public class AppointmentRequestValidatorTest
    {
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

        public void ValidateRequest_BookingDateWithCorrectDateAndTime_ReturnsSuccessValidationResult([ValueSource("_testDataPass")] DateTime bookingDate)
        {
            //arrange
            var request = GetValidRequest();
            request.BookingDate = bookingDate;

            //act
            var res = _validator.ValidateMakeAppointmentRequest(request);

            //assert
            res.PassedValidation.Should().BeTrue();
        }

        public void ValidateRequest_BookingDateWithWrongTime_ReturnsFailedValidationResult([ValueSource("_testDataFail1")] DateTime bookingDate)
        {
            //arrange
            var request = GetValidRequest();
            request.BookingDate = bookingDate;

            //act
            var res = _validator.ValidateMakeAppointmentRequest(request);

            //assert
            res.PassedValidation.Should().BeFalse();
            res.Errors.Should().Contain("Appointments can be made between 08:00 and 16:00!");
        }

        public void ValidateRequest_BookingDateWithWrongDate_ReturnsFailedValidationResult([ValueSource("_testDataFail2")] DateTime bookingDate)
        {
            //arrange
            var request = GetValidRequest();
            request.BookingDate = bookingDate;

            //act
            var res = _validator.ValidateMakeAppointmentRequest(request);

            //assert
            res.PassedValidation.Should().BeTrue();
            res.Errors.Should().Contain("Appointments can only be made for 2 weeks later at most!");
        }


        private AppointmentRequest GetValidRequest()
        {
            var data = _fixture.Create<Appointments>();
            _context.Appointments.Add(data);
            _context.SaveChanges();

            var request = _fixture.Build<AppointmentRequest>()
                .With(x => x.PatientId, data.AppointmentId)
                .With(x => x.BookingDate, data.BookingDate)
                .Create();
            return request;
        }
    }
}
