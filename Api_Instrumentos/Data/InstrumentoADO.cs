using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Npgsql;
using Api_Instrumentos.Models;

namespace Api_Instrumentos.Data
{
    public class InstrumentoADO
    {
        string connectionString = ConfigurationManager.ConnectionStrings["InstrumentosConexion"].ConnectionString;

        public List<Instrumento> GetAll() //Declaro un método público llamado GetAll, que no recibe ningún parámetro y que me devolverá una lista cargada con objetos Instrumento
        {
            List<Instrumento> lista = new List<Instrumento>();
            using (NpgsqlConnection conexion = new NpgsqlConnection(connectionString))
            {
                string query = "select i.id_instrumento, i.nombre AS instrumento, c.nombre AS categoria,m.nombre as marca, p.nombre as proveedor,i.precio_compra,i.precio_venta,i.stock,i.stock_minimo,i.descripcion,i.fecha_registro FROM instrumentos i INNER JOIN categorias c ON i.id_categoria = c.id_categoria INNER JOIN marcas m ON i.id_marca = m.id_marca INNER JOIN proveedores p ON i.id_proveedor = p.id_proveedor;";
                NpgsqlCommand cmd = new NpgsqlCommand(query, conexion);
                conexion.Open();
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Instrumento()
                    {

                        Id = (int)reader["id_instrumento"],
                        Nombre = reader["instrumento"].ToString(),
                        Categoria = reader["categoria"].ToString(),
                        Marca = reader["marca"].ToString(),
                        Provedor = reader["proveedor"].ToString(),
                        precio_compra = (double)reader["precio_compra"],
                        precio_venta = (double)reader["precio_venta"],
                        stock = reader["stock"] == DBNull.Value ? 0 : Convert.ToInt32(reader["stock"]),
                        stock_minimo = reader["stock_minimo"] == DBNull.Value ? 0 : Convert.ToInt32(reader["stock_minimo"]),
                        descripcion = reader["descripcion"] == DBNull.Value ? "" : reader["descripcion"].ToString(),
                        FechaCreacion = reader["fecha_registro"] == DBNull.Value ? DateTime.Now : (DateTime)reader["fecha_registro"]
                    });
                }
            }
            return lista;
        }

        public Instrumento GetById(int id)
        {
            Instrumento buscado = null;
            using (NpgsqlConnection conexion = new NpgsqlConnection(connectionString))
            {
                string query = "select i.id_instrumento, i.nombre AS instrumento, c.nombre AS categoria,m.nombre as marca, p.nombre as proveedor,i.precio_compra,i.precio_venta,i.stock,i.stock_minimo,i.descripcion,i.fecha_registro FROM instrumentos i INNER JOIN categorias c ON i.id_categoria = c.id_categoria INNER JOIN marcas m ON i.id_marca = m.id_marca INNER JOIN proveedores p ON i.id_proveedor = p.id_proveedor WHERE i.id_instrumento = @id;";
                NpgsqlCommand cmd = new NpgsqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@id", id);
                conexion.Open();
                NpgsqlDataReader reader = cmd.ExecuteReader(); // Se ejecuta la consulta y se obtiene un objeto NpgsqlDataReader que permite leer los resultados de la consulta
                if (reader.Read()) // Si se encuentra un registro con el id proporcionado, se crea un objeto Instrumento con los datos obtenidos de la base de datos
                {
                    buscado = new Instrumento() // Se crea un nuevo objeto Instrumento y se asignan los valores obtenidos de la base de datos a sus propiedades
                    {
                        Id = (int)reader["id_instrumento"],
                        Nombre = reader["instrumento"].ToString(),
                        Categoria = reader["categoria"].ToString(),
                        Marca = reader["marca"].ToString(),
                        Provedor = reader["proveedor"].ToString(),
                        precio_compra = (double)reader["precio_compra"],
                        precio_venta = (double)reader["precio_venta"],
                        stock = reader["stock"] == DBNull.Value ? 0 : Convert.ToInt32(reader["stock"]),
                        stock_minimo = reader["stock_minimo"] == DBNull.Value ? 0 : Convert.ToInt32(reader["stock_minimo"]),
                        descripcion = reader["descripcion"] == DBNull.Value ? "" : reader["descripcion"].ToString(),
                        FechaCreacion = reader["fecha_registro"] == DBNull.Value ? DateTime.Now : (DateTime)reader["fecha_registro"]
                    };
                }
            }
            return buscado; // Se devuelve el objeto Instrumento encontrado o null si no se encontró ningún registro con el id proporcionado
        }

        // INSERT
        public void Insert(Instrumento i)
        {
            using (NpgsqlConnection conexion = new NpgsqlConnection(connectionString)) 
            {
                string query =
                "INSERT INTO instrumentos(nombre, id_categoria, id_marca, id_proveedor, precio_compra, precio_venta, stock, stock_minimo, descripcion) VALUES(@nombre, @id_categoria, @id_marca, @id_proveedor, @precio_compra, @precio_venta, @stock, @stock_minimo, @descripcion)";
                NpgsqlCommand cmd = new NpgsqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@nombre", i.Nombre);
                cmd.Parameters.AddWithValue("@id_categoria", i.Categoria);
                cmd.Parameters.AddWithValue("@id_marca", i.Marca);
                cmd.Parameters.AddWithValue("@id_proveedor", i.Provedor);
                cmd.Parameters.AddWithValue("@precio_compra", i.precio_compra);
                cmd.Parameters.AddWithValue("@precio_venta", i.precio_venta);
                cmd.Parameters.AddWithValue("@stock", i.stock);
                cmd.Parameters.AddWithValue("@stock_minimo", i.stock_minimo);
                cmd.Parameters.AddWithValue("@descripcion", i.descripcion);
                conexion.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}