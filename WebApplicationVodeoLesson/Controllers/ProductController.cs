using Microsoft.AspNetCore.Mvc;
using WebApplicationVodeoLesson.Models;
using System.Data;
using System.Text.Json;
using Org.BouncyCastle.Asn1.Ocsp;

namespace WebApplicationVideoLesson.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProductController : ControllerBase
    {
        //create a new product and add to list
        private static List<Product> products = new List<Product>();
        //{
        // new Product
        // {
        //     Id = 1,
        //    Name = "jacket",
        //    Price = 210
        // },
        // new Product
        // {
        //      Id = 2,
        //    Name = "belt",
        //    Price = 150
        // }
        //};

        [HttpPost] //create a new product and add to list
        public IActionResult CreateProduct(Product productsdto)
        {
            try
            {
                products.Add(productsdto);
                //products.Add(new Product
                //{
                //    Id = productsdto.Id,
                //    Name = productsdto.Name,
                //    Price = productsdto.Price,
                //});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(products);
        }

        [HttpGet] //show all items in list
        public IActionResult EditProduct()
        {
            return Ok(products);
        }

        [HttpDelete] //detete items in list
        public IActionResult DeleteProduct(int id)
        {
            products.RemoveAll(x => x.Id == id);
            return Ok(products);
        }

        [HttpPut("{id}")] //update items in list
        public IActionResult UpdateProduct(int id, ProductDto product)
        {
            //object addOrUpdateValue = products.AddOrUpdate(x => x.Id, product);
            var IdProducts = products.Where(x => x.Id == id).ToList();

            if (IdProducts.Any())
            {
                products[1].Id = id;
                products[1].Name = product.Name;
                products[1].Price = product.Price;
            }
            else
            {
                products.Add(new Product
                {
                    Id = id,
                    Name = product.Name,
                    Price = product.Price
                });
            }
            return Ok(IdProducts);
        }


        //    private readonly string connectionString = "server=localhost;database=apitest;user=root;password=adminroot;";

        //    [HttpPost]
        //    public IActionResult CreateProduct(ProductDto productdto)
        //    {
        //        try
        //        {
        //            using (var connection = new MySqlConnection(connectionString))
        //            {
        //                connection.Open();

        //                string sql = "insert into products (name, price) values (@name, @price)";

        //                using (var command = new MySqlCommand(sql, connection))
        //                {
        //                    command.Parameters.AddWithValue("@name", productdto.Name);
        //                    command.Parameters.AddWithValue("@price", productdto.Price);
        //                    command.ExecuteNonQuery();
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            return BadRequest(ex.Message);
        //        }

        //        return Ok();
        //    }

        //    [HttpGet]
        //    public IActionResult GetProduct()
        //    {
        //        List<Product> products = new List<Product>();
        //        try
        //        {
        //            using (var connection = new MySqlConnection(connectionString))
        //            {
        //                connection.Open();
        //                string sql = "select * from products";
        //                using (var command = new MySqlCommand(sql, connection))
        //                {
        //                    using (var reader = command.ExecuteReader())
        //                    {
        //                        while (reader.Read())
        //                        {
        //                            Product product = new Product();
        //                            product.Id = reader.GetInt32(0);
        //                            product.Name = reader.GetString(1);
        //                            product.Price = reader.GetInt32(2);
        //                            products.Add(product);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            return BadRequest(ex.Message);
        //        }
        //        return Ok(products);
        //    }

        //    [HttpPut("{id}")]
        //    public IActionResult UpdateProduct(int id, ProductDto productDto)
        //    {
        //        try
        //        {
        //            using (var connection = new MySqlConnection(connectionString))
        //            {
        //                connection.Open();
        //                string sql = "update products set name=@name, price=@price where id = @id;";
        //                using (var command = new MySqlCommand(sql, connection))
        //                {
        //                    command.Parameters.AddWithValue("@name", productDto.Name);
        //                    command.Parameters.AddWithValue("@price", productDto.Price);
        //                    command.Parameters.AddWithValue("@id", id);

        //                    command.ExecuteNonQuery();
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            return BadRequest(ex.Message);
        //        }
        //        return Ok();
        //    }

        //    [HttpDelete("{id}")]
        //    public IActionResult DeleteProduct(int id)
        //    {
        //        try
        //        {
        //            using (var connection = new MySqlConnection(connectionString))
        //            {
        //                connection.Open();
        //                string sql = "delete from products where id = @id;";
        //                using (var command = new MySqlCommand(sql, connection))
        //                {
        //                    command.Parameters.AddWithValue("@id", id);
        //                    command.ExecuteNonQuery();
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            return BadRequest(ex.Message);
        //        }
        //        return Ok();
        //    }
    }

}
