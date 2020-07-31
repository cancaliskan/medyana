using System;
using System.Collections.Generic;

using Medyana.Business.Contracts;
using Medyana.Common.Contracts;
using Medyana.Common.Helpers.Shared;
using Medyana.DataAccess.UnitOfWork;
using Medyana.Domain.Entities;

namespace Medyana.Business.Services
{
    public class EquipmentService : IEquipmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EquipmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Response<Equipment> GetById(int id)
        {
            var response = new Response<Equipment>();

            try
            {
                if (id <= 0)
                {
                    response.IsSucceed = false;
                    response.ErrorMessage = "Invalid equipment id";
                }

                var equipment = _unitOfWork.EquipmentRepository.GetById(id);
                if (equipment == null)
                {
                    response.IsSucceed = false;
                    response.ErrorMessage = "Equipment could not found";
                    return response;
                }

                if (equipment.IsActive)
                {
                    response.IsSucceed = true;
                    response.Result = equipment;
                }

            }
            catch (Exception e)
            {
                response.IsSucceed = false;
                response.ErrorMessage = e.Message;
            }

            return response;
        }

        public Response<IEnumerable<Equipment>> GetAll()
        {
            var response = new Response<IEnumerable<Equipment>>();

            try
            {
                var equipments = _unitOfWork.EquipmentRepository.GetAll();
                response.IsSucceed = true;
                response.Result = equipments;
            }
            catch (Exception e)
            {
                response.IsSucceed = false;
                response.ErrorMessage = e.Message;
            }

            return response;
        }

        public Response<Equipment> Add(Equipment entity)
        {
            var response = new Response<Equipment>();

            try
            {
                response = EquipmentValidation(entity, response);
                if (!response.IsSucceed)
                {
                    return response;
                }

                var clinic = _unitOfWork.ClinicRepository.GetById(entity.ClinicId);
                if (clinic == null)
                {
                    response.IsSucceed = false;
                    response.ErrorMessage = "Clinic could not found";
                    return response;
                }

                entity.CreatedDate = DateTime.Now;
                entity.IsActive = true;

                _unitOfWork.EquipmentRepository.Add(entity);
                _unitOfWork.Complete();

                response.IsSucceed = true;
                response.Result = entity;
            }
            catch (Exception e)
            {
                response.IsSucceed = false;
                response.ErrorMessage = e.Message;
            }

            return response;
        }

        public Response<Equipment> Update(Equipment entity)
        {
            var response = new Response<Equipment>();

            try
            {
                var existEquipment = _unitOfWork.EquipmentRepository.GetById(entity.Id);
                if (existEquipment == null)
                {
                    response.IsSucceed = false;
                    response.ErrorMessage = "Equipment could not found";
                    return response;
                }

                response = EquipmentValidation(entity, response);
                if (response.IsSucceed)
                {
                    existEquipment.UpdateDate = DateTime.Now;
                    existEquipment.IsActive = true;

                    _unitOfWork.EquipmentRepository.Update(existEquipment);
                    _unitOfWork.Complete();

                    response.IsSucceed = true;
                    response.Result = existEquipment;
                }
            }
            catch (Exception e)
            {
                response.IsSucceed = false;
                response.ErrorMessage = e.Message;
            }

            return response;
        }

        public Response<bool> Remove(int id)
        {
            var response = new Response<bool>();

            try
            {
                var existEquipment = _unitOfWork.EquipmentRepository.GetById(id);
                if (existEquipment == null)
                {
                    response.IsSucceed = false;
                    response.ErrorMessage = "Equipment could not found";
                    return response;
                }

                existEquipment.DeletedDate = DateTime.Now;
                existEquipment.IsActive = false;

                _unitOfWork.EquipmentRepository.Update(existEquipment);
                _unitOfWork.Complete();

                response.IsSucceed = true;
                response.Result = true;
            }
            catch (Exception e)
            {
                response.IsSucceed = false;
                response.ErrorMessage = e.Message;
            }

            return response;
        }

        private static Response<Equipment> EquipmentValidation(Equipment entity, Response<Equipment> response)
        {
            if (entity.Name.IsEmpty())
            {
                response.IsSucceed = false;
                response.ErrorMessage = "Name is mandatory";
                return response;
            }
            else if (entity.Quantity <= 0)
            {
                response.IsSucceed = false;
                response.ErrorMessage = "Quantity Must be grater than zero";
                return response;
            }
            else if (entity.UnitPrice <= Convert.ToDecimal(0.01))
            {
                response.IsSucceed = false;
                response.ErrorMessage = "Unit Price must be grater than 0.01";
                return response;
            }
            else if (entity.UsageRate < Convert.ToDecimal(0.00) && entity.UsageRate > Convert.ToDecimal(100))
            {
                response.IsSucceed = false;
                response.ErrorMessage = "Usage Rate must be between 0 and 100";
                return response;
            }

            response.IsSucceed = true;
            return response;
        }
    }
}