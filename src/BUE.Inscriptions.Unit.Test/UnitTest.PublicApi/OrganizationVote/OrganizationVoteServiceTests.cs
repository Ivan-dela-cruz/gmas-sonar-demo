using AutoMapper;
using BUE.Inscriptions.Application.Implementation;
using BUE.Inscriptions.Domain.AppSettings;
using BUE.Inscriptions.Domain.Elecctions.DTO;
using BUE.Inscriptions.Domain.Response;
using BUE.Inscriptions.Infrastructure;
using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUE.Inscriptions.Unit.Test.UnitTest.PublicApi.OrganizationVote
{
    public class OrganizationVoteServiceTests
    {

        //[Fact]
        //public async Task CreateServiceAsync_ValidModel_ReturnsSuccessResponse()
        //{
        //    int init = 12000;
        //    int end = 12436;
        //    var configuration = new ConfigurationBuilder()
        //        .AddJsonFile("appsettings.Test.json")
        //        .Build();
        //    IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();

        //    using (var context = new PortalMatriculasDBContext(configuration))
        //    {
        //        var organizationVoteRepository = new OrganizationVoteRepository(context, mapper);
        //        var organizationVoteService = new OrganizationVoteService(organizationVoteRepository);

        //        var models = new List<OrganizationVoteDTO>();
        //        for (int userId = init; userId < end; userId++)
        //        {
        //            int randomElectionId = new Random().Next(1, 4);
        //            models.Add(new OrganizationVoteDTO()
        //            {
        //                ElectionId = 3,
        //                OrganizationElectionId = randomElectionId,
        //                UserId = userId,
        //                VoteType = "VALID",
        //                VoteDate = DateTime.Now
        //            });
        //        }

        //        // Act
        //        var results = new List<IBaseResponse<OrganizationVoteDTO>>();
        //        foreach (var model in models)
        //        {
        //            var result = await organizationVoteService.CreateServiceAsync(model);
        //            results.Add(result);
        //        }

        //        // Assert
        //        Assert.NotNull(results);
        //        Assert.Equal(end - init, results.Count); // Ajusta esto al número correcto de inserciones
        //        Assert.All(results, r => Assert.True(r.status));
        //    }
        //}
    }
}
