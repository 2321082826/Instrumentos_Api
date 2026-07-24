using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Api_Instrumentos.Data;
using Api_Instrumentos.Models;

namespace Api_Instrumentos.Controllers
{
    public class InstrumentosApiController : ApiController
    {
        InstrumentoADO instrumentoADO = new InstrumentoADO(); //crear una instancia (o un "objeto") de la clase InstrumentoADO

        // GET api/<controller>
        public IEnumerable<Instrumento> Get() //método GET que devuelve una lista de instrumentos
        {
            return instrumentoADO.GetAll(); //llamo al método GetAll de la clase InstrumentoADO y devuelvo la lista de instrumentos
        }

        // GET api/<controller>/5
        public Instrumento Get(int id)
        {
            return instrumentoADO.GetById(id); //llamo al método GetById de la clase InstrumentoADO y dev
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}