using CostumeCraze.Auth.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CostumeCraze.Models
{
    public class ProductRepository : IProductRepository
    {
        private readonly IConfiguration _config;

        public ProductRepository(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public List<Product> GetAllProducts()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                         SELECT Id, ImageUrl, [Name], [Description], Color, Price, Quantity, ProductTypeId, UserProfileId
                         FROM Product
                         ";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Product> products = new List<Product>();
                    while (reader.Read())
                    {
                        Product product = new Product
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            ImageUrl = reader.GetString(reader.GetOrdinal("ImageUrl")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Description = reader.GetString(reader.GetOrdinal("Description")),
                            Color = reader.GetString(reader.GetOrdinal("Color")),
                            Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                            Quantity = reader.GetInt32(reader.GetOrdinal("Quantity")),
                            ProductTypeId = reader.GetInt32(reader.GetOrdinal("ProductTypeId")),
                            UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId"))
                        };

                        products.Add(product);
                    }

                    reader.Close();

                    return products;
                }
            }
        }

        public Product GetProductById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id, ImageUrl, [Name], [Description], Color, Price, Quantity, ProductTypeId, UserProfileId
                        FROM Product
                        WHERE Id = @id
                    ";

                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        Product product = new Product
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            ImageUrl = reader.GetString(reader.GetOrdinal("ImageUrl")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Description = reader.GetString(reader.GetOrdinal("Description")),
                            Color = reader.GetString(reader.GetOrdinal("Color")),
                            Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                            Quantity = reader.GetInt32(reader.GetOrdinal("Quantity")),
                            ProductTypeId = reader.GetInt32(reader.GetOrdinal("ProductTypeId")),
                            UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId"))
                        };

                        reader.Close();
                        return product;
                    }
                    else
                    {
                        reader.Close();
                        return null;
                    }
                }
            }
        }
            public void AddProduct(Product product)
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"
                            INSERT INTO Product (ImageUrl, [Name], [Description], Color, Price, Quantity, ProductTypeId)
                            OUTPUT INSERTED.ID
                            VALUES (@imageUrl, @name, @description, @color, @price, @quantity, @productTypeId);
                            ";

                        cmd.Parameters.AddWithValue("@imageUrl", product.ImageUrl);
                        cmd.Parameters.AddWithValue("@name", product.Name);
                        cmd.Parameters.AddWithValue("@description", product.Description);
                        cmd.Parameters.AddWithValue("@color", product.Color);
                        cmd.Parameters.AddWithValue("@price", product.Price);
                        cmd.Parameters.AddWithValue("@quantity", product.Quantity);
                        cmd.Parameters.AddWithValue("@productTypeId", product.ProductTypeId);

                        int id = (int)cmd.ExecuteScalar();

                        product.Id = id;
                    }
                }
            }
    }
}