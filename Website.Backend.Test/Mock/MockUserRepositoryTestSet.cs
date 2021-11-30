using System;
using System.Collections.Generic;
using Website.Backend.Models;
using Xunit;

namespace Website.Backend.Test.Mock
{
    public class MockUserRepositoryTestSet
    {
        private List<User> _mockUsers = new List<User>
                                        {
                                            new User
                                            {
                                                Id = 1,
                                                Name = "Foo",
                                                EmailAddress = "Foo@google.com",
                                                PasswordHash = "sdfdfasdfasdfsadf",
                                                CreatedDate = DateTimeOffset.UtcNow,
                                            },
                                            new User
                                            {
                                                Id = 2,
                                                Name = "sdfsdfsdfsd",
                                                EmailAddress = "sdfsdf@google.com",
                                                PasswordHash = "sdfdfasdfasdfsadf",
                                                CreatedDate = DateTimeOffset.UtcNow,
                                            },
                                            new User
                                            {
                                                Id = 3,
                                                Name = "fsfsfsfs",
                                                EmailAddress = "jthth@google.com",
                                                PasswordHash = "sdfdfasdfasdfsadf",
                                                CreatedDate = DateTimeOffset.UtcNow,
                                            }
                                        };

        [Fact]
        public async void GetAll_ReturnsUsers_GivenMockData()
        {
            // Arrange
            MockUserRepository target = new MockUserRepository(_mockUsers);

            // Act
            IEnumerable<User> response = await target.GetAll();

            // Assert
            Assert.NotEmpty(response);
        }

        [Fact]
        public async void GetById_ReturnsUsersGivenId()
        {
            // Arrange
            MockUserRepository target = new MockUserRepository(_mockUsers);

            // Act
            User response = await target.GetById(3);

            // Assert
            Assert.Equal("fsfsfsfs", response.Name);
        }

        [Fact]
        public async void Update_ChangesNameGiven_UpdatedUser()
        {
            // Arrange
            MockUserRepository target = new MockUserRepository(_mockUsers);

            User updateMe = new User
            {
                Id = 3,
                Name = "UPDATEDNAME!!!!",
                EmailAddress = "UPDATED EMAIL",
                PasswordHash = "sdfdfasdfasdfsadf",
                CreatedDate = DateTimeOffset.UtcNow,
            };

            // Act
            User response = await target.Update(updateMe);

            // Assert
            Assert.Equal(updateMe.Name, response.Name);
        }

        [Fact]
        public async void Delete_RemovesUser_GivenUserId()
        {
            // Arrange
            MockUserRepository target = new MockUserRepository(_mockUsers);

            // Act
            await target.Delete(3);

            User result = await target.GetById(3);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void Create_AddsUser_GivenUser()
        {
            // Arrange
            MockUserRepository target = new MockUserRepository(_mockUsers);

            User createMe = new User
            {
                Name = "Create me",
                EmailAddress = "Created EMAIL",
                PasswordHash = "sdfdfasdfasdfsadf",
                CreatedDate = DateTimeOffset.UtcNow,
            };

            // Act
            User result = await target.Create(createMe);

            // Assert
            Assert.NotNull(result);
            Assert.NotEqual(0, result.Id);
        }
    }
}
