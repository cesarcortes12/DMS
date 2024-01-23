using Microsoft.AspNetCore.Mvc;
using pruebaDMS4.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace pruebaDMS4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
  
    {
        private readonly Db _repositoryprod;

        public ProductosController(Db repositoryprod) 
        {
            this._repositoryprod = repositoryprod ?? throw new ArgumentNullException(nameof(repositoryprod));
        }
        // GET: api/<ProductosController>/sp_mostrarTodoProductos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Productos>>> Get()
        {
            return await _repositoryprod.GetAllProd();
        }

        // GET api/<ProductosController>/sp_mostrarProductobyId
        [HttpGet("{id}")]
        public async Task<ActionResult<Productos>> GetById(int id)
        {
            var response = await _repositoryprod.GetByIdpROD(id);
            if (response == null) { return NotFound(); };
            return (response);
        }

        // POST api/<ProductosController>/sp_insertarProducto
        [HttpPost]
        public async Task Post([FromBody] Productos prod)
        {
            await _repositoryprod.InsertProd(prod);
        }

        // PUT api/<ProductosController>/sp_editarProducto
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Productos prod)
        {
            var existProducto = await _repositoryprod.GetByIdpROD(id);
            if (existProducto == null)
            {
                return NotFound();
            }
            existProducto.NombreProducto = prod.NombreProducto;
            existProducto.PrecioProducto = prod.PrecioProducto;
            existProducto.Cantidad = prod.Cantidad;
            existProducto.fkIdCliente = prod.fkIdCliente;

            await _repositoryprod.UpdateProd(existProducto);
            return NoContent();
        }

        // DELETE api/<ProductosController>/sp_eliminarProducto
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _repositoryprod.EliminiarProd(id);
        }
    }
}
