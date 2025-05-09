using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using WebApplicationVodeoLesson.Models;

namespace WebApplicationVodeoLesson.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    //create a new product and add to list
    private static readonly List<Product> Products = [];

    [HttpPost] //create a new product and add to list
    public IActionResult Create(Product product)
    {
        try
        {
            Products.Add(product);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok(product);
    }

    [HttpGet] //show all items in list
    public IActionResult GetAll()
    {
        return Ok(Products);
    }

    [HttpDelete] //delete items in list by id
    public IActionResult Delete(int id)
    {
        var findProduct = Products.SingleOrDefault(x => x.Id == id);
        if (findProduct == null)
        {
            return NotFound($"product with id: {id} does not exist!");
        }

        Products.Remove(findProduct);

        return Ok(findProduct);
    }

    [HttpPut("{id}")] //update items in list
    public IActionResult Update(int id, Product product)
    {
        var lookForProduct = Products.SingleOrDefault(x => x.Id == id);
        if (lookForProduct == null)
        {
            return NotFound($"product with id: {id} does not exist!");
        }

        lookForProduct = product;

        return Ok(lookForProduct);
    }


    private const string ConnectionString = "server=localhost;database=apitest;user=root;password='';";

    [HttpPost]
    public IActionResult CreateProduct(Product product)
    {
        try
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                string sql = "insert into products (name, price) values (@name, @price)";

                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@name", product.Name);
                    command.Parameters.AddWithValue("@price", product.Price);
                    command.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok();
    }

    [HttpGet]
    public IActionResult GetProduct()
    {
        List<Product> products = new List<Product>();
        try
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                string sql = "select * from products";
                using (var command = new MySqlCommand(sql, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Product product = new Product();
                            product.Id = reader.GetInt32(0);
                            product.Name = reader.GetString(1);
                            product.Price = reader.GetInt32(2);
                            products.Add(product);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok(products);
    }

    [HttpPut]
    public IActionResult UpdateProduct(int id, Product product)
    {
        try
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                string sql = "update products set name=@name, price=@price where id = @id;";
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@name", product.Name);
                    command.Parameters.AddWithValue("@price", product.Price);
                    command.Parameters.AddWithValue("@id", id);

                    command.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id)
    {
        try
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                string sql = "delete from products where id = @id;";
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok();
    }
}