using System;
using System.Collections.Generic;
using System.Linq;

using Medyana.Business.Contracts;
using Medyana.Common.Contracts;
using Medyana.Common.Helpers.Shared;
using Medyana.DataAccess.UnitOfWork;
using Medyana.Domain.Entities;

namespace Medyana.Business.Services
{
    public class ClinicService : IClinicService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClinicService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Response<Clinic> GetById(int id)
        {
            var response = new Response<Clinic>();

            try
            {
                if (id <= 0)
                {
                    response.IsSucceed = false;
                    response.ErrorMessage = "Invalid clinic id";
                    return response;
                }

                var clinic = _unitOfWork.ClinicRepository.GetById(id);
                if (clinic == null)
                {
                    response.IsSucceed = false;
                    response.ErrorMessage = "Clinic could not found";
                    return response;
                }

                if (clinic.IsActive)
                {
                    response.IsSucceed = true;
                    response.Result = clinic;
                }
            }
            catch (Exception e)
            {
                response.IsSucceed = false;
                response.ErrorMessage = e.Message;
            }

            return response;
        }

        public Response<IEnumerable<Clinic>> GetAll()
        {
            var response = new Response<IEnumerable<Clinic>>();

            try
            {
                var clinics = _unitOfWork.ClinicRepository.GetAll();
                response.IsSucceed = true;
                response.Result = clinics;
            }
            catch (Exception e)
            {
                response.IsSucceed = false;
                response.ErrorMessage = e.Message;
            }

            return response;
        }

        public Response<Clinic> Add(Clinic entity)
        {
            var response = new Response<Clinic>();

            try
            {
                if (entity.Name.IsEmpty())
                {
                    response.IsSucceed = false;
                    response.ErrorMessage = "Name is mandatory";
                    return response;
                }

                entity.CreatedDate = DateTime.Now;
                entity.IsActive = true;

                _unitOfWork.ClinicRepository.Add(entity);
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

        public Response<Clinic> Update(Clinic entity)
        {
            var response = new Response<Clinic>();

            try
            {
                var existClinic = _unitOfWork.ClinicRepository.GetById(entity.Id);
                if (existClinic == null)
                {
                    response.IsSucceed = false;
                    response.ErrorMessage = "Clinic could not found";
                    return response;
                }

                existClinic.UpdateDate = DateTime.Now;
                existClinic.Name = entity.Name;

                _unitOfWork.ClinicRepository.Update(existClinic);
                _unitOfWork.Complete();
                _unitOfWork.Dispose();

                response.IsSucceed = true;
                response.Result = existClinic;
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
                var existClinic = _unitOfWork.ClinicRepository.GetById(id);
                if (existClinic == null)
                {
                    response.IsSucceed = false;
                    response.ErrorMessage = "Clinic could not found";
                    return response;
                }

                existClinic.DeletedDate = DateTime.Now;
                existClinic.IsActive = false;

                var equipmentList = new List<Equipment>();
                foreach (var equipment in existClinic.Equipments)
                {
                    equipment.IsActive = false;
                    equipment.DeletedDate=DateTime.Now;
                    equipmentList.Add(equipment);
                }

                _unitOfWork.EquipmentRepository.UpdateRange(equipmentList);
                _unitOfWork.ClinicRepository.Update(existClinic);
                _unitOfWork.Complete();
                _unitOfWork.Dispose();

                response.Result = true;
                response.IsSucceed = true;
            }
            catch (Exception e)
            {
                response.IsSucceed = false;
                response.ErrorMessage = e.Message;
            }

            return response;
        }
    }
}