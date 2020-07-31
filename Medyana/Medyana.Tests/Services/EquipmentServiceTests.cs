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
    public class EquipmentServiceTests
    {
        private Mock<IUnitOfWork> _unitOfWork;
        private EquipmentService _equipmentService;
        private AssertHelper<Equipment> _assertHelper;

        [SetUp]
        public void Setup()
        {
            _assertHelper = new AssertHelper<Equipment>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _equipmentService = new EquipmentService(_unitOfWork.Object);
        }

        [TestCase(2)]
        [TestCase(4)]
        [TestCase(5)]
        public void GetById_EquipmentNotFound_Fail(int id)
        {
            // arrange
            var response = new Response<Equipment>()
            {
                IsSucceed = false,
                ErrorMessage = "Equipment could not found",
                Result = null
            };
            _unitOfWork.Setup(x => x.EquipmentRepository.GetById(It.IsAny<int>())).Returns((Equipment)null);

            // act
            var result = _equipmentService.GetById(id);

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
            var equipment = new Equipment()
            {
                Id = 1,
                Name = "Test Equipment",
                IsActive = true,
                CreatedDate = date
            };
            var response = new Response<Equipment>()
            {
                IsSucceed = true,
                ErrorMessage = null,
                Result = equipment
            };
            _unitOfWork.Setup(x => x.EquipmentRepository.GetById(It.IsAny<int>())).Returns(equipment);

            // act
            var result = _equipmentService.GetById(id);

            // assert
            _assertHelper.Assertion(response, result);
        }

        [Test]
        public void GetAll_ValidRequest_Success()
        {
            // arrange
            var equipment = new Equipment()
            {
                Id = 1,
                Name = "Test Equipment",
                IsActive = true,
                ClinicId = 1
            };
            var equipments = (IEnumerable<Equipment>)new List<Equipment> { equipment };
            var response = new Response<IEnumerable<Equipment>>
            {
                IsSucceed = true,
                Result = equipments,
                ErrorMessage = null
            };

            _unitOfWork.Setup(x => x.EquipmentRepository.GetAll()).Returns(equipments);

            // act
            var result = _equipmentService.GetAll();

            // assert
            Assert.AreEqual(response.Result.Count(), result.Result.Count());
        }

        [Test]
        public void Add_NameIsMandatory_Fail()
        {
            // arrange
            var equipment = new Equipment();
            var response = new Response<Equipment>()
            {
                ErrorMessage = "Name is mandatory"
            };

            // act
            var result = _equipmentService.Add(equipment);

            // assert
            _assertHelper.Assertion(response, result);
        }

        [Test]
        public void Add_InvalidQuantity_Fail()
        {
            // arrange
            var equipment = new Equipment()
            {
                Name = "Name",
                Quantity = -1
            };

            var response = new Response<Equipment>()
            {
                ErrorMessage = "Quantity Must be grater than zero"
            };

            // act
            var result = _equipmentService.Add(equipment);

            // assert
            _assertHelper.Assertion(response, result);
        }

        [Test]
        public void Add_InvalidUnitPrice_Fail()
        {
            // arrange
            var equipment = new Equipment()
            {
                Name = "Name",
                Quantity = 4,
                UnitPrice = -1
            };

            var response = new Response<Equipment>()
            {
                ErrorMessage = "Unit Price must be grater than 0.01"
            };

            // act
            var result = _equipmentService.Add(equipment);

            // assert
            _assertHelper.Assertion(response, result);
        }

        [Test]
        public void Add_ValidRequest_Success()
        {
            // arrange
            var equipment = new Equipment()
            {
                Name = "Name",
                Quantity = 4,
                UnitPrice = 5,
                UsageRate = 55
            };

            var response = new Response<Equipment>()
            {
                IsSucceed = true,
                ErrorMessage = null,
                Result = equipment
            };

            var clinic = new Clinic()
            {
                Name = "Test Clinic",
            };

            _unitOfWork.Setup(x => x.ClinicRepository.GetById(It.IsAny<int>())).Returns(clinic);
            _unitOfWork.Setup(x => x.EquipmentRepository.Add(It.IsAny<Equipment>()));

            // act
            var result = _equipmentService.Add(equipment);

            // assert
            _assertHelper.Assertion(response, result);
        }

        [Test]
        public void EquipmentCouldNotFound_Fail()
        {
            // arrange
            var equipment = new Equipment()
            {
                Name = "Name",
                Quantity = 4,
                UnitPrice = 5,
                UsageRate = 55
            };

            var response = new Response<Equipment>()
            {
                IsSucceed = false,
                ErrorMessage = "Equipment could not found",
                Result = null
            };

            _unitOfWork.Setup(x => x.EquipmentRepository.GetById(It.IsAny<int>())).Returns((Equipment)null);

            // act
            var result = _equipmentService.Update(equipment);

            // assert
            _assertHelper.Assertion(response, result);
        }

        [Test]
        public void Update_ValidRequest_Success()
        {
            // arrange
            var date = new DateTime(2017, 1, 18);
            var equipment = new Equipment()
            {
                Name = "Name",
                Quantity = 4,
                UnitPrice = 5,
                UsageRate = 55
            };

            var response = new Response<Equipment>()
            {
                IsSucceed = true,
                ErrorMessage = null,
                Result = equipment
            };

            _unitOfWork.Setup(x => x.EquipmentRepository.GetById(It.IsAny<int>())).Returns(equipment);
            _unitOfWork.Setup(x => x.EquipmentRepository.Update(It.IsAny<Equipment>()));

            // act
            var result = _equipmentService.Update(equipment);

            // assert
            _assertHelper.Assertion(response, result);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void EquipmentCouldNotFound_Fail(int id)
        {
            // arrange
            var response = new Response<bool>()
            {
                IsSucceed = false,
                ErrorMessage = "Equipment could not found",
                Result = false
            };

            _unitOfWork.Setup(x => x.EquipmentRepository.GetById(It.IsAny<int>())).Returns((Equipment)null);

            // act
            var result = _equipmentService.Remove(id);

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
            var equipment = new Equipment()
            {
                Id = 1,
                Name = "Test Equipment"
            };

            var response = new Response<bool>()
            {
                IsSucceed = true,
                ErrorMessage = null,
                Result = true
            };

            _unitOfWork.Setup(x => x.EquipmentRepository.GetById(It.IsAny<int>())).Returns(equipment);
            _unitOfWork.Setup(x => x.EquipmentRepository.Update(It.IsAny<Equipment>()));

            // act
            var result = _equipmentService.Remove(id);

            // assert
            Assert.IsTrue(result.IsSucceed);
            Assert.IsTrue(result.Result);
            Assert.IsNull(result.ErrorMessage);
        }
    }
}