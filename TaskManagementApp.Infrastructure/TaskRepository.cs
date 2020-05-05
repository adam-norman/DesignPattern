using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementApp.Application.Interfaces;
using TaskManagmentApp.Core.Entities;
using Task = TaskManagmentApp.Core.Entities.Task;

namespace TaskManagementApp.Infrastructure
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IConfiguration _configuration;

        public TaskRepository(IConfiguration configuration )
        {
            _configuration = configuration;
        }
        public async Task<int> Add(Task entity)
        {
            entity.DateCreated = DateTime.Now;
            var sql = "INSERT INTO [Tasks]([Name],[Description],[Status],[DueDate],[DateCreated],[DateModified]) VALUES (@Name,@Description,@Status,@DueDate,@DateCreated,@DateModified)";
            using (var con=new SqlConnection(_configuration.GetConnectionString("connectionstring")))
            {
                con.Open();
                var affectedRows = await con.ExecuteAsync(sql, entity);
                return affectedRows;
            }
        }
        public async Task<int> Delete(int id)
        {
            var sql = "DELETE FROM [Tasks] WHERE Id=@Id";
            using (var con = new SqlConnection(_configuration.GetConnectionString("connectionstring")))
            {
                con.Open();
                var affectedRows = await con.ExecuteAsync(sql, id);
                return affectedRows;
            }
        }

        public async Task<Task> Get(int id)
        {
            var sql = "SELECT * FROM [Tasks] WHERE Id=@Id";
            using (var con = new SqlConnection(_configuration.GetConnectionString("connectionstring")))
            {
                con.Open();
                var res = await con.QueryAsync<Task>(sql, new { Id = id });
                return res.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<Task>> GetAll()
        {
            var sql = "SELECT * FROM [Tasks]";
            using (var con = new SqlConnection(_configuration.GetConnectionString("connectionstring")))
            {
                con.Open();
                var res = await con.QueryAsync<Task>(sql);
                return res;
            }
        }

        public async Task<int> Update(Task entity)
        {
            entity.DateCreated = DateTime.Now;
            var sql = "UPDATE [Tasks]" +
                "SET [Name]=@Name,[Description]=@Description,[Status]=@Status,[DueDate]=@DueDate,[DateCreated]=@DateCreated,[DateModified]=@DateModified WHERE Id=@Id";
            using (var con = new SqlConnection(_configuration.GetConnectionString("connectionstring")))
            {
                con.Open();
                var affectedRows = await con.ExecuteAsync(sql, entity);
                return affectedRows;
            }
        }
    }
}
