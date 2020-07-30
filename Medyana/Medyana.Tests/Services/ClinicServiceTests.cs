using System;
using System.Collections.Generic;
using System.Linq;

using Moq;
using NUnit.Framework;

using Medyana.Business.Services;
using Medyana.Common.Contracts;
using Medyana.DataAccess.UnitOfWork;
using Medyana.Domain.Entities;
using Medyana.Tests.Helpers;

namespace Medyana.Tests.Services
{
    [TestFixture]
    public class ClinicServiceTests
    {
        private Mock<IUnitOfWork> _unitOfWork;
        private ClinicService _clinicService;
        private AssertHelper<Clinic> _assertHelper;

        [SetUp]
        public void Setup()
        {
            _assertHelper = new AssertHelper<Clinic>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _clinicService = new ClinicService(_unitOfWork.Object);
        }

        [TestCase(-1)]
        [TestCase(-2)]
        [TestCase(0)]
        public void GetById_InvalidClinic_Fail(int id)
        {
            // arrange
            var response = new Response<Clinic>()
            {
                IsSucceed = false,
                ErrorMessage = "Invalid clinic id",
                Result = null
            };

            // act
            var result = _clinicService.GetById(id);

            // assert
            _assertHelper.Assertion(response, result);
        }

        [TestCase(2)]
        [TestCase(4)]
        [TestCase(5)]
        public void GetById_ClinicNotFound_Fail(int id)
        {
            // arrange
            var response = new Response<Clinic>()
            {
                IsSucceed = false,
                ErrorMessage = "Clinic could not found",
                Result = null
            };
            _unitOfWork.Setup(x => x.ClinicRepository.GetById(It.IsAny<int>())).Returns((Clinic)null);

            // act
            var result = _clinicService.GetById(id);

            // assert
            _assertHelper.Assertion(response, result);
        }

        [TestCase(5)]
        [TestCase(3)]
        [TestCase(2)]
        public void GetById_ValidRequest_Success(int id)
        {
            // arrange
            var date = new DateTime(2017, 1, 18);
            var clinic = new Clinic()
            {
                Id = 1,
                Name = "Test Clinic",
                IsActive = true,
                CreatedDate = date,
                Equipments = null
            };
            var response = new Response<Clinic>()
            {
                IsSucceed = true,
                ErrorMessage = null,
                Result = clinic
            };
            _unitOfWork.Setup(x => x.ClinicRepository.GetById(It.IsAny<int>())).Returns(clinic);

            // act
            var result = _clinicService.GetById(id);

            // assert
            _assertHelper.Assertion(response, result);
        }

        [Test]
        public void GetAll_ValidRequest_Success()
        {
            // arrange
            var clinic = new Clinic()
            {
                Id = 1,
                Name = "Test Clinic",
                IsActive = true,
                Equipments = new List<Equipment>()
                {
                    new Equipment()
                    {
                        Id = 1,
                        Name = "Test Equipment",
                        IsActive = true,
                        ClinicId = 1
                    }
                }
            };
            var clinicList = (IEnumerable<Clinic>)new List<Clinic> { clinic };
            var response = new Response<IEnumerable<Clinic>>
            {
                IsSucceed = true,
                Result = clinicList,
                ErrorMessage = null
            };

            _unitOfWork.Setup(x => x.ClinicRepository.GetAll()).Returns(clinicList);

            // act
            var result = _clinicService.GetAll();

            // assert
            Assert.AreEqual(response.Result.Count(), result.Result.Count());
        }

        [Test]
        public void Add_NameIsMandatory_Fail()
        {
            // arrange
            var clinic = new Clinic();
            var response = new Response<Clinic>()
            {
                ErrorMessage = "Name is mandatory"
            };

            // act
            var result = _clinicService.Add(clinic);

            // assert
            _assertHelper.Assertion(response, result);
        }

        [Test]
        public void Add_ValidRequest_Success()
        {
            // arrange
            var date = new DateTime(2017, 1, 18);
            var clinic = new Clinic()
            {
                Name = "Test Clinic",
                CreatedDate = date
            };

            var response = new Response<Clinic>()
            {
                IsSucceed = true,
                ErrorMessage = null,
                Result = clinic
            };

            _unitOfWork.Setup(x => x.ClinicRepository.Add(It.IsAny<Clinic>()));

            // act
            var result = _clinicService.Add(clinic);

            // assert
            _assertHelper.Assertion(response, result);
        }

        [Test]
        public void Update_ClinicCouldNotFound_Fail()
        {
            // arrange
            var clinic = new Clinic()
            {
                Name = "Test Clinic"
            };

            var response = new Response<Clinic>()
            {
                IsSucceed = false,
                ErrorMessage = "Clinic could not found",
                Result = null
            };

            _unitOfWork.Setup(x => x.ClinicRepository.GetById(It.IsAny<int>())).Returns((Clinic)null);

            // act
            var result = _clinicService.Update(clinic);

            // assert
            _assertHelper.Assertion(response, result);
        }

        [Test]
        public void Update_ValidRequest_Success()
        {
            // arrange
            var date = new DateTime(2017, 1, 18);
            var clinic = new Clinic()
            {
                Name = "Test Clinic",
                UpdateDate = date,
            };

            var response = new Response<Clinic>()
            {
                IsSucceed = true,
                ErrorMessage = null,
                Result = clinic
            };

            _unitOfWork.Setup(x => x.ClinicRepository.GetById(It.IsAny<int>())).Returns(clinic);
            _unitOfWork.Setup(x => x.ClinicRepository.Update(It.IsAny<Clinic>()));

            // act
            var result = _clinicService.Update(clinic);

            // assert
            _assertHelper.Assertion(response, result);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void Remove_ClinicCouldNotFound_Fail(int id)
        {
            // arrange
            var response = new Response<bool>()
            {
                IsSucceed = false,
                ErrorMessage = "Clinic could not found",
                Result = false
            };

            _unitOfWork.Setup(x => x.ClinicRepository.GetById(It.IsAny<int>())).Returns((Clinic)null);

            // act
            var result = _clinicService.Remove(id);

            // assert
            Assert.IsFalse(result.IsSucceed);
            Assert.IsFalse(result.Result);
            Assert.AreEqual(response.ErrorMessage, result.ErrorMessage);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void Remove_ValidRequest_Success(int id)
        {
            // arrange
            var date = new DateTime(2017, 1, 18);
            var clinic = new Clinic()
            {
                Id = 1,
                Name = "Test Clinic"
            };

            var response = new Response<bool>()
            {
                IsSucceed = true,
                ErrorMessage = null,
                Result = true
            };

            _unitOfWork.Setup(x => x.ClinicRepository.GetById(It.IsAny<int>())).Returns(clinic);
            _unitOfWork.Setup(x => x.EquipmentRepository.UpdateRange(It.IsAny<IEnumerable<Equipment>>()));
            _unitOfWork.Setup(x => x.ClinicRepository.Update(It.IsAny<Clinic>()));

            // act
            var result = _clinicService.Remove(id);

            // assert
            Assert.IsTrue(result.IsSucceed);
            Assert.IsTrue(result.Result);
            Assert.IsNull(result.ErrorMessage);
        }
    }
}