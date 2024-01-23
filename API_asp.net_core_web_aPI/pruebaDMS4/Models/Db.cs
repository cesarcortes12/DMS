using pruebaDMS4.Models;
using Microsoft.Extensions.Caching;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;



namespace pruebaDMS4.Models
{
    public class Db
    {
        private readonly string? _connectionString;
        public Db(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("defaultConnection");
        }
        //SqlConnection con = new SqlConnection("Data source=DESKTOP-F97KFH5;Initial Catalog=pruebaDMS4;Integrated Security=True");


        //Mmetodo para traer todos los clientes
        public async Task<List<Cliente>> GetAll()
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_mostrarTodo", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    var response = new List<Cliente>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToValue(reader));
                        }
                    }

                    return response;
                }
            }
        }


        //metodo mapeo
        private Cliente MapToValue(SqlDataReader reader)
        {
            return new Cliente()
            {
                IdCliente = (int)reader["IdCliente"],
                NombreCliente = reader["NombreCliente"].ToString(),
                ApellidoCliente = reader["ApellidoCliente"].ToString(),
                Telefono = reader["Telefono"].ToString()
            };
        }

        //METODO TRAER CLIENTE POR ID
        public async Task<Cliente> GetById(int IdCliente)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_mostrarCliente", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@IdCliente", IdCliente));
                    Cliente response = null;
                    await sql.OpenAsync();

                    using(var reader = await cmd.ExecuteReaderAsync())
                    { 
                        while (await reader.ReadAsync()) 
                            {
                                response = MapToValue(reader);  
                            } 
                    }
                    return response;
                }
            }
        }


        //METODO PARA INSERTAR 5 NUEVOS CLIENTES DINAMICAMENTE
        
        public async Task InsertarClientesDina(List<Cliente> clientes)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_insertarclientesDinami",sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    //estoy creando una tabla temporal en memoria
                    var table = new DataTable();
                    table.Columns.Add("NombreCliente", typeof(string));
                    table.Columns.Add("ApellidoCliente", typeof(string));
                    table.Columns.Add("Telefono", typeof(string));

                foreach(var cliente in clientes)
                    {
                        table.Rows.Add(cliente.NombreCliente, cliente.ApellidoCliente, cliente.Telefono);
                    }

                    var parameter = new SqlParameter("@Clientes", SqlDbType.Structured)
                    {
                        TypeName = "dbo.ClientesType",
                        Value = table
                        
                    };
                    cmd.Parameters.Add(parameter);
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                }
            }
        }




        //METODO CREAR CLIENTE

        public async Task Insert (Cliente cli)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_insertarCliente", sql))
                {
                    cmd.CommandType= CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@NombreCliente", cli.NombreCliente));
                    cmd.Parameters.Add(new SqlParameter("ApellidoCliente", cli.ApellidoCliente));
                    cmd.Parameters.Add(new SqlParameter("@Telefono", cli.Telefono));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }


        //  METODO ELIMINAR

        public async Task Delete(int IdCliente)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_eliminarCliente", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@IdCliente", IdCliente));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }

        //METODO EDITAR

        public async Task Update(Cliente cli)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_editarCliente" ,sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@IdCliente", cli.IdCliente));
                    cmd.Parameters.Add(new SqlParameter("@NombreCliente", cli.NombreCliente));
                    cmd.Parameters.Add(new SqlParameter("@ApellidoCliente", cli.ApellidoCliente));
                    cmd.Parameters.Add(new SqlParameter("@Telefono", cli.Telefono));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }





        //INICIA METODOS PRODUCTOS



        //metodo mapeoproductos
        private Productos MapToValueProd(SqlDataReader reader)
        {
            return new Productos()
            {
                IdProducto = (int)reader["IdProducto"],
                NombreProducto = reader["NombreProducto"].ToString(),
                PrecioProducto = (int)reader["PrecioProducto"],
                Cantidad = (int)reader["Cantidad"],
                fkIdCliente = (int)reader["fkIdCliente"]
            };
        }

        //Metodo para traer todos los productos
        public async Task<List<Productos>> GetAllProd()
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[sp_mostrarTodoProductos]", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    var response = new List<Productos>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToValueProd(reader));
                        }
                    }

                    return response;
                }
            }
        }


        //METODO TRAER PRODUCTO POR ID
        public async Task<Productos> GetByIdpROD(int IdProducto)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_mostrarProductobyId", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@IdProducto", IdProducto));
                    Productos response = null;
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response = MapToValueProd(reader);
                        }
                    }
                    return response;
                }
            }
        }


        //METODO CREAR Producto

        public async Task InsertProd(Productos prod)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_insertarProducto", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@NombreProducto", prod.NombreProducto));
                    cmd.Parameters.Add(new SqlParameter("@PrecioProducto", prod.PrecioProducto));
                    cmd.Parameters.Add(new SqlParameter("@Cantidad", prod.Cantidad));
                    cmd.Parameters.Add(new SqlParameter("@fkIdCliente", prod.fkIdCliente));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }




        //METODO EDITAR producto

        public async Task UpdateProd(Productos prod)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_editarProducto", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@IdProducto", prod.IdProducto));
                    cmd.Parameters.Add(new SqlParameter("@NombreProducto", prod.NombreProducto));
                    cmd.Parameters.Add(new SqlParameter("@PrecioProducto", prod.PrecioProducto));
                    cmd.Parameters.Add(new SqlParameter("@Cantidad", prod.Cantidad));
                    cmd.Parameters.Add(new SqlParameter("@fkIdCliente", prod.fkIdCliente));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }


        //METODO ELIMINAR PRODUCTO


        public async Task EliminiarProd(int IdProducto)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_eliminarProducto", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@IdProducto", IdProducto));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }






        /* public void guardarCliente (Cliente cli) 
         {
             string procedimiento = "sp_insertar";
             SqlCommand cmd = new SqlCommand (procedimiento, con);
             con.Open ();
             cmd.CommandType = CommandType.StoredProcedure;
             cmd.Parameters.AddWithValue("@IdCliente", cli.IdCliente);
             cmd.Parameters.AddWithValue("@NombreCliente", cli.NombreCliente);
             cmd.Parameters.AddWithValue("@ApellidoCliente", cli.ApellidoCliente);
             cmd.Parameters.AddWithValue("@Telefono", cli.Telefono);

             cmd.ExecuteNonQuery ();
             con.Close ();
         }   */

    }

}
