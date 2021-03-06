﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using NPress.Core.Domains;
using Xunit;

namespace NPress.Core.Test.Repositories.Sql
{
    public class RepositoryTest
    {
        private const string ConnectionString = "Data Source=(localdb)\\ProjectsV12;Initial Catalog=Sql;Integrated Security=True;Pooling=False;Connect Timeout=30";

        [Fact]
        public async Task Create_Success()
        {
            var repository = new Core.Repositories.Sql.PostRepository(ConnectionString);
            var post = new Post
            {
                Title = "Test title",
                UserId = "1",
                Content = "Test content.",
                CreationDateTime = DateTime.UtcNow,
                RevisionDateTime = DateTime.UtcNow
            };

            await repository.CreateAsync(post);

            Assert.NotNull(post.Id);

            using(var connection = new SqlConnection(ConnectionString))
            {
                var testPost = (await connection.QueryAsync<Post>($"SELECT * FROM [Post] WHERE [Id] = '{post.Id}';")).FirstOrDefault();

                Assert.NotNull(testPost);
                Assert.Equal(post.Id, testPost.Id);
            }
        }

        [Fact]
        public async Task Page_CorrectOrder()
        {
            var repository = new Core.Repositories.Sql.PostRepository(ConnectionString);
            var posts = await repository.PageAsync(null, 1, 2, true);
        }
    }
}
