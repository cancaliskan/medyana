using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using AutoMapper;

using Medyana.Application.Models;
using Medyana.Business.Contracts;
using Medyana.Common.Contracts;
using Medyana.Domain.Entities;

namespace Medyana.Application.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class EquipmentsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IEquipmentService _equipmentService;

        public EquipmentsController(IMapper mapper, IEquipmentService equipmentService)
        {
            _mapper = mapper;
            _equipmentService = equipmentService;
        }

        [HttpGet]
        public Response<IEnumerable<Equipment>> Get()
        {
            return _equipmentService.GetAll();
        }

        [HttpGet("{id}")]
        public Response<Equipment> Get(int id)
        {
            var response = _equipmentService.GetById(id);
            return response;
        }

        [HttpPost]
        public Response<Equipment> Post([FromBody] EquipmentModel equipment)
        {
            var model = _mapper.Map<Equipment>(equipment);
            var response = _equipmentService.Add(model);
            return response;
        }

        [HttpPut()]
        public Response<Equipment> Update([FromBody] EquipmentModel equipment)
        {
            var model = _mapper.Map<Equipment>(equipment);
            var response = _equipmentService.Update(model);
            return response;
        }

        [HttpDelete("{id}")]
        public Response<bool> Delete(int id)
        {
            var response = _equipmentService.Remove(id);
            return response;
        }
    }
}