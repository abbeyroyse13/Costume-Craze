using CostumeCraze.Auth.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CostumeCraze.Models;
using CostumeCraze.Repositories;

namespace CostumeCraze.Models
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public ProductRepository(IConfiguration config) : base(config) { }

        public List<Product> GetAllProducts()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                         SELECT p.Id AS pId, p.ImageUrl, p.[Name] AS pName, p.[Description], p.Color, p.Price, p.Quantity, p.ProductTypeId, p.UserProfileId,
                                pt.Id AS ptId, pt.[Name] AS ptName,
                                up.Id AS upId, up.FirstName AS upFirstName
                         FROM Product p
                         LEFT JOIN ProductType pt ON p.ProductTypeId = pt.Id
                         LEFT JOIN UserProfile up ON p.UserProfileId = up.Id
                         ";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Product> products = new List<Product>();
                    while (reader.Read())
                    {
                        Product product = new Product
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("pId")),
                            ImageUrl = reader.GetString(reader.GetOrdinal("ImageUrl")),
                            Name = reader.GetString(reader.GetOrdinal("pName")),
                            Description = reader.GetString(reader.GetOrdinal("Description")),
                            Color = reader.GetString(reader.GetOrdinal("Color")),
                            Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                            Quantity = reader.GetInt32(reader.GetOrdinal("Quantity")),
                            ProductTypeId = reader.GetInt32(reader.GetOrdinal("ProductTypeId")),
                            UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                            ProductType = new ProductType()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("ptId")),
                                Name = reader.GetString(reader.GetOrdinal("ptName"))
                            },
                            UserProfile = new UserProfile()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("upId")),
                                FirstName = reader.GetString(reader.GetOrdinal("upFirstName"))
                            }
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
                        SELECT p.Id AS pId, p.ImageUrl, p.[Name] AS pName, p.[Description], p.Color, p.Price, p.Quantity, p.ProductTypeId, p.UserProfileId,
                                pt.Id AS ptId, pt.[Name] AS ptName,
                                up.Id AS upId, up.FirstName AS upFirstName
                         FROM Product p
                         LEFT JOIN ProductType pt ON p.ProductTypeId = pt.Id
                         LEFT JOIN UserProfile up ON p.UserProfileId = up.Id
                         WHERE p.Id = @id
                    ";

                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        Product product = new Product
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("pId")),
                            ImageUrl = reader.GetString(reader.GetOrdinal("ImageUrl")),
                            Name = reader.GetString(reader.GetOrdinal("pName")),
                            Description = reader.GetString(reader.GetOrdinal("Description")),
                            Color = reader.GetString(reader.GetOrdinal("Color")),
                            Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                            Quantity = reader.GetInt32(reader.GetOrdinal("Quantity")),
                            ProductTypeId = reader.GetInt32(reader.GetOrdinal("ProductTypeId")),
                            UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                            ProductType = new ProductType()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("ptId")),
                                Name = reader.GetString(reader.GetOrdinal("ptName"))
                            },
                            UserProfile = new UserProfile()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("upId")),
                                FirstName = reader.GetString(reader.GetOrdinal("upFirstName"))
                            }
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
                            INSERT INTO Product (p.ImageUrl, p.[Name], p.[Description], p.Color, p.Price, p.Quantity, p.ProductTypeId, p.UserProfileId)
                            OUTPUT INSERTED.ID
                            VALUES (@imageUrl, @name, @description, @color, @price, @quantity, @productTypeId, @userProfileId);
                            ";

                    cmd.Parameters.AddWithValue("@imageUrl", product.ImageUrl);
                    cmd.Parameters.AddWithValue("@name", product.Name);
                    cmd.Parameters.AddWithValue("@description", product.Description);
                    cmd.Parameters.AddWithValue("@color", product.Color);
                    cmd.Parameters.AddWithValue("@price", product.Price);
                    cmd.Parameters.AddWithValue("@quantity", product.Quantity);
                    cmd.Parameters.AddWithValue("@productTypeId", product.ProductTypeId);
                    cmd.Parameters.AddWithValue("@userProfileId", product.UserProfileId);

                    int id = (int)cmd.ExecuteScalar();

                    product.Id = id;
                }
            }
        }

        public void UpdateProduct(Product product)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE Product 
                        SET 
                            ImageUrl = @imageUrl,
                            [Name] = @name,
                            [Description] = @description,
                            Color = @color,
                            Price = @price,
                            Quantity = @quantity,
                            ProductTypeId = @productTypeId,
                            UserProfileId = @userProfileId
                        WHERE Product.Id = @id";

                    cmd.Parameters.AddWithValue("@imageUrl", product.ImageUrl);
                    cmd.Parameters.AddWithValue("@name", product.Name);
                    cmd.Parameters.AddWithValue("@description", product.Description);
                    cmd.Parameters.AddWithValue("@color", product.Color);
                    cmd.Parameters.AddWithValue("@price", product.Price);
                    cmd.Parameters.AddWithValue("@quantity", product.Quantity);
                    cmd.Parameters.AddWithValue("@productTypeId", product.ProductTypeId);
                    cmd.Parameters.AddWithValue("@userProfileId", product.UserProfileId);
                    cmd.Parameters.AddWithValue("@id", product.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteProduct(int productId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        DELETE FROM Product
                        WHERE Id = @id
                    ";

                    cmd.Parameters.AddWithValue("@id", productId);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}