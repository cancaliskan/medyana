using System;
using System.Collections.Generic;

using AutoMapper;
using Moq;
using NUnit.Framework;

using Medyana.Application.Controllers;
using Medyana.Application.Models;
using Medyana.Business.Contracts;
using Medyana.Common.Contracts;
using Medyana.Domain.Entities;

namespace Medyana.Tests.Controllers
{
    [TestFixture]
    public class EquipmentsControllerTests
    {
        private Mock<IEquipmentService> _equipmentService;
        private Mock<IMapper> _mapper;
        private EquipmentsController _equipmentsController;

        [SetUp]
        public void Setup()
        {
            _equipmentService = new Mock<IEquipmentService>();
            _mapper = new Mock<IMapper>();
            _equipmentsController = new EquipmentsController(_mapper.Object, _equipmentService.Object);
        }

        [Test]
        public void Get_AllEquipments_Success()
        {
            // arrange
            var equipment = new Equipment()
            {
                Id = 1,
                Name = "Test Equipment",
                IsActive = true,
                ClinicId = 1
            };
            var equipmentList = (IEnumerable<Equipment>)new List<Equipment> { equipment };
            var response = new Response<IEnumerable<Equipment>>
            {
                IsSucceed = true,
                Result = equipmentList,
                ErrorMessage = null
            };

            _equipmentService.Setup(x => x.GetAll()).Returns(response);

            // act
            var clinics = _equipmentsController.Get();

            // assert
            Assert.AreEqual(response, clinics);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void Get_EquipmentById_Success(int id)
        {
            // arrange
            var equipment = new Equipment()
            {
                Id = 1,
                Name = "Test Equipment",
                IsActive = true,
                ClinicId = 1
            };
            var response = new Response<Equipment>
            {
                IsSucceed = true,
                Result = equipment,
                ErrorMessage = null
            };
            _equipmentService.Setup(x => x.GetById(It.IsAny<int>())).Returns(response);

            // act
            var result = _equipmentsController.Get(id);

            // assert
            Assert.AreEqual(response, result);
        }

        [Test]
        public void Get_Post_Success()
        {
            // arrange
            var date = new DateTime(2017, 1, 18);
            var equipment = new Equipment()
            {
                Id = 1,
                Name = "Test Equipment",
                ClinicId = 1,
                Quantity = 3,
                ProvideDate = date,
                UnitPrice = 33,
                UsageRate = 99,
                IsActive = true,
                CreatedDate = date,
            };

            var equipmentModel = new EquipmentModel()
            {
                Id = 1,
                Name = "Test Equipment",
                ClinicId = 1,
                Quantity = 3,
                ProvideDate = date,
                UnitPrice = 33,
                UsageRate = 99,
            };

            var response = new Response<Equipment>()
            {
                IsSucceed = true,
                ErrorMessage = null,
                Result = equipment
            };

            _equipmentService.Setup(x => x.Add(It.IsAny<Equipment>())).Returns(response);

            // act
            var result = _equipmentsController.Post(equipmentModel);

            // assert
            Assert.AreEqual(response, result);
        }

        [Test]
        public void Get_Update_Success()
        {
            // arrange
            var date = new DateTime(2017, 1, 18);
            var equipment = new Equipment()
            {
                Id = 1,
                Name = "Test Equipment",
                IsActive = true,
                CreatedDate = date,
            };

            var equipmentModel = new EquipmentModel()
            {
                Id = 1,
                Name = "Test Equipment"
            };

            var response = new Response<Equipment>()
            {
                IsSucceed = true,
                ErrorMessage = null,
                Result = equipment
            };

            _equipmentService.Setup(x => x.Update(It.IsAny<Equipment>())).Returns(response);

            // act
            var result = _equipmentsController.Update(equipmentModel);

            // assert
            Assert.AreEqual(response, result);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void Get_Delete_Success(int id)
        {
            // arrange
            var response = new Response<bool>()
            {
                IsSucceed = true,
                ErrorMessage = null,
                Result = true
            };
            _equipmentService.Setup(x => x.Remove(It.IsAny<int>())).Returns(response);

            // act
            var result = _equipmentsController.Delete(id);

            // assert
            Assert.AreEqual(response, result);
        }
    }
}