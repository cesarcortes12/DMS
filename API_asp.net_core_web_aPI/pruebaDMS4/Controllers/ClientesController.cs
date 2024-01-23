using Microsoft.AspNetCore.Mvc;
using pruebaDMS4.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace pruebaDMS4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly Db _repository;

        public ClientesController(Db repository) 
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        // GET: api/<ClientesController>/obtener-todos-clientes
        [HttpGet("obtener-todos-clientes")]
        public async Task<ActionResult<IEnumerable<Cliente>>> Get()
        {
            return await _repository.GetAll();
        }

        // GET api/<ClientesController>/obtenerCliente
        [HttpGet("obtenerCliente{id}")]
        public async Task<ActionResult<Cliente>> GetById(int id)
        {
            var response = await _repository.GetById(id);
            if(response==null) { return NotFound(); };
            return response;
        }

        // POST api/<ClientesController>/crearCliente
        [HttpPost("crearCliente")]
        public async Task Post([FromBody] Cliente cli)
        {
            await _repository.Insert(cli);
        }

        //METODO PARA INSERTAR 5 CLIENTES DE MANERA DINAMICA

        // POST api/<ClientesController>/insertarDinamica
        [HttpPost("insertarDinamica")]
        public async Task insertarDinamica()
        {
            List<Cliente> nuevosClientes = new List<Cliente>();
                for (int i = 1; i <= 5; i++)
                {
                    Cliente nuevoCliente = new Cliente
                    {
                        NombreCliente = $"Cliente{i}",
                        ApellidoCliente = $"Apellido{i}",
                        Telefono = $"123456{i}"
                    };
                nuevosClientes.Add(nuevoCliente);
                    
                }
            await _repository.InsertarClientesDina(nuevosClientes);
        }

        //FIN METODO PARA INSERTAR DINAMICAMENTE


        // PUT api/<ClientesController>/editarCliente
        [HttpPut("editarcliente{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Cliente cli)
        {
            var existCliente = await _repository.GetById(id);
            if (existCliente == null)
            {
                return NotFound();
            }
            existCliente.NombreCliente = cli.NombreCliente;
            existCliente.ApellidoCliente = cli.ApellidoCliente;
            existCliente.Telefono = cli.Telefono;

            await _repository.Update(existCliente);
            return NoContent();
        }

        // DELETE api/<ClientesController>/eliminarCliente
        [HttpDelete("eliminarCliente{id}")]
        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }
    }
}
