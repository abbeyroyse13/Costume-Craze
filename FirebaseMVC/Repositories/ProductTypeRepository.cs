using CostumeCraze.Auth.Models;
using CostumeCraze.Models;
using CostumeCraze.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CostumeCraze.Models
{
    public class ProductTypeRepository : BaseRepository, IProductTypeRepository 
    {
        public ProductTypeRepository(IConfiguration config) : base(config) { }

        public List<ProductType> GetAllProductTypes()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id, [Name]
                        FROM ProductType
                    ";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<ProductType> productTypes = new List<ProductType>();
                    while (reader.Read())
                    {
                        ProductType productType = new ProductType()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name"))
                        };

                        productTypes.Add(productType);
                    }

                    reader.Close();

                    return productTypes;
                }
            }
        }

        public ProductType GetProductTypeById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id, [Name]
                        FROM ProductType
                        WHERE Id = @id
                    ";

                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        ProductType productType = new ProductType
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name"))
                        };

                        reader.Close();
                        return productType;
                    }
                    else
                    {
                        reader.Close();
                        return null;
                    }
                }
            }
        }

        public void AddProductType(ProductType productType)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO ProductType ([Name])
                        OUTPUT INSERTED.ID
                        VALUES (@name)
                    ";

                    cmd.Parameters.AddWithValue("@name", productType.Name);

                    int id = (int)cmd.ExecuteScalar();

                    productType.Id = id;
                }
            }
        }

        public void UpdateProductType(ProductType productType)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE ProductType
                        SET
                            Name = @name
                        WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@name", productType.Name);
                    cmd.Parameters.AddWithValue("@id", productType.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteProductType(int productTypeId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        DELETE FROM ProductType
                        WHERE Id = @id
                    ";

                    cmd.Parameters.AddWithValue("@id", productTypeId);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
