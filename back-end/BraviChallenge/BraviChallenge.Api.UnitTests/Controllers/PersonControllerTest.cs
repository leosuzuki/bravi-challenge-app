using BraviChallenge.Api.Controllers;
using BraviChallenge.Application.Dtos.Requests;
using BraviChallenge.Application.Dtos.Responses;
using BraviChallenge.Application.Interfaces;
using BraviChallenge.Core.Enums;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace BraviChallenge.Api.UnitTests.Controllers
{
    public class PersonControllerTest
    {
        private PersonController _controller;
        private Mock<IPersonService> _service;

        public PersonControllerTest()
        {
            _service = new Mock<IPersonService>();
            _controller = new PersonController(_service.Object);
        }

        [Fact]
        public async Task Should_Return_List_Person()
        {
            List<PersonResponse> expected = new List<PersonResponse>()
            {
                new PersonResponse() { Id = 1, Name = "Pessoa 1" },
                new PersonResponse() { Id = 2, Name = "Pessoa 2" },
                new PersonResponse() { Id = 3, Name = "Pessoa 3" },
            };

            var result = await _controller.Get();

            _service.Verify(_ => _.GetPersons(), Times.Once);
            _service.Setup(_ => _.GetPersons()).ReturnsAsync(It.IsAny<List<PersonResponse>>());

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Should_Return_One_Person()
        {
            int idMock = 1;

            PersonResponse expected = new PersonResponse()
            {
                Id = 1,
                Name = "Pessoa 1",
                Contacts = new List<ContactResponse>()
                {
                    new ContactResponse() { Id = 1, ContactType = ContactType.Telefone, Description = "1736363636" },
                    new ContactResponse() { Id = 1, ContactType = ContactType.Email, Description = "test@mail.com" },
                    new ContactResponse() { Id = 1, ContactType = ContactType.Whatsapp, Description = "17997779999" }
                }
            };

            var result = await _controller.Get(idMock);

            _service.Verify(_ => _.GetPersonById(It.IsAny<int>()), Times.Once);
            _service.Setup(_ => _.GetPersonById(idMock)).ReturnsAsync(It.IsAny<PersonResponse>());

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Should_Create_Person()
        {
            var bodyMock = new CreatePersonRequest()
            {
                Name = "Pessoa 1",
                Contacts = new List<CreateContact>()
                {
                    new CreateContact() { ContactType = ContactType.Telefone, Description = "1736363636" },
                    new CreateContact() { ContactType = ContactType.Email, Description = "test@mail.com" },
                    new CreateContact() { ContactType = ContactType.Whatsapp, Description = "17997779999" }
                }
            };

            await _controller.Post(bodyMock);

            _service.Verify(_ => _.CreatePerson(It.IsAny<CreatePersonRequest>()), Times.Once);
            _service.Setup(_ => _.CreatePerson(bodyMock)).ReturnsAsync(It.IsAny<int>());
        }

        [Fact]
        public async Task Should_Update_Person()
        {
            int idMock = 1;

            var bodyMock = new UpdatePersonRequest()
            {
                Name = "Pessoa 1",
                Contacts = new List<UpdateContact>()
                {
                    new UpdateContact() { ContactType = ContactType.Telefone, Description = "1736363636" },
                    new UpdateContact() { ContactType = ContactType.Email, Description = "test@mail.com" },
                    new UpdateContact() { ContactType = ContactType.Whatsapp, Description = "17997779999" }
                }
            };

            await _controller.Put(idMock, bodyMock);

            _service.Verify(_ => _.UpdatePerson(It.IsAny<int>(), It.IsAny<UpdatePersonRequest>()), Times.Once);
            _service.Setup(_ => _.UpdatePerson(idMock, bodyMock)).ReturnsAsync(It.IsAny<bool>());
        }

        [Fact]
        public async Task Should_Delete_Person()
        {
            int idMock = 1;

            await _controller.Delete(idMock);

            _service.Verify(_ => _.ExcludePerson(It.IsAny<int>()), Times.Once);
            _service.Setup(_ => _.ExcludePerson(idMock)).ReturnsAsync(It.IsAny<bool>());
        }
    }
}
