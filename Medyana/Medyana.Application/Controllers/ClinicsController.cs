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
    public class ClinicsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IClinicService _clinicService;

        public ClinicsController(IMapper mapper, IClinicService clinicService)
        {
            _mapper = mapper;
            _clinicService = clinicService;
        }

        [HttpGet]
        public Response<IEnumerable<Clinic>> Get()
        {
            return _clinicService.GetAll();
        }

        [HttpGet("{id}")]
        public Response<Clinic> Get(int id)
        {
            var response = _clinicService.GetById(id);
            return response;
        }

        [HttpPost]
        public Response<Clinic> Post([FromBody] ClinicModel clinic)
        {
            var model = _mapper.Map<Clinic>(clinic);
            var response = _clinicService.Add(model);
            return response;
        }

        [HttpPut]
        public Response<Clinic> Update([FromBody] ClinicModel clinic)
        {
            var model = _mapper.Map<Clinic>(clinic);
            var response = _clinicService.Update(model);
            return response;
        }

        [HttpDelete("{id}")]
        public Response<bool> Delete(int id)
        {
            var response = _clinicService.Remove(id);
            return response;
        }
    }
}