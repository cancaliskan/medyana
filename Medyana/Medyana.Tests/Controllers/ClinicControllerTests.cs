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
    public class ClinicControllerTests
    {
        private Mock<IClinicService> _clinicService;
        private Mock<IMapper> _mapper;
        private ClinicsController _clinicsController;

        [SetUp]
        public void Setup()
        {
            _clinicService = new Mock<IClinicService>();
            _mapper = new Mock<IMapper>();
            _clinicsController = new ClinicsController(_mapper.Object, _clinicService.Object);
        }

        [Test]
        public void Get_AllClinics_Success()
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

            _clinicService.Setup(x => x.GetAll()).Returns(response);

            // act
            var clinics = _clinicsController.Get();

            // assert
            Assert.AreEqual(response, clinics);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void Get_ClinicById_Success(int id)
        {
            // arrange
            var clinic = new Clinic()
            {
                Id = id,
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
            var response = new Response<Clinic>
            {
                IsSucceed = true,
                Result = clinic,
                ErrorMessage = null
            };
            _clinicService.Setup(x => x.GetById(It.IsAny<int>())).Returns(response);

            // act
            var result = _clinicsController.Get(id);

            // assert
            Assert.AreEqual(response, result);
        }

        [Test]
        public void Get_Post_Success()
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

            var clinicModel = new ClinicModel()
            {
                Id = 1,
                Name = "Test Clinic"
            };

            var response = new Response<Clinic>()
            {
                IsSucceed = true,
                ErrorMessage = null,
                Result = clinic
            };

            _clinicService.Setup(x => x.Add(It.IsAny<Clinic>())).Returns(response);

            // act
            var result = _clinicsController.Post(clinicModel);

            // assert
            Assert.AreEqual(response, result);
        }

        [Test]
        public void Get_Update_Success()
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

            var clinicModel = new ClinicModel()
            {
                Id = 1,
                Name = "Test Clinic"
            };

            var response = new Response<Clinic>()
            {
                IsSucceed = true,
                ErrorMessage = null,
                Result = clinic
            };

            _clinicService.Setup(x => x.Update(It.IsAny<Clinic>())).Returns(response);

            // act
            var result = _clinicsController.Update(clinicModel);

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
            _clinicService.Setup(x => x.Remove(It.IsAny<int>())).Returns(response);

            // act
            var result = _clinicsController.Delete(id);

            // assert
            Assert.AreEqual(response, result);
        }
    }
}